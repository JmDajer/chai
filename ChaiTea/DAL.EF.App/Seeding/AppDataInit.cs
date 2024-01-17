using System.Security.Claims;
using Domain.App;
using Domain.App.Identity;
using Domain.App.ManyToMany;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Seeding;

public class AppDataInit
{
    private static readonly Guid AdminId = Guid.Parse("69a435c1-9d50-4d38-af58-c9406356efad");
    
    private static readonly Guid YorkshireBiscuitBeverageId = Guid.Parse("B35A1313-140B-450D-858A-53C167BF0865");
    private static readonly Guid YorkshireBeverageId = Guid.Parse("B35A1313-140B-450D-858A-53C167BF0866");
    private static readonly Guid YorkshireGoldBeverageId = Guid.Parse("52BC4BE0-B3EC-459C-A198-7338E31E0AA0");
    private static readonly Guid ClipperSnorePeaceBeverageId = Guid.Parse("B35A1313-140B-450D-858A-53C167BF0868");
    private static readonly Guid RimiGunpowderBeverageId = Guid.Parse("B35A1313-140B-450D-858A-53C167BF0869");
    private static readonly Guid RimiEarlGrayBeverageId = Guid.Parse("B35A1313-140B-450D-858A-53C167BF0870");
    private static readonly Guid AhmadTeaGreenBeverageId = Guid.Parse("B35A1313-140B-450D-858A-53C167BF0871");
    private static readonly Guid HerbaKamilleBeverageId = Guid.Parse("B35A1313-140B-450D-858A-53C167BF0872");
    private static readonly Guid HerbaMintBeverageId = Guid.Parse("B35A1313-140B-450D-858A-53C167BF0873");
    
    private static readonly Guid BlackTeaTagId = Guid.Parse("5C9E9478-DE7C-4B0D-B4ED-0CA5467ACB74");
    private static readonly Guid GreenTeaTagId = Guid.Parse("5C9E9478-DE7C-4B0D-B4ED-0CA5467ACB75");
    private static readonly Guid WhiteTeaTagId = Guid.Parse("5C9E9478-DE7C-4B0D-B4ED-0CA5467ACB76");
    private static readonly Guid OolongTeaTagId = Guid.Parse("5C9E9478-DE7C-4B0D-B4ED-0CA5467ACB77");
    private static readonly Guid PuerhTeaTagId = Guid.Parse("5C9E9478-DE7C-4B0D-B4ED-0CA5467ACB78");
    private static readonly Guid BasicTagId = Guid.Parse("5C9E9478-DE7C-4B0D-B4ED-0CA5467ACB79");
    private static readonly Guid FlavouredTagId = Guid.Parse("5C9E9478-DE7C-4B0D-B4ED-0CA5467ACB80");
    private static readonly Guid HerbalTagId = Guid.Parse("5C9E9478-DE7C-4B0D-B4ED-0CA5467ACB81");
    private static readonly Guid SweetTagId = Guid.Parse("5C9E9478-DE7C-4B0D-B4ED-0CA5467ACB82");
    private static readonly Guid BitterTagId = Guid.Parse("5C9E9478-DE7C-4B0D-B4ED-0CA5467ACB83");
    private static readonly Guid SourTagId = Guid.Parse("5C9E9478-DE7C-4B0D-B4ED-0CA5467ACB84");
    private static readonly Guid NuttyTagId = Guid.Parse("5C9E9478-DE7C-4B0D-B4ED-0CA5467ACB85");
    private static readonly Guid MintyTagId = Guid.Parse("5C9E9478-DE7C-4B0D-B4ED-0CA5467ACB86");
    
