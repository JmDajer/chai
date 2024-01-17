using DAL.Contracts.Base;
using DAL.DTO;
using Domain.App;

namespace DAL.Contracts.App;

public interface IPictureRepository : IBaseRepository<DalPicture>, IPictureRepositoryCustom<DalPicture>
{
    public Task<IEnumerable<DalPicture>> GetBeveragePicturesAsync(Guid beverageId);
}

public interface IPictureRepositoryCustom<TEntity>
{
    // Custom shared methods for Repo and Service
}