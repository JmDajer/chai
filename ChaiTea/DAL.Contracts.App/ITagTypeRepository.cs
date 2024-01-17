using DAL.Contracts.Base;
using DAL.DTO;
using Domain.App;

namespace DAL.Contracts.App;

public interface ITagTypeRepository : IBaseRepository<DalTagType>, ITagTypeRepositoryCustom<DalTagType>
{
    public Task<DalTagType> GetTagsTypeAsync(Guid tagId);
}

public interface ITagTypeRepositoryCustom<TEntity>
{
    // Custom shared methods for Repo and Service
}