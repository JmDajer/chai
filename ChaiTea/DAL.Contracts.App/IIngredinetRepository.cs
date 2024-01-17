using DAL.Contracts.Base;
using DAL.DTO;
using Domain.App;

namespace DAL.Contracts.App;

public interface IIngredientRepository :
    IBaseRepository<DalIngredient>,
    IIngredientRepositoryCustom<DalIngredient>
{
}

public interface IIngredientRepositoryCustom<TEntity>
{
    // Custom shared methods for Repo and Service
}