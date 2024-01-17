using BLL.DTO;
using DAL.Contracts.App;
using DAL.Contracts.Base;

namespace BLL.Contracts.App;

public interface IReviewService : IBaseRepository<BllReview>, IReviewRepositoryCustom<BllReview>
{
    // Custom methods for ReviewService
}