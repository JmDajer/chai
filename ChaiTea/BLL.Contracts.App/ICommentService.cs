using System.Security.Claims;
using BLL.DTO;
using DAL.Contracts.App;
using DAL.Contracts.Base;

namespace BLL.Contracts.App;

public interface ICommentService : IBaseRepository<BllComment>, ICommentRepositoryCustom<BllComment>
{
    // Custom methods for service
}