    private static readonly Guid GreenFieldTagId = Guid.Parse("5C9E9478-DE7C-4B0D-B4ED-0CA5467ACB87");
    private static readonly Guid YorkshireTagId = Guid.Parse("5C9E9478-DE7C-4B0D-B4ED-0CA5467ACB88");
    private static readonly Guid RimiTagId = Guid.Parse("5C9E9478-DE7C-4B0D-B4ED-0CA5467ACB89");
    private static readonly Guid DilmahTagId = Guid.Parse("5C9E9478-DE7C-4B0D-B4ED-0CA5467ACB90");
    private static readonly Guid AhmadTeaTagId = Guid.Parse("5C9E9478-DE7C-4B0D-B4ED-0CA5467ACB91");
    private static readonly Guid HerbaTagId = Guid.Parse("5C9E9478-DE7C-4B0D-B4ED-0CA5467ACB92");
    private static readonly Guid DanyunTagId = Guid.Parse("5C9E9478-DE7C-4B0D-B4ED-0CA5467ACB93");
    private static readonly Guid LiptonTagId = Guid.Parse("5C9E9478-DE7C-4B0D-B4ED-0CA5467ACB94");
    private static readonly Guid TeekanneTagId = Guid.Parse("5C9E9478-DE7C-4B0D-B4ED-0CA5467ACB95");
    private static readonly Guid ClipperTagId = Guid.Parse("5C9E9478-DE7C-4B0D-B4ED-0CA5467ACB96");
    
    // Tag types
    private static readonly TagType CompanyTagType = new() {Id = Guid.Parse("4ac53f43-cb4d-4787-98ac-d9703a0df6ac"), Name = "Company" };                                           
    private static readonly TagType TeaTypeTagType = new() {Id = Guid.Parse("ac6a1a61-451c-44f9-8df2-f6e94769674c"), Name = "Tea Type" }; 
    private static readonly TagType CategoryTagType = new() {Id = Guid.Parse("6ea7a9c1-d14a-48ae-a7c2-b9de80ee5f18"), Name = "Category" };  
    private static readonly TagType FlavourTagType = new() {Id = Guid.Parse("6aa8701a-ee73-40b6-a49f-53ef718530e1"), Name = "Tag" }; 
    
    // Tags
    // Tea type tags
    private static readonly Tag BlackTeaTag = new()
        { Id = BlackTeaTagId, Name = "Black tea", TagType = TeaTypeTagType };
    private static readonly Tag GreenTeaTag = new()
        { Id = GreenTeaTagId, Name = "Green tea", TagType = TeaTypeTagType};
    private static readonly Tag WhiteTeaTag = new()
        { Id = WhiteTeaTagId, Name = "White tea", TagType = TeaTypeTagType }; 
    private static readonly Tag OolongTeaTag = new() 
        { Id = OolongTeaTagId,Name = "Oolong tea", TagType = TeaTypeTagType };  
    private static readonly Tag PuerhTeaTag = new() 
        { Id = PuerhTeaTagId, Name = "Pu-erh tea", TagType = TeaTypeTagType };
    
    // Category tags
    private static readonly Tag BasicTag = new() 
        { Id = BasicTagId,Name = "Basic", TagType = CategoryTagType };
    private static readonly Tag FlavouredTag = new() 
        { Id = FlavouredTagId,Name = "Flavoured", TagType = CategoryTagType };
    private static readonly Tag HerbalTag = new()
        { Id = HerbalTagId, Name = "Herbal", TagType = CategoryTagType };
    
    // Flavour tags
    private static readonly Tag SweetTag = new() 
        { Id = SweetTagId,Name = "Sweet", TagType = FlavourTagType };
    private static readonly Tag BitterTag = new() 
        { Id = BitterTagId,Name = "Bitter", TagType = FlavourTagType };
    private static readonly Tag SourTag = new() 
        { Id = SourTagId,Name = "Sour", TagType = FlavourTagType };
    private static readonly Tag NuttyTag = new() 
        { Id = NuttyTagId,Name = "Nutty", TagType = FlavourTagType };
    private static readonly Tag MintyTag = new() 
        { Id = MintyTagId,Name = "Minty", TagType = FlavourTagType };
    
