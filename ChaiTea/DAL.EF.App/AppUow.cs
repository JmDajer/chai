using AutoMapper;
using DAL.Contracts.App;
using DAL.EF.App.Mapper;
using DAL.EF.App.Repositories;
using DAL.EF.Base;

namespace DAL.EF.App;

public class AppUow : EfBaseUow<ApplicationDbContext>, IAppUow
{
    private readonly AutoMapper.IMapper _mapper;
    
    public AppUow(ApplicationDbContext dataContext, IMapper mapper) : base(dataContext)
    {
        _mapper = mapper;
    }
    
    private IBeverageRepository? _beverageRepository;
    private IIngredientRepository? _ingredientRepository;
    private IIngredientTypeRepository? _ingredientTypeRepository;
    private IPictureRepository? _pictureRepository;
    private IReviewRepository? _reviewRepository;
    private ICommentRepository? _commentRepository;
    private ITagRepository? _tagRepository;
    private ITagTypeRepository? _tagTypeRepository;

    public IBeverageRepository BeverageRepository =>
        _beverageRepository ??= new BeverageRepository(UowDbContext, new DalBeverageMapper(_mapper));
    
    public ICommentRepository CommentRepository =>
        _commentRepository ??= new CommentRepository(UowDbContext, new DalCommentMapper(_mapper));

    public IIngredientRepository IngredientRepository =>
        _ingredientRepository ??= new IngredientRepository(UowDbContext, new DalIngredientMapper(_mapper));

    public IIngredientTypeRepository IngredientTypeRepository =>
        _ingredientTypeRepository ??= new IngredientTypeRepository(UowDbContext, new DalIngredientTypeMapper(_mapper));

    public IPictureRepository PictureRepository => 
        _pictureRepository ??= new PictureRepository(UowDbContext, new DalPictureMapper(_mapper));

    public IReviewRepository ReviewRepository => 
        _reviewRepository ??= new ReviewRepository(UowDbContext, new DalReviewMapper(_mapper));

    public ITagRepository TagRepository => 
        _tagRepository ??= new TagRepository(UowDbContext, new DalTagMapper(_mapper));

    public ITagTypeRepository TagTypeRepository => 
        _tagTypeRepository ??= new TagTypeRepository(UowDbContext, new DalTagTypeMapper(_mapper));
}