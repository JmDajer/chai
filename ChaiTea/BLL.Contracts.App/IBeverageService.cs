using BLL.DTO;
using DAL.Contracts.App;
using DAL.Contracts.Base;

namespace BLL.Contracts.App;

public interface IBeverageService : IBaseRepository<BllBeverage>, IBeverageRepositoryCustom<BllBeverage>
{
    // Custom methods for service
    public Task<IEnumerable<BllBeverage>> GetUserRecommendedBeverages(Guid userId);
}