    // Company tags
    private static readonly Tag GreenFieldTag = new()
        { Id = GreenFieldTagId, Name = "Greenfield", TagType = CompanyTagType };
    private static readonly Tag YorkshireTag = new()
        { Id = YorkshireTagId, Name = "Yorkshire Tea", TagType = CompanyTagType };
    private static readonly Tag RimiTag = new() 
        { Id = RimiTagId, Name = "Rimi", TagType = CompanyTagType };
    private static readonly Tag DilmahTag = new() 
        { Id = DilmahTagId, Name = "Dilmah", TagType = CompanyTagType };
    private static readonly Tag AhmadTeaTag = new()
        { Id = AhmadTeaTagId, Name = "Ahmad Tea", TagType = CompanyTagType };
    private static readonly Tag HerbaTag = new() 
        { Id = HerbaTagId, Name = "Herba", TagType = CompanyTagType };
    private static readonly Tag DanyunTag = new() 
        { Id = DanyunTagId, Name = "Danyun", TagType = CompanyTagType };
    private static readonly Tag LiptonTag = new() 
        { Id = LiptonTagId, Name = "Lipton", TagType = CompanyTagType };
    private static readonly Tag TeekanneTag = new()
        { Id = TeekanneTagId, Name = "Teekanne", TagType = CompanyTagType };
    private static readonly Tag ClipperTag = new() 
        { Id = ClipperTagId, Name = "Clipper", TagType = CompanyTagType };                        
           
    // Ingredient types
    private static readonly IngredientType SpiceIngredientType = new() {Id = Guid.NewGuid(), Name = "Spice" };
    private static readonly IngredientType FruitIngredientType = new() {Id = Guid.NewGuid(), Name = "Fruit" };
    private static readonly IngredientType SyrupIngredientType = new() {Id = Guid.NewGuid(), Name = "Syrup" };
    private static readonly IngredientType LiquidIngredientType = new() { Id = Guid.NewGuid(), Name = "Liquid" };
    private static readonly IngredientType SolidIngredientType = new() { Id = Guid.NewGuid(), Name = "Solid" };
    
    // Ingredients
    private static readonly Ingredient MilkIngredient = new Ingredient()
        { Id = Guid.NewGuid(), Name = "Milk", IngredientType = LiquidIngredientType };
    private static readonly Ingredient CitrusIngredient = new Ingredient()
        { Id = Guid.NewGuid(), Name = "Citrus", IngredientType = FruitIngredientType };
    private static readonly Ingredient SugarIngredient = new Ingredient()
        { Id = Guid.NewGuid(), Name = "Sugar", IngredientType = SpiceIngredientType };
    private static readonly Ingredient HoneyIngredient = new Ingredient()
        { Id = Guid.NewGuid(), Name = "Honey", IngredientType = SolidIngredientType };
    private static readonly Ingredient CaramelSyrupIngredient = new Ingredient()
        { Id = Guid.NewGuid(), Name = "Caramel syrup", IngredientType = SyrupIngredientType };
                                     
    // Initial pictures for the MVP
    private static readonly Picture YorkshireBiscuitPicture = new()
        { Id = Guid.NewGuid(), Url = "ED2A5C40-DC40-4E69-99CD-9C392ABA4B1E.jpg",
            BeverageId = YorkshireBiscuitBeverageId};
    
    private static readonly Picture YorkshirePicture = new()
    { Id = Guid.NewGuid(), Url = "F0E2984C-B190-4847-A47F-6AF546BD039D.jpg",
        BeverageId = YorkshireBeverageId};
    
    private static readonly Picture YorkshireGoldPicture = new()
    { Id = Guid.NewGuid(), Url = "D695491C-85B2-4D59-9548-853E3ECE46BD.jpg",
        BeverageId = YorkshireGoldBeverageId};
    
    private static readonly Picture ClipperSnorePeacePicture = new()
    { Id = Guid.NewGuid(), Url = "5B8313B3-E29A-422F-BE59-0E4BCDD43185.jpg",
        BeverageId = ClipperSnorePeaceBeverageId};
    
    private static readonly Picture RimiGunpowderPicture = new()
    { Id = Guid.NewGuid(), Url = "464F8552-A546-42C2-907A-628A1F2F787F.jpg",
        BeverageId = RimiGunpowderBeverageId};
    
