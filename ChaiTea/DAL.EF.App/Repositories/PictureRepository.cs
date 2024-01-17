using Contracts.Base;
using DAL.Contracts.App;
using DAL.DTO;
using DAL.EF.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class PictureRepository : EfBaseRepository<Picture, DalPicture, ApplicationDbContext>, IPictureRepository
{
    public PictureRepository(
        ApplicationDbContext dataContext, IMapper<Picture, DalPicture> mapper) :
        base(dataContext, mapper)
    {
    }

    /// <summary>
    /// Get a list of pictures that have the beverageId.
    /// </summary>
    /// <param name="beverageId"></param>
    /// <returns>A list of <c>DalPicture</c> objects.</returns>
    public async Task<IEnumerable<DalPicture>> GetBeveragePicturesAsync(Guid beverageId)
    {
        return await RepositoryDbSet
            .Where(p => p.BeverageId == beverageId)
            .Select(p => Mapper.Map(p)!)
            .ToListAsync();
    }
}