using BLL.Contracts.Base;

namespace BLL.Contracts.App;

public interface IAppBll : IBaseBll
{
    IBeverageService BeverageService { get; }
    IPictureService PictureService { get; }
    IReviewService ReviewService { get; }
    ICommentService CommentService { get; }
    IIngredientService IngredientService { get; }
    IIngredientTypeService IngredientTypeService { get; }
    ITagService TagService { get; }
    ITagTypeService TagTypeService { get; }
}