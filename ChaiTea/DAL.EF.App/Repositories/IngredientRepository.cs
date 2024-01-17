using Contracts.Base;
using DAL.Contracts.App;
using DAL.DTO;
using DAL.EF.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class IngredientRepository :
    EfBaseRepository<Ingredient, DalIngredient, ApplicationDbContext>,
    IIngredientRepository
{
    public IngredientRepository(
        ApplicationDbContext dataContext, IMapper<Ingredient, DalIngredient> mapper) :
        base(dataContext, mapper)
    {
    }

    public async override Task<IEnumerable<DalIngredient>> AllAsync()
    {
        return await RepositoryDbContext.Ingredients
            .AsNoTracking()
            .Include(i => i.IngredientType)
            .Select(i => Mapper.Map(i)!)
            .ToListAsync();
    }
}