    private static readonly Picture RimiEarlGrayPicture = new()
    { Id = Guid.NewGuid(), Url = "F41548B2-C697-4928-8547-F3BDBB712026.jpg",
        BeverageId = RimiEarlGrayBeverageId};
    
    private static readonly Picture AhmadTeaGreenPicture = new()
    { Id = Guid.NewGuid(), Url = "150F1F4F-1156-47FA-AF92-E249E970775A.jpg",
        BeverageId = AhmadTeaGreenBeverageId};
    
    private static readonly Picture HerbaKamillePicture = new()
    { Id = Guid.NewGuid(), Url = "C3A4EE84-7C5D-41D0-BC6B-9FA829530A8E.jpg",
        BeverageId = HerbaKamilleBeverageId};
    
    private static readonly Picture HerbaMintPicture = new()
    { Id = Guid.NewGuid(), Url = "98134128-CB32-44D2-B00C-8434AE9A5D92.jpg",
        BeverageId = HerbaMintBeverageId};
    
    // Initial comments for the MVP

    private static readonly Comment YorkshireBiscuitReview1Comment = new()
    {
        Id = Guid.NewGuid(),
        Name = "Admin App",
        AppUserId = AdminId,
        ReviewId = Guid.Parse("E5C799CD-2A85-43E5-9BB2-BE1ACDF68764"),
        Text = "I totally agree with you!"
    };
    
    private static readonly Comment ClippersSleepReviewComment = new()
    {
        Id = Guid.NewGuid(),
        Name = "Admin App",
        AppUserId = AdminId,
        ReviewId = Guid.Parse("E5C799CD-2A85-43E5-9BB2-BE1ACDF68761"),
        Text = "I've also tried this tea and it defo is such a good sleep tea!"
    };
    
    // Initial reviews for the MVP
    private static readonly Review YorkshireBiscuitReview1 = new()
    {
        Id = Guid.Parse("E5C799CD-2A85-43E5-9BB2-BE1ACDF68761"),
        Name = "Admin App",
        AppUserId = AdminId,
        BeverageId = YorkshireBiscuitBeverageId,
        Rating = Decimal.Parse("4.8"),
        ReviewText = "This tea is really good, the biscuit favour with" +
                     " the strong bitter taste, makes it absolutely delicious.",
        Comments = new List<Comment>(){YorkshireBiscuitReview1Comment}
    };
    
    private static readonly Review YorkshireBiscuitReview2 = new()
    {
        Id = Guid.Parse("E5C799CD-2A85-43E5-9BB2-BE1ACDF68762"),
        Name = "Admin App",
        AppUserId = AdminId,
        BeverageId = YorkshireBiscuitBeverageId,
        Rating = Decimal.Parse("3.5"),
        ReviewText = "After drinking this tea for a month now, I've decided that its not that good :(."
    };
    
    private static readonly Review YorkshireReview = new()
    {
        Id = Guid.Parse("E5C799CD-2A85-43E5-9BB2-BE1ACDF68763"),
        Name = "Admin App",
        AppUserId = AdminId,
        BeverageId = YorkshireBeverageId,
        Rating = Decimal.Parse("3.7"),
        ReviewText = "It's worth the money definitely, but the taste isn't as good as Yorkshire Gold."
    };
    
    private static readonly Review ClippersSleepReview = new()
    {
        Id = Guid.Parse("E5C799CD-2A85-43E5-9BB2-BE1ACDF68764"),
        Name = "Admin App",
        AppUserId = AdminId,
        BeverageId = ClipperSnorePeaceBeverageId,
        Rating = Decimal.Parse("5.0"),
        ReviewText = "One of the best sleep teas, I've ever had!",
        Comments = new List<Comment>(){ClippersSleepReviewComment}
    };
    
