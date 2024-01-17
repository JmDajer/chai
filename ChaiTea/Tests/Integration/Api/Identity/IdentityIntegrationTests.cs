using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using Domain.App;
using Microsoft.AspNetCore.Mvc.Testing;
using Mono.TextTemplating;
using Public.DTO.v1.Identity;
using Xunit.Abstractions;

namespace Tests.Integration.api.identity;

public class IdentityIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;
    private readonly int _jwtTimeInSeconds = 60;

    private readonly JsonSerializerOptions _camelCaseJsonSerializerOptions = new JsonSerializerOptions()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public IdentityIntegrationTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    [Fact(DisplayName = "POST - register new user")]
    public async Task RegisterNewUserTest()
    {
        // Arrange
        const string url = "/api/v1/identity/account/register";
        const string email = "register@test.ee";
        const string password = "Foo.bar1";
        const string firstName = "Register";
        const string lastName = "Test";

        var registerData = new
        {
            Email = email,
            Password = password,
            Firstname = firstName,
            Lastname = lastName
        };
        var data = JsonContent.Create(registerData);

        // Act
        var response = await _client.PostAsync(url, data);

        // Assert
        var responseContent = await response.Content.ReadAsStringAsync();
        Assert.True(response.IsSuccessStatusCode);
        VerifyJwtContent(responseContent, email, DateTime.Now.AddSeconds(_jwtTimeInSeconds + 1).ToUniversalTime());
    }

    private void VerifyJwtContent(string jwt, string email, DateTime validToIsSmallerThan)
    {
        var jwtResponse = JsonSerializer.Deserialize<JWTResponse>(jwt, _camelCaseJsonSerializerOptions);

        Assert.NotNull(jwtResponse);
        Assert.NotNull(jwtResponse.RefreshToken);
        Assert.NotNull(jwtResponse.Jwt);

        // verify the actual JWT
        var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(jwtResponse.Jwt);
        Assert.Equal(email, jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value);
        Assert.True(jwtToken.ValidTo < validToIsSmallerThan);
    }

    private async Task<string> RegisterNewUser(string email, string password, string firstName, string lastName)
    {
        var url = "/api/v1/identity/account/register";
        var registerData = new
        {
            Email = email,
            Password = password,
            Firstname = firstName,
            Lastname = lastName
        };
        var data = JsonContent.Create(registerData);
        
        // Act
        var response = await _client.PostAsync(url, data);
        var responseContent = await response.Content.ReadAsStringAsync();
        
        // Assert
        Assert.True(response.IsSuccessStatusCode);
        VerifyJwtContent(responseContent, email, DateTime.Now.AddSeconds(_jwtTimeInSeconds + 1).ToUniversalTime());
        return responseContent;
    }
    
    [Fact(DisplayName = "POST - login user")]
    public async Task LoginUserTest()
    {
        const string email = "login@test.ee";
        const string password = "Foo.bar1";
        const string firstName = "Login";
        const string lastName = "Test";

        // Arrange
        await RegisterNewUser(email, password, firstName, lastName);
        var url = "/api/v1/identity/account/login";
        var loginData = new
        {
            Email = email,
            Password = password,
        };
        var data = JsonContent.Create(loginData);

        // Act
        var response = await _client.PostAsync(url, data);
        var responseContent = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.True(response.IsSuccessStatusCode);
        VerifyJwtContent(responseContent, email, DateTime.Now.AddSeconds(_jwtTimeInSeconds + 1).ToUniversalTime());
    }

    [Fact(DisplayName = "POST - JWT expired")]
    public async Task JWTExpired()
    {
        const string email = "expired@test.ee";
        const string password = "Foo.bar1";
        const string firstName = "Expired";
        const string lastName = "Test";
        const string url = "/api/v1/Beverages/4F66FCF3-2760-4E2A-AA14-0938B7498C56/Pictures";
        
        // Arrange
        var jwt = await RegisterNewUser(email, password, firstName, lastName);
        var jwtResponse = JsonSerializer.Deserialize<JWTResponse>(jwt, _camelCaseJsonSerializerOptions);
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtResponse!.Jwt);

        // Act
        var response = await _client.SendAsync(request);

        // Assert
        Assert.True(response.IsSuccessStatusCode);

        // Arrange
        await Task.Delay((_jwtTimeInSeconds + 2) * 1000);

        var request2 = new HttpRequestMessage(HttpMethod.Get, url);
        request2.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtResponse.Jwt);

        // Act
        var response2 = await _client.SendAsync(request2);

        // Assert
        Assert.False(response2.IsSuccessStatusCode);
    }

    [Fact(DisplayName = "POST - JWT renewal")]
    public async Task JWTRenewal()
    {
        const string email = "renewal@test.ee";
        const string password = "Foo.bar1";
        const string firstName = "Renewal";
        const string lastName = "Test";

        const string url = "/api/v1/Beverages/4F66FCF3-2760-4E2A-AA14-0938B7498C56/Pictures";

        // Arrange
        var jwt = await RegisterNewUser(email, password, firstName, lastName);
        var jwtResponse = JsonSerializer.Deserialize<JWTResponse>(jwt, _camelCaseJsonSerializerOptions);
        
        // let the jwt expire
        await Task.Delay((_jwtTimeInSeconds + 2) * 1000);

        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtResponse!.Jwt);

        // Act
        var response = await _client.SendAsync(request);

        // Assert
        Assert.False(response.IsSuccessStatusCode);

        // Arrange
        var refreshUrl = "/api/v1/identity/account/refreshtoken";
        var refreshData = new
        {
            JWT = jwtResponse.Jwt,
            RefreshToken = jwtResponse.RefreshToken,
        };

        var data =  JsonContent.Create(refreshData);
        
        var response2 = await _client.PostAsync(refreshUrl, data);
        var responseContent2 = await response2.Content.ReadAsStringAsync();
        
        Assert.True(response2.IsSuccessStatusCode);
        
        jwtResponse = JsonSerializer.Deserialize<JWTResponse>(responseContent2, _camelCaseJsonSerializerOptions);

        var request3 = new HttpRequestMessage(HttpMethod.Get, url);
        request3.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtResponse!.Jwt);

        // Act
        var response3 = await _client.SendAsync(request3);
        // Assert
        Assert.True(response3.IsSuccessStatusCode);
    }
}
