using AutoMapper;
using DAL.DTO;

namespace DAL.EF.App;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<DalBeverage, Domain.App.Beverage>().ReverseMap();
        CreateMap<DalReview, Domain.App.Review>().ReverseMap();
        CreateMap<DalComment, Domain.App.Comment>().ReverseMap();
        CreateMap<DalPicture, Domain.App.Picture>().ReverseMap();
        CreateMap<DalTag, Domain.App.Tag>().ReverseMap();
        CreateMap<DalTagType, Domain.App.TagType>().ReverseMap();
        CreateMap<DalIngredient, Domain.App.Ingredient>().ReverseMap();
        CreateMap<DalIngredientType, Domain.App.IngredientType>().ReverseMap();
    }
}