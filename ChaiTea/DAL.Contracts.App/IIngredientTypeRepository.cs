using DAL.Contracts.Base;
using DAL.DTO;
using Domain.App;

namespace DAL.Contracts.App;

public interface IIngredientTypeRepository : IBaseRepository<DalIngredientType>, IIngredientTypeRepositoryCustom<DalIngredientType>
{
}

public interface IIngredientTypeRepositoryCustom<TEntity>
{
    // Custom shared methods for Repo and Service
}