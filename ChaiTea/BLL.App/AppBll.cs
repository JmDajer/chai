using AutoMapper;
using BLL.App.Mappers;
using BLL.App.Services;
using BLL.Base;
using BLL.Contracts.App;
using DAL.Contracts.App;

namespace BLL.App;

public class AppBll : BaseBll<IAppUow>, IAppBll
{
    protected IAppUow Uow;
    private readonly IMapper _mapper;
    
    public AppBll(IAppUow uow, IMapper mapper) : base(uow)
    {
        Uow = uow;
        _mapper = mapper;
    }

    private IBeverageService? _beverageService;
    private IPictureService? _pictureService;
    private IReviewService? _reviewService;
    private ICommentService? _commentService;
    private IIngredientService? _ingredientService;
    private IIngredientTypeService? _ingredientTypeService;
    private ITagService? _tagService;
    private ITagTypeService? _tagTypeService;

    public IBeverageService BeverageService => 
        _beverageService ??= new BeverageService(Uow, new BllBeverageMapper(_mapper));

    public IPictureService PictureService =>
        _pictureService ??= new PictureService(Uow, new BllPictureMapper(_mapper));
    
    public IReviewService ReviewService =>
        _reviewService ??= new ReviewService(Uow, new BllReviewMapper(_mapper));

    public ICommentService CommentService =>
        _commentService ??= new CommentService(Uow, new BllCommentMapper(_mapper));

    public IIngredientService IngredientService =>
        _ingredientService ??= new IngredientService(Uow, new BllIngredientMapper(_mapper));

    public IIngredientTypeService IngredientTypeService => _ingredientTypeService ??=
        new IngredientTypeService(Uow, new BllIngredientTypeMapper(_mapper));

    public ITagService TagService => 
        _tagService ??= new TagService(Uow, new BllTagMapper(_mapper));

    public ITagTypeService TagTypeService =>
        _tagTypeService ??= new TagTypeService(Uow, new BllTagTypeMapper(_mapper));
}