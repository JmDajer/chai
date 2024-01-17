using Contracts.Base;
using DAL.Contracts.App;
using DAL.DTO;
using DAL.EF.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class TagTypeRepository : EfBaseRepository<TagType, DalTagType, ApplicationDbContext>, ITagTypeRepository
{
    public TagTypeRepository(
        ApplicationDbContext dataContext, IMapper<TagType, DalTagType> mapper) :
        base(dataContext, mapper)
    {
    }

    /// <summary>
    /// Get tags tag type.
    /// </summary>
    /// <param name="tagId"></param>
    /// <returns>A <c>DalTagType</c> object.</returns>
    public async Task<DalTagType> GetTagsTypeAsync(Guid tagId)
    {
        return Mapper.Map(await RepositoryDbSet
            .FirstOrDefaultAsync(tt => tt.Tags
                .Any(t => t.Id == tagId)))!;
    }
}