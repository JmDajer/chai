using BLL.Base;
using BLL.Contracts.App;
using BLL.DTO;
using Contracts.Base;
using DAL.Contracts.App;
using DAL.DTO;

namespace BLL.App.Services;

public class IngredientTypeService : BaseEntityService<BllIngredientType, DalIngredientType, IIngredientTypeRepository>,
    IIngredientTypeService
{
    protected IAppUow Uow;
    
    public IngredientTypeService(IAppUow uow, IMapper<DalIngredientType, BllIngredientType> mapper) :
        base(uow.IngredientTypeRepository, mapper)
    {
        Uow = uow;
    }
}