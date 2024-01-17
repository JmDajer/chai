using Contracts.Base;
using DAL.Contracts.App;
using DAL.DTO;
using DAL.EF.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class ReviewRepository : EfBaseRepository<Review, DalReview, ApplicationDbContext>, IReviewRepository
{
    public ReviewRepository(
        ApplicationDbContext dataContext, IMapper<Review, DalReview> mapper) :
        base(dataContext, mapper)
    {
    }
    
    /// <summary>
    /// Get a list of reviews for a beverage.
    /// </summary>
    /// <param name="beverageId">Beverage id.</param>
    /// <returns></returns>
    public async Task<IEnumerable<DalReview>> GetBeverageReviewsAsync(Guid beverageId)
    {
        return await RepositoryDbSet
            .Include(r => r.Comments)
            .Where(r => r.Beverage.Id == beverageId)
            .Select(r => Mapper.Map(r)!)
            .ToListAsync();
    }

    /// <summary>
    /// Get all of the users reviews.
    /// </summary>
    /// <param name="userId"></param>
    /// <returns>A list of <c>DalReview</c> objects.</returns>
    public async Task<IEnumerable<DalReview>> GetUsersReviewsAsync(Guid userId)
    {
        return await RepositoryDbSet
            .Include(r => r.AppUser)
            .Include(r => r.Comments)
            .Where(r => r.AppUserId == userId)
            .Select(r => Mapper.Map(r)!)
            .ToListAsync();
    }
}