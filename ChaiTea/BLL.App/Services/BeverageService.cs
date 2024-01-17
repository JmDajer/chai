using BLL.Base;
using BLL.Contracts.App;
using BLL.DTO;
using Contracts.Base;
using DAL.Contracts.App;
using DAL.DTO;

namespace BLL.App.Services;

public class BeverageService : 
    BaseEntityService<BllBeverage, DalBeverage, IBeverageRepository>,
    IBeverageService
{
    protected readonly IAppUow Uow;

    public BeverageService(IAppUow uow, IMapper<DalBeverage, BllBeverage> mapper) : 
        base(uow.BeverageRepository, mapper)
    {
        Uow = uow;
    }

    /// <summary>
    /// Get list of recommended beverages for the user!
    /// </summary>
    /// <param name="userId"></param>
    /// <returns>A list of recommended beverages.</returns>
    public async Task<IEnumerable<BllBeverage>> GetUserRecommendedBeverages(Guid userId)
    {
        var beverages = Repository.AllAsync();
        
        var userRatedBeverages = beverages.Result.
            Where(b => b.Reviews.Any(r => r.AppUserId == userId));

        var userPositivelyRatedBeverages = userRatedBeverages
            .Where(b => b.Reviews.Any(r => r.Rating > 3));
        
        var userNegativelyRatedBeverages = userRatedBeverages
            .Where(b => b.Reviews.Any(r => r.Rating < 3));

        var userPositivelyRatedBeveragesTags = new HashSet<string>();
        var userNegativelyRatedBeveragesTags = new HashSet<string>();
        
        foreach (var beverage in userPositivelyRatedBeverages)
        {
            var tags = await Uow.TagRepository.GetBeverageTagsAsync(beverage.Id);

            foreach (var tag in tags)
            {
                if (userPositivelyRatedBeveragesTags.Contains(tag.Name)) continue;
                userPositivelyRatedBeveragesTags.Add(tag.Name);
            }
        }
        
        foreach (var beverage in userNegativelyRatedBeverages)
        {
            var tags = await Uow.TagRepository.GetBeverageTagsAsync(beverage.Id);

            foreach (var tag in tags)
            {
                if (userNegativelyRatedBeveragesTags.Contains(tag.Name)) continue;
                userNegativelyRatedBeveragesTags.Add(tag.Name);
            }
        }

        var userRatedBeveragesTags = new HashSet<string>(
            userPositivelyRatedBeveragesTags.Except(userNegativelyRatedBeveragesTags)
            );
        
        var recommendedBeverages = new HashSet<BllBeverage>();
        var recommendedBeverageIds = new HashSet<Guid>();
        foreach (var tagName in userRatedBeveragesTags)
        {
            var currentBeverages = beverages.Result
                .Where(b => b.Tags.Any(t => t.Name == tagName));
            
            foreach (var beverage in currentBeverages)
            {
                if (recommendedBeverageIds.Contains(beverage.Id)) continue;
                recommendedBeverageIds.Add(beverage.Id);
                recommendedBeverages.Add(Mapper.Map(beverage)!);
            }
        }

        return recommendedBeverages;
    }
    
    /// <summary>
    /// Get user beverages
    /// </summary>
    /// <param name="userId"></param>
    /// <returns>Custom created beverage for the user</returns>
    public async Task<IEnumerable<BllBeverage>> GetUserBeverages(Guid userId)
    {
        var userBeverages = await Repository.GetUserBeverages(userId);
        return userBeverages.Select(b => Mapper.Map(b)!);
    }
}