using BLL.DTO;
using DAL.Contracts.App;
using DAL.Contracts.Base;

namespace BLL.Contracts.App;

public interface IPictureService : IBaseRepository<BllPicture>, ITagRepositoryCustom<BllPicture>
{
    // Custom methods for service
}