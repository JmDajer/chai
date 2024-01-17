using DAL.Contracts.Base;
using DAL.DTO;

namespace DAL.Contracts.App;

public interface IBeverageRepository : IBaseRepository<DalBeverage>, IBeverageRepositoryCustom<DalBeverage>
{
    Task<IEnumerable<DalBeverage>> GetBeveragesByTagAsync(Guid tagId);
}

public interface IBeverageRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetUserBeverages(Guid userId);
    // Custom shared methods for Repo and Service
}