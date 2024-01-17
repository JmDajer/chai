using BLL.Base;
using BLL.Contracts.App;
using BLL.DTO;
using Contracts.Base;
using DAL.Contracts.App;
using DAL.DTO;

namespace BLL.App.Services;

public class IngredientService : BaseEntityService<BllIngredient, DalIngredient, IIngredientRepository>,
    IIngredientService
{
    protected IAppUow Uow;
    
    public IngredientService(IAppUow uow, IMapper<DalIngredient, BllIngredient> mapper) :
        base(uow.IngredientRepository, mapper)
    {
        Uow = uow;
    }
}