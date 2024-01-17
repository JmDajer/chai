using DAL.Contracts.Base;
using DAL.DTO;

namespace DAL.Contracts.App;

public interface IReviewRepository : IBaseRepository<DalReview>, IReviewRepositoryCustom<DalReview>
{
}

public interface IReviewRepositoryCustom<TEntity>
{
    // Custom shared methods for Repo and Service
    public Task<IEnumerable<TEntity>> GetBeverageReviewsAsync(Guid beverageId);
    public Task<IEnumerable<TEntity>> GetUsersReviewsAsync(Guid userId);
}