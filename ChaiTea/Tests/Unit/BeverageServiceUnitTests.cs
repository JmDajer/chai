using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using BLL.App.Mappers;
using BLL.App.Services;
using BLL.DTO;
using DAL.EF.App;
using DAL.EF.App.Seeding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace Tests.Unit;

public class BeverageServiceUnitTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly BeverageService _service;
    private readonly ApplicationDbContext _context;
    
    private static readonly Guid AdminId = Guid.Parse("69a435c1-9d50-4d38-af58-c9406356efad");
    private static readonly Guid YorkshireBiscuitBeverageId = Guid.Parse("B35A1313-140B-450D-858A-53C167BF0865");
    private static readonly Guid TestBeverageId = Guid.Parse("00F725B4-596B-4D74-B1A9-834556F2137C");

    public BeverageServiceUnitTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;

        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new DAL.EF.App.AutoMapperConfig());
            cfg.AddProfile(new BLL.App.AutoMapperConfig());
        });

        var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        _context = new ApplicationDbContext(optionBuilder.Options);
        var uow = new AppUow(_context, mapperConfig.CreateMapper());
        
        // reset database
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
        
        using var logFactory = LoggerFactory.Create(builder => builder.AddConsole());
        var logger = logFactory.CreateLogger<BeverageService>();

        
        // SUT
        _service = new BeverageService(uow, new BllBeverageMapper(mapperConfig.CreateMapper()));
        AppDataInit.SeedAppData(_context);
    }
    
    [Fact(DisplayName = "GET - Beverages are present.")]
    public async Task TestGetPublicBeverages()
    {
        // Assert and Act
        var beverages = await _service.AllAsync();

        // Assert
        Assert.NotNull(beverages);
    }
    
    [Fact(DisplayName = "GET - Beverages are for the public only.")]
    public async Task TestGettingAllPublicBeverages()
    {
        // Arrange and Act
        var beverages = await _service.AllAsync();

        // Assert
        foreach (var beverage in beverages)
        {
            // Check if the beverages are made by the admin, because those are meant for everyone to see.
            Assert.True(beverage.AppUserId == AdminId);
        }
    }
    
    [Fact(DisplayName = "GET - Yorkshire Tea Biscuit Brew beverage.")]
    public async Task TestGettingYorkshireBiscuitBeverage()
    {
        // Arrange and Act
        var beverage = await _service.FindAsync(YorkshireBiscuitBeverageId);

        // Assert
        Assert.NotNull(beverage);
        Assert.IsType<BllBeverage>(beverage);
        Assert.True(beverage.Id == YorkshireBiscuitBeverageId);
    }
    
    [Fact(DisplayName = "POST - Add a new beverage.")]
    public void TestAddNewBeverage()
    {
        // Arrange
        Guid userId = Guid.Parse("2F41B7E9-1041-4FBB-95C1-0A35BA4713EC");
        const string beverageName = "Test beverage";
        const string upc = "0000000000013";
        const string description = "Test beverage is testing hard rn...";

        var testBeverage = new BllBeverage()
        {
            Id = TestBeverageId,
            AppUserId = AdminId,
            Name = beverageName,
            Description = description,
            Upc = upc,
            Tags = new List<BllTag>(){new BllTag()
            {
                Name = "TestTag",
                TagType = new BllTagType()
                {
                    Name = "TestTagType"
                }
            }}
        };
        
        // Act
        var addedBeverage = _service.Add(testBeverage);

        // Assert
        Assert.NotNull(addedBeverage);
        Assert.IsType<BllBeverage>(addedBeverage);
        Assert.True(addedBeverage.Id == TestBeverageId);
        Assert.True(addedBeverage.Name == beverageName);
        Assert.True(addedBeverage.Upc == upc);
        Assert.True(addedBeverage.Description == description);
    }
    
    [Fact(DisplayName = "PUT - Update beverage.")]
    public async Task TestUpdateBeverage()
    {
        // Arrange and Act
        const string newName = "NEW NAME FOR TESTING";
        var beverageToUpdate = await _service.FindAsync(YorkshireBiscuitBeverageId);
        
        // Assert
        Assert.NotNull(beverageToUpdate);
        Assert.IsType<BllBeverage>(beverageToUpdate);
        Assert.Equal(beverageToUpdate.Id,YorkshireBiscuitBeverageId);

        // Arrange
        beverageToUpdate.Name = newName;
        
        // Act
        var updatedBeverage = _service.Update(beverageToUpdate);

        // Assert
        Assert.NotNull(updatedBeverage);
        Assert.IsType<BllBeverage>(updatedBeverage);
    }
    
    [Fact(DisplayName = "DELETE - Delete beverage.")]
    public async Task TestDeleteBeverage()
    {
        // Arrange and Act
        await _service.RemoveAsync(YorkshireBiscuitBeverageId);

        var deleted = await _service.FindAsync(YorkshireBiscuitBeverageId);

        // Assert
        Assert.Null(deleted);
    }
    
    [Fact(DisplayName = "GET - User created beverages.")]
    public async Task TestUsersBeverages()
    {
        // Arrange and Act
        var userBeverages = await _service.GetUserBeverages(AdminId);
        // Assert
        Assert.NotNull(userBeverages);
    }
    
    [Fact(DisplayName = "GET - Recommended beverages for user.")]
    public async Task TestUserRecommendedBeverages()
    {
        // Arrange and Act
        var recommendedBeverages = await _service.GetUserRecommendedBeverages(AdminId);
        // Assert
        Assert.NotNull(recommendedBeverages);
        Assert.True(recommendedBeverages.Count() > 0);
    }
}

