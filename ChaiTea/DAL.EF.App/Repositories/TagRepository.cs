using Contracts.Base;
using DAL.Contracts.App;
using DAL.DTO;
using DAL.EF.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class TagRepository : EfBaseRepository<Tag, DalTag, ApplicationDbContext>, ITagRepository
{
    public TagRepository(
        ApplicationDbContext dataContext, IMapper<Tag, DalTag> mapper) :
        base(dataContext, mapper)
    {
    }

    public override async Task<IEnumerable<DalTag>> AllAsync()
    {
        var existingTags = await RepositoryDbContext.Tags.ToListAsync();
        return existingTags
            .Select(t => Mapper.Map(t)!)
            .ToList();
    }

    public async Task<IEnumerable<DalTag>> GetBeverageTagsAsync(Guid beverageId)
    {
        return await RepositoryDbSet
            .Where(t => t.Beverages
                .Any(b => b.Id == beverageId))
            .Select(t => Mapper.Map(t)!)
            .ToListAsync();
    }
}