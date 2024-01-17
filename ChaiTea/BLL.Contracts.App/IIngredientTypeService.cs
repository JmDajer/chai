using BLL.DTO;
using DAL.Contracts.App;
using DAL.Contracts.Base;

namespace BLL.Contracts.App;

public interface IIngredientTypeService : IBaseRepository<BllIngredientType>, IIngredientTypeRepositoryCustom<BllIngredientType>
{
    // Custom methods for service
}