    // Initial beverages for the MVP
    private static readonly Beverage YorkshireBiscuitBeverage = new()
    {
        Id = YorkshireBiscuitBeverageId,
        AppUserId = AdminId,
        Name = "Yorkshire Tea Biscuit Brew",
        Upc = "615357123403",
        Description = "Here's a miraculous tea that tastes like biscuits - " +
                      "because when those two flavours combine, the resulting" +
                      " deliciousness creates a wave of happiness big enough" +
                      " to power an entire human being! It's a magical mug" +
                      " of biscuity goodness that doesn't get crumbs on your jumper.",
        //Tags = new List<Tag>(){YorkshireTag, BitterTag, SweetTag, FlavouredTag, BlackTeaTag},
        Pictures = new List<Picture>(){YorkshireBiscuitPicture},
        Reviews = new List<Review>(){YorkshireBiscuitReview1, YorkshireBiscuitReview2}
    };
    
    private static readonly Beverage YorkshireBeverage = new()
    {
        Id = YorkshireBeverageId,
        AppUserId = AdminId,
        Name = "Yorkshire Tea Bags Pack of 160",
        Upc = "5010357112085",
        Description = "A proper brew - pure and simple. Yorkshire Tea made from farms in Africa and India," +
                      " producing a blend that's big on flavour. A refreshing drink which is kind to the people" +
                      " who grow it by paying them fair prices.",
        //Tags = new List<Tag>(){YorkshireTag, BitterTag, BlackTeaTag},
        Pictures = new List<Picture>(){YorkshirePicture},
        Reviews = new List<Review>(){YorkshireReview}
    };
    
    private static readonly Beverage YorkshireGoldBeverage = new()
    {
        Id = YorkshireGoldBeverageId,
        AppUserId = AdminId,
        Name = "Yorkshire Gold",
        Upc = "5010357112108",
        Description = "Premium version of the Yorkshire tea series. Has a better black tea aroma, that's less bitter.",
        //Tags = new List<Tag>(){YorkshireTag, BitterTag, BlackTeaTag},
        Pictures = new List<Picture>(){YorkshireGoldPicture}
    };
    
    private static readonly Beverage ClipperSnorePeaceBeverage = new()
    {
        Id = ClipperSnorePeaceBeverageId,
        AppUserId = AdminId,
        Name = "Clipper Organic Snore & Peace",
        Upc = "5021991941757",
        Description = "Chamomile lemon balm & lavender organic infusion." +
                      " Just the thing before lights outClipper products are" +
                      " made with pure ingredients and a clear conscience.",
        //Tags = new List<Tag>(){ClipperTag, HerbalTag},
        Pictures = new List<Picture>(){ClipperSnorePeacePicture},
        Reviews = new List<Review>(){ClippersSleepReview}
    };
    
    private static readonly Beverage RimiGunpowderBeverage = new()
    {
        Id = RimiGunpowderBeverageId,
        AppUserId = AdminId,
        Name = "Selection By Rimi Gunpowder green loose leaf tea",
        Upc = "4752050017755",
        Description = "Loose leaf green tea in gunpowder form.",
        //Tags = new List<Tag>(){RimiTag, GreenTeaTag, NuttyTag},
        Pictures = new List<Picture>(){RimiGunpowderPicture}
    };
    
    private static readonly Beverage RimiEarlGrayBeverage = new()
    {
        Id = RimiEarlGrayBeverageId,
        AppUserId = AdminId,
        Name = "Rimi Earl Grey Black Tea",
        Upc = "4752050206845",
        Description = "Bag earl grey black tea",
        //Tags = new List<Tag>(){RimiTag, BlackTeaTag, FlavouredTag},
        Pictures = new List<Picture>(){RimiEarlGrayPicture}
    };
    
    private static readonly Beverage AhmadTeaGreenBeverage = new()
    {
        Id = AhmadTeaGreenBeverageId,
        AppUserId = AdminId,
        Name = "Ahmad Tea Green Tea Herbata Liciasta 100g",
        Upc = "054881008020",
        //Tags = new List<Tag>(){AhmadTeaTag, GreenTeaTag},
        Pictures = new List<Picture>(){AhmadTeaGreenPicture}
    };
    
    private static readonly Beverage HerbaKamilleBeverage = new()
    {
        Id = HerbaKamilleBeverageId,
        AppUserId = AdminId,
        Name = "Herba Kamille - Camomile herbal infusion",
        Upc = "4009300018193",
        Description = "Basic chamomile tea.",
        //Tags = new List<Tag>(){HerbaTag, HerbalTag},
        Pictures = new List<Picture>(){HerbaKamillePicture}
    };
    
