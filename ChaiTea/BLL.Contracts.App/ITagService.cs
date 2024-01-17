using BLL.DTO;
using DAL.Contracts.App;
using DAL.Contracts.Base;

namespace BLL.Contracts.App;

public interface ITagService : IBaseRepository<BllTag>, ITagRepositoryCustom<BllTag>
{
    // Custom methods for service
}