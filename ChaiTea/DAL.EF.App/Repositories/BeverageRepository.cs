using System.ComponentModel.DataAnnotations;
using Contracts.Base;
using DAL.Contracts.App;
using DAL.DTO;
using DAL.EF.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;

namespace DAL.EF.App.Repositories;

public class BeverageRepository : EfBaseRepository<Beverage, DalBeverage, ApplicationDbContext>, IBeverageRepository
{
    public BeverageRepository(
        ApplicationDbContext dataContext, IMapper<Beverage, DalBeverage> mapper) :
        base(dataContext, mapper)
    {
    }

    /// <summary>
    /// Admin Id that is used to determine the main beverages that every user should see.
    /// </summary>
    private static readonly Guid AdminId = Guid.Parse("69a435c1-9d50-4d38-af58-c9406356efad");
    
    
    /// <summary>
    /// Gets a list of beverages that the admin account has added for the users to use.
    /// </summary>
    /// <returns>A list of <c>DalBeverage</c> objects.</returns>
    public override async Task<IEnumerable<DalBeverage>> AllAsync()
    {
        return await RepositoryDbContext.Beverages
            .AsNoTracking()
            .Where(b => b.AppUserId == AdminId)
            .Include(b => b.Tags)
            .Include(b => b.Reviews)
            .Include(b => b.Pictures)
            .Select(b => Mapper.Map(b)!)
            .ToListAsync();
    }

    public override DalBeverage Add(DalBeverage entity)
    {
        var tags = entity.Tags!.Select(t => t.Id).ToList();
        var ingredients = entity.Ingredients!.Select(i => i.Id).ToList();
        var parentBeverages = entity.ParentBeverages!.Select(i => i.Id).ToList();
        entity.Tags!.Clear();
        entity.Ingredients!.Clear();
        entity.ParentBeverages!.Clear();
        var beverage = Mapper.Map(entity)!;
        if (beverage.Tags != null && beverage.Ingredients != null && beverage.ParentBeverages != null)
        {
            var dbIngredients = RepositoryDbContext.Ingredients.Where(i => ingredients.Contains(i.Id)).ToList();
            var dbTags = RepositoryDbContext.Tags.Where(t => tags.Contains(t.Id)).ToList();
            var dbParentBeverages = RepositoryDbContext.Beverages.Where(b => parentBeverages.Contains(b.Id)).ToList();
            beverage.Tags.AddRange(dbTags);
            beverage.Ingredients.AddRange(dbIngredients);
            beverage.ParentBeverages.AddRange(dbParentBeverages);
        }
        
        return Mapper.Map(RepositoryDbContext.Beverages.Add(beverage!).Entity)!;
    }

    /// <summary>
    /// Gets a beverage by it's given ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>A <c>DalBeverage</c> object.</returns>
    public override async Task<DalBeverage?> FindAsync(Guid id)
    {
        var beverage = await RepositoryDbSet
            .Include(b => b.Tags)
            .Include(b => b.Pictures)
            .Include(b => b.Reviews)
            .Include(b => b.Ingredients)
            .FirstOrDefaultAsync(b => b.Id == id);
        
        return Mapper.Map(beverage);
    }

    public override DalBeverage? Update(DalBeverage entity)
    {
        var domainEntity = RepositoryDbSet.FirstOrDefault(x => x.Id == entity.Id);
        
        if (domainEntity != null)
        {
            domainEntity.Name = entity.Name;
            domainEntity.Upc = entity.Upc;
            domainEntity.Description = entity.Description;
            
            RepositoryDbSet.UpdateRange(domainEntity);
            RepositoryDbContext.SaveChanges();

            return Mapper.Map(domainEntity);
        }

        // Handle the case when the entity is not found

        return null;
    }

    public async override Task RemoveAsync(Guid id)
    {
        var entity = await RepositoryDbSet.Include(b => b.Tags)
            .FirstOrDefaultAsync(t => t.Id.Equals(id));
        if (entity != null)
        {
            RepositoryDbSet.Remove(entity);
        }

        await RepositoryDbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Get a list of beverages that the user has created.
    /// </summary>
    /// <param name="userId"></param>
    /// <returns>A list of <c>DalBeverage</c> objects by userId.</returns>
    public async Task<IEnumerable<DalBeverage>> GetUserBeverages(Guid userId)
    {
        return await RepositoryDbSet
            .Include(b => b.Tags)
            .Include(b => b.Ingredients)
            .Include(b => b.SubBeverages)
            .Include(b => b.Pictures)
            .Include(b => b.Reviews)
            .Where(b => b.AppUserId == userId)
            .Select(b => Mapper.Map(b)!)
            .ToListAsync();
    }

    /// <summary>
    /// Get beverages by tags.
    /// </summary>
    /// <param name="tagId">Tag id to get Beverages by.</param>
    /// <returns></returns>
    public async Task<IEnumerable<DalBeverage>> GetBeveragesByTagAsync(Guid tagId)
    {
        return await RepositoryDbSet
            .AsNoTracking()
            .Include(b => b.Tags)
            .Include(b => b.Pictures)
            .Include(b => b.Reviews)
            .Where(b => b.Tags
                .Any(t => t.Id == tagId))
            .Select(b => Mapper.Map(b))
            .ToListAsync();
    }
}