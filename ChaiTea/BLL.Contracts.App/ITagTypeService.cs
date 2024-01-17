using BLL.DTO;
using DAL.Contracts.App;
using DAL.Contracts.Base;

namespace BLL.Contracts.App;

public interface ITagTypeService : IBaseRepository<BllTagType>, ITagRepositoryCustom<BllTagType>
{
    // Custom methods for service
}