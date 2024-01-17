using DAL.Contracts.Base;
using DAL.DTO;
using Domain.App;

namespace DAL.Contracts.App;


public interface ITagRepository : IBaseRepository<DalTag>, ITagRepositoryCustom<DalTag>
{
    Task<IEnumerable<DalTag>> GetBeverageTagsAsync(Guid beverageId);
}

public interface ITagRepositoryCustom<TEntity>
{
    // Custom shared methods for Repo and Service
    
}