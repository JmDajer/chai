using AutoMapper;
using BLL.DTO;
using Public.DTO.v1;

namespace Public.DTO;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<BllBeverage, PublicBeverage>().ReverseMap();
        CreateMap<BllComment, PublicComment>().ReverseMap();
        CreateMap<BllReview, PublicReview>().ReverseMap();
        CreateMap<BllPicture, PublicPicture>().ReverseMap();
        CreateMap<BllIngredient, PublicIngredient>().ReverseMap();
        CreateMap<BllIngredientType, PublicIngredientType>().ReverseMap();
        CreateMap<BllTag, PublicTag>().ReverseMap();
        CreateMap<BllTagType, PublicTagType>().ReverseMap();
    }
    
}