    private static readonly Beverage HerbaMintBeverage = new()
    {
        Id = HerbaMintBeverageId,
        AppUserId = AdminId,
        Name = "Herba Pfefferminze - Peppermint herbal infusion",
        Upc = "4009300018186",
        Description = "Basic pepperming tea.",
        //Tags = new List<Tag>(){HerbaTag, HerbalTag, MintyTag},
        Pictures = new List<Picture>(){HerbaMintPicture}
    };
    
    public static void MigrateDatabase(ApplicationDbContext context)
    {
        context.Database.Migrate();
    }
    public static void DropDatabase(ApplicationDbContext context)
    {
        context.Database.EnsureDeleted();  
    }

    public static void SeedIdentity(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        (Guid id, string email, string password) userData = (AdminId, "admin@app.com", "Foo.bar.1");
        var user = userManager.FindByEmailAsync(userData.email).Result;
        if (user != null) return;
        user = new AppUser()
        {
            Id = userData.id,
            UserName = userData.email,
            Email = userData.email,
            FirstName = "Admin",
            LastName = "App",
            EmailConfirmed = true,
        };
        
        var result = userManager.CreateAsync(user, userData.password).Result;
        if (!result.Succeeded) throw new ApplicationException("Cannot seed users");
        
        // Add claims of the admin user
        userManager.AddClaimsAsync(user, new List<Claim>()
        {
            new(ClaimTypes.GivenName, user.FirstName),
            new(ClaimTypes.Surname, user.LastName)
        });
    }

    public static void SeedAppData(ApplicationDbContext context)
    {
        SeedAppDataPictures(context);
        SeedAppDataComments(context);
        SeedAppDataReviews(context);
        SeedAppDataIngredientType(context);
        SeedAppDataIngredient(context);
        SeedAppDataBeverages(context);
        SeedAppDataTagTypes(context);
        SeedAppDataTags(context);
        SeedAppDataBeverageTags(context);
        context.SaveChanges();
    }
    
    private static void SeedAppDataTagTypes(ApplicationDbContext context)
    {
        if (!context.TagTypes.Any())
        {
            context.TagTypes.AddRange(CompanyTagType, CategoryTagType, FlavourTagType, TeaTypeTagType);
        }
    }

    private static void SeedAppDataTags(ApplicationDbContext context)
    {
        if (!context.Tags.Any())
        {
            context.Tags.AddRange(BlackTeaTag, GreenTeaTag, WhiteTeaTag, OolongTeaTag, PuerhTeaTag,
                BasicTag, FlavouredTag, HerbalTag,
                SweetTag, BitterTag, SourTag, NuttyTag, MintyTag,
                GreenFieldTag, YorkshireTag, RimiTag, DilmahTag, AhmadTeaTag,
                HerbaTag, DanyunTag, LiptonTag, TeekanneTag, ClipperTag);
        }
    }

    private static void SeedAppDataIngredientType(ApplicationDbContext context)
    {
        if (!context.IngredientTypes.Any())
        {
            context.IngredientTypes.AddRange(SpiceIngredientType, FruitIngredientType, SyrupIngredientType,
                LiquidIngredientType, SolidIngredientType);
        }
    }
    
    private static void SeedAppDataIngredient(ApplicationDbContext context)
    {
        if (!context.Ingredients.Any())
        {
            context.Ingredients.AddRange(MilkIngredient, HoneyIngredient, SugarIngredient,
                CitrusIngredient, CaramelSyrupIngredient);
        }
    }
    
    private static void SeedAppDataComments(ApplicationDbContext context)
    {
        if (!context.Comments.Any())
        {
            context.Comments.AddRange(YorkshireBiscuitReview1Comment, ClippersSleepReviewComment);
        }
    }
    
    private static void SeedAppDataReviews(ApplicationDbContext context)
    {
        if (!context.Reviews.Any())
        {
            context.Reviews.AddRange(YorkshireBiscuitReview1, YorkshireBiscuitReview2,
                YorkshireReview, ClippersSleepReview);
        }
    }

