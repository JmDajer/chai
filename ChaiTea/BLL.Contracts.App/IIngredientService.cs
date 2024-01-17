using BLL.DTO;
using DAL.Contracts.App;
using DAL.Contracts.Base;

namespace BLL.Contracts.App;

public interface IIngredientService : IBaseRepository<BllIngredient>, IIngredientRepositoryCustom<BllIngredient>
{
    // Custom methods for service
}