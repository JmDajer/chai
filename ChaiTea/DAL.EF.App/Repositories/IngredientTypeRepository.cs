using Contracts.Base;
using DAL.Contracts.App;
using DAL.DTO;
using DAL.EF.Base;
using Domain.App;

namespace DAL.EF.App.Repositories;

public class IngredientTypeRepository :
    EfBaseRepository<IngredientType, DalIngredientType, ApplicationDbContext>,
    IIngredientTypeRepository
{
    public IngredientTypeRepository(
        ApplicationDbContext dataContext, IMapper<IngredientType, DalIngredientType> mapper) :
        base(dataContext, mapper)
    {
    }
}