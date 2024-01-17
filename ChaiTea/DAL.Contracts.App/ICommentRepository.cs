using DAL.Contracts.Base;
using DAL.DTO;

namespace DAL.Contracts.App;

public interface ICommentRepository : IBaseRepository<DalComment>, ICommentRepositoryCustom<DalComment>
{
}

public interface ICommentRepositoryCustom<TEntity>
{
    // Custom shared methods for Repo and Service
    public Task<IEnumerable<TEntity>> GetReviewsComments(Guid reviewId);
}