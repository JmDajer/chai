using BLL.Base;
using BLL.Contracts.App;
using BLL.DTO;
using Contracts.Base;
using DAL.Contracts.App;
using DAL.DTO;

namespace BLL.App.Services;

public class ReviewService : 
    BaseEntityService<BllReview, DalReview, IReviewRepository>,
    IReviewService
{
    protected IAppUow Uow;
     
    public ReviewService(IAppUow uow, IMapper<DalReview, BllReview> mapper) : 
        base(uow.ReviewRepository, mapper)
    {
        Uow = uow;
    }

    public async Task<IEnumerable<BllReview>> GetBeverageReviewsAsync(Guid beverageId)
    {
        var dalReviews = await Repository.GetBeverageReviewsAsync(beverageId);
        return dalReviews.Select(r => Mapper.Map(r)!);
    }

    public Task<IEnumerable<BllReview>> GetUsersReviewsAsync(Guid userId)
    {
        throw new NotImplementedException();
    }
}