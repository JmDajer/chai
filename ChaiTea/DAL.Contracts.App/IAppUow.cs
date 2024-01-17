using DAL.Contracts.Base;

namespace DAL.Contracts.App;

public interface IAppUow : IBaseUow
{
    IBeverageRepository BeverageRepository { get; }
    IPictureRepository PictureRepository { get; }
    ICommentRepository CommentRepository { get; }
    IReviewRepository ReviewRepository { get; }
    IIngredientRepository IngredientRepository { get; }
    IIngredientTypeRepository IngredientTypeRepository { get; }
    ITagRepository TagRepository { get; }
    ITagTypeRepository TagTypeRepository { get; }
}