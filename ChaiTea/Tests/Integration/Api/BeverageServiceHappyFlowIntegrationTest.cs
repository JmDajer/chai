using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using BLL.DTO;
using Microsoft.AspNetCore.Mvc.Testing;
using Public.DTO.v1;
using Public.DTO.v1.Identity;

namespace Tests.Integration.Api;

//-------------------------------------------------------------------//
//                          Happy flow test                          //
//-------------------------------------------------------------------//
// Register 
// Login 
// Get beverages for the main view
// Select a beverage and get its info
// Look at beverage reviews
// Look at beverage reviews comments.
// Give beverage a review.
// Give beverage review a comment.
// Create custom beverage.
// Look if the custom beverage exist in custom beverages
// Look at recommended beverages from the previous review
// Logout
//-------------------------------------------------------------------//


public class BeverageServiceHappyFlowIntegrationTest : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;

    private readonly JsonSerializerOptions _camelCaseJsonSerializerOptions = new JsonSerializerOptions()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
    

    public BeverageServiceHappyFlowIntegrationTest(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }
    
    
    [Fact(DisplayName = "Test main happy flow - all steps one by one")]
    public async Task MainHappyFlowTest()
    {
        // Arrange
        const string url = "/Home";

        // Act
        var response = await _client.GetAsync(url);

        // Assert
        Assert.True(response.IsSuccessStatusCode);
        
        await RegisterUser();
    }


    /// <summary>
    /// Register new user.
    /// </summary>
    private async Task RegisterUser()
    {
        // Arrange
        const string url = "/api/v1/identity/account/register";
        const string email = "happy@flow.com";
        const string firstname = "Happy";
        const string lastname = "Flow";
        const string password = "Foo.bar.1";

        var registerData = new Register
        {
            Email = email,
            Password = password,
            Firstname = firstname,
            Lastname = lastname,
        };
        var data = JsonContent.Create(registerData);

        // Act
        var response = await _client.PostAsync(url, data);

        // Assert
        Assert.True(response.IsSuccessStatusCode);

        await LoginWithNewUser();
    }

    /// <summary>
    /// Login with the newly register user.
    /// </summary>
    private async Task LoginWithNewUser()
    {
        // Arrange
        const string url = "/api/v1/identity/account/login";
        const string email = "happy@flow.com";
        const string password = "Foo.bar.1";
        
        var loginData = new Login()
        {
            Email = email,
            Password = password,
        };
        var data = JsonContent.Create(loginData);
        
        // Act
        var response = await _client.PostAsync(url, data);
        
        var responseContent = await response.Content.ReadAsStringAsync();
        var jwtResponse = JsonSerializer.Deserialize<JWTResponse>(responseContent, _camelCaseJsonSerializerOptions);
        
        // Assert
        Assert.True(response.IsSuccessStatusCode);
        Assert.NotNull(jwtResponse);
        Assert.NotNull(jwtResponse.Jwt);
        Assert.NotNull(jwtResponse.RefreshToken);

        await GetBeverages(jwtResponse);
    }

    /// <summary>
    /// Get beverages that are for the public to see.
    /// </summary>
    /// <param name="jwtResponse">Users jwt object.</param>
    private async Task GetBeverages(JWTResponse jwtResponse)
    {
        //Arrange
        const string url = "/api/v1/Beverages";
        
        //Act
        var response = await _client.GetAsync(url);

        var responseContent = await response.Content.ReadAsStringAsync();
        var beveragesResponse =
            JsonSerializer.Deserialize<List<PublicBeverage>>(responseContent, _camelCaseJsonSerializerOptions);
        
        // Assert
        Assert.True(response.IsSuccessStatusCode);
        Assert.NotNull(beveragesResponse);

        var beverageId = beveragesResponse[0].Id;

        await GetBeverage(beverageId, jwtResponse);
    }

    /// <summary>
    /// Get a specific beverage from the list of beverages.
    /// </summary>
    /// <param name="beverageId">Beverages id.</param>
    /// <param name="jwtResponse">Users jwt object.</param>
    private async Task GetBeverage(Guid beverageId, JWTResponse jwtResponse)
    {
        //Arrange
        string url = $"api/v1/Beverages/{beverageId}";
        
        //Act
        var response = await _client.GetAsync(url);
        var responseContent = await response.Content.ReadAsStringAsync();
        var beveragesResponse = JsonSerializer
            .Deserialize<PublicBeverage>(responseContent, _camelCaseJsonSerializerOptions);
        
        // Assert
        Assert.True(response.IsSuccessStatusCode);
        Assert.NotNull(beveragesResponse);

        await GetBeverageReviews(beverageId, jwtResponse);
    }

    /// <summary>
    /// Get the chosen beverages reviews.
    /// </summary>
    /// <param name="beverageId">Beverages id.</param>
    /// <param name="jwtResponse">Users jwt object.</param>
    private async Task GetBeverageReviews(Guid beverageId, JWTResponse jwtResponse)
    {
        //Arrange
        string url = $"api/v1/Beverages/{beverageId}/Reviews";
        
        //Act
        var response = await _client.GetAsync(url);
        var responseContent = await response.Content.ReadAsStringAsync();
        var reviewsResponse = JsonSerializer
            .Deserialize<List<PublicReview>>(responseContent, _camelCaseJsonSerializerOptions);
        
        // Assert
        Assert.NotNull(reviewsResponse);
        Assert.True(reviewsResponse.Count() == 2);
        
        var reviewId = reviewsResponse[0].Id;

        await GetBeverageReviewsComments(beverageId, reviewId, jwtResponse);
    }

    /// <summary>
    /// Get the beverages first reviews comments.
    /// </summary>
    /// <param name="beverageId">Beverage id.</param>
    /// <param name="reviewId">Review id.</param>
    /// <param name="jwtResponse">Users jwt object.</param>
    private async Task GetBeverageReviewsComments(Guid beverageId, Guid reviewId, JWTResponse jwtResponse)
    {
        // Arrange
        string url = $"api/v1/Reviews/{reviewId}/Comments";
        
        //Act
        var response = await _client.GetAsync(url);
        var responseContent = await response.Content.ReadAsStringAsync();
        var commentsResponse = JsonSerializer
            .Deserialize<List<PublicComment>>(responseContent, _camelCaseJsonSerializerOptions);
        
        // Assert
        Assert.NotNull(commentsResponse);
        Assert.True(commentsResponse.Count() == 1);

        await PostBeverageReview(beverageId, jwtResponse);
    }

    /// <summary>
    /// Add a review to the chosen beverage.
    /// </summary>
    /// <param name="beverageId">Beverage id.</param>
    /// <param name="jwtResponse">Users jwt object.</param>
    private async Task PostBeverageReview(Guid beverageId, JWTResponse jwtResponse)
    {
        // Arrange
        string url = $"api/v1/Beverages/{beverageId}/Reviews";
        Guid userId = GetUserId(jwtResponse);
        var reviewData = new PublicReview()
        {
            AppUserId = userId,
            BeverageId = beverageId,
            Rating = Decimal.Parse("4"),
            ReviewText = "Decent tea, that's all I have to say!"
        };
        var data = JsonContent.Create(reviewData);
        
        var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtResponse.Jwt);
        request.Content = data;

        // Act
        var response = await _client.SendAsync(request);
        var responseContent = await response.Content.ReadAsStringAsync();
        var reviewResponse =
            JsonSerializer.Deserialize<PublicReview>(responseContent, _camelCaseJsonSerializerOptions);
        
        // Assert
        Assert.NotNull(reviewResponse);

        var reviewId = reviewResponse.Id;
        
        await PostBeverageReviewComment(beverageId, reviewId, jwtResponse);
    }
    
    /// <summary>
    /// Add a comment to the created review.
    /// </summary>
    /// <param name="beverageId">Beverage id.</param>
    /// <param name="reviewId">Review id.</param>
    /// <param name="jwtResponse">Users jwt object.</param>
    private async Task PostBeverageReviewComment(Guid beverageId, Guid reviewId, JWTResponse jwtResponse)
    {
        // Arrange
        string url = $"api/v1/Reviews/{reviewId}/Comments";
        Guid userId = GetUserId(jwtResponse);
        var commentData = new PublicComment()
        {
            AppUserId = userId,
            ReviewId = reviewId,
            Text = "I am agreeing so hard rn!!!"
        };
        var data = JsonContent.Create(commentData);
        
        var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtResponse.Jwt);
        request.Content = data;
        
        //Act
        var response = await _client.SendAsync(request);

        var responseContent = await response.Content.ReadAsStringAsync();
        var commentsResponse = JsonSerializer
            .Deserialize<PublicComment>(responseContent, _camelCaseJsonSerializerOptions);
        
        // Assert
        Assert.NotNull(commentsResponse);

        await GetRecommendedBeverages(jwtResponse);
    }
    
    /// <summary>
    /// Get recommended beverages for the user.
    /// </summary>
    /// <param name="jwtResponse">Users jwt object.</param>
    private async Task GetRecommendedBeverages(JWTResponse jwtResponse)
    {
        // Arrange
        var userId = GetUserId(jwtResponse);
        string url = $"api/v1/Beverages/Users/{userId}/Recommended";
        
        // Act
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtResponse.Jwt);
        
        var response = await _client.SendAsync(request);

        var responseContent = await response.Content.ReadAsStringAsync();
        var commentsResponse = JsonSerializer
            .Deserialize<List<PublicBeverage>>(responseContent, _camelCaseJsonSerializerOptions);
        
        // Assert
        Assert.NotNull(commentsResponse);
        
        await AddCustomBeverage(jwtResponse);
    }
    
    /// <summary>
    /// Add custom made beverage to the DB.
    /// </summary>
    /// <param name="jwtResponse">Users jwt object.</param>
    private async Task AddCustomBeverage(JWTResponse jwtResponse)
    {
        // Arrange
        const string url = "/api/v1/Beverages";
        Guid userId = GetUserId(jwtResponse);

        var beverageData = new BllBeverage()
        {
            AppUserId = userId,
            Name = "TEST BEVERAGE",
            Upc = "000000000013",
            Description = "Test beverage for testing!",
            Tags = new List<BllTag>(){ new BllTag()
            {
                Name = "TestTag",
                TagType = new BllTagType()
                {
                    Name = "TestTagType"
                }
            }}
        };
        var data = JsonContent.Create(beverageData);

        var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtResponse.Jwt);
        request.Content = data;
        
        // Act
        var response = await _client.SendAsync(request);
        
        // Assert
        Assert.True(response.IsSuccessStatusCode);
        
        await LogOut(jwtResponse);
    }

    /// <summary>
    /// Log user out.
    /// </summary>
    /// <param name="jwtResponse">Users jwt object.</param>
    private async Task LogOut(JWTResponse jwtResponse)
    {
        // Arrange
        const string url = "/api/v1/identity/account/logout";

        var logoutData = new Logout
        {
            RefreshToken = jwtResponse.RefreshToken
        };
        var data = JsonContent.Create(logoutData);

        var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtResponse.Jwt);
        request.Content = data;
        
        // Act
        var response = await _client.SendAsync(request);
        
        // Assert
        Assert.True(response.IsSuccessStatusCode);
    }

    
    /// <summary>
    /// Helper method to get the user id from the jwt.
    /// </summary>
    /// <param name="jwtResponse">Users jwt object.</param>
    /// <returns></returns>
    private Guid GetUserId(JWTResponse jwtResponse)
    {
        var token = jwtResponse.Jwt;  
        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(token);

        var UserId = jwtSecurityToken.Claims.First(claim => claim.Type.Contains("nameidentifier")).Value;

        return Guid.Parse(UserId);
    }
}