    private static void SeedAppDataPictures(ApplicationDbContext context)
    {
        if (!context.Pictures.Any())
        {
            context.Pictures.AddRange(YorkshireBiscuitPicture, YorkshirePicture, YorkshireGoldPicture,
                ClipperSnorePeacePicture, AhmadTeaGreenPicture,
                RimiGunpowderPicture, RimiEarlGrayPicture,
                HerbaKamillePicture, HerbaMintPicture);
        }
    }
    
    private static void SeedAppDataBeverages(ApplicationDbContext context)
    {
        if (!context.Beverages.Any())
        {
            context.Beverages.AddRange(YorkshireBiscuitBeverage, YorkshireBeverage, YorkshireGoldBeverage,
                ClipperSnorePeaceBeverage, AhmadTeaGreenBeverage,
                RimiGunpowderBeverage, RimiEarlGrayBeverage,
                HerbaKamilleBeverage, HerbaMintBeverage);
        }
    }

    private static void SeedAppDataBeverageTags(ApplicationDbContext context)
    {
        if (!context.BeverageTags.Any())
        {
            context.BeverageTags.AddRange(
            new BeverageTag(){BeverageId = YorkshireBiscuitBeverageId, TagId = YorkshireTagId},
            new BeverageTag(){BeverageId = YorkshireBiscuitBeverageId, TagId = BitterTagId},
            new BeverageTag(){BeverageId = YorkshireBiscuitBeverageId, TagId = SweetTagId},
            new BeverageTag(){BeverageId = YorkshireBiscuitBeverageId, TagId = FlavouredTagId},
            new BeverageTag(){BeverageId = YorkshireBiscuitBeverageId, TagId = BlackTeaTagId},
            new BeverageTag(){BeverageId = YorkshireBeverageId, TagId = YorkshireTagId},
            new BeverageTag(){BeverageId = YorkshireBeverageId, TagId = BitterTagId},
            new BeverageTag(){BeverageId = YorkshireBeverageId, TagId = BlackTeaTagId},
            new BeverageTag(){BeverageId = YorkshireGoldBeverageId, TagId = YorkshireTagId},
            new BeverageTag(){BeverageId = YorkshireGoldBeverageId, TagId = BitterTagId},
            new BeverageTag(){BeverageId = YorkshireGoldBeverageId, TagId = BlackTeaTagId},
            new BeverageTag(){BeverageId = ClipperSnorePeaceBeverageId, TagId = ClipperTagId},
            new BeverageTag(){BeverageId = ClipperSnorePeaceBeverageId, TagId = HerbalTagId},
            new BeverageTag(){BeverageId = RimiGunpowderBeverageId, TagId = RimiTagId},
            new BeverageTag(){BeverageId = RimiGunpowderBeverageId, TagId = GreenTeaTagId},
            new BeverageTag(){BeverageId = RimiGunpowderBeverageId, TagId = NuttyTagId},
            new BeverageTag(){BeverageId = RimiEarlGrayBeverageId, TagId = RimiTagId},
            new BeverageTag(){BeverageId = RimiEarlGrayBeverageId, TagId = BlackTeaTagId},
            new BeverageTag(){BeverageId = RimiEarlGrayBeverageId, TagId = FlavouredTagId},
            new BeverageTag(){BeverageId = AhmadTeaGreenBeverageId, TagId = AhmadTeaTagId},
            new BeverageTag(){BeverageId = AhmadTeaGreenBeverageId, TagId = GreenTeaTagId},
            new BeverageTag(){BeverageId = HerbaKamilleBeverageId, TagId = HerbaTagId},
            new BeverageTag(){BeverageId = HerbaKamilleBeverageId, TagId = HerbalTagId},
            new BeverageTag(){BeverageId = HerbaMintBeverageId, TagId = HerbaTagId},
            new BeverageTag(){BeverageId = HerbaMintBeverageId, TagId = HerbalTagId},
            new BeverageTag(){BeverageId = HerbaMintBeverageId, TagId = MintyTagId}
            );
        }
    }
}