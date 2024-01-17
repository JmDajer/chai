using AutoMapper;
using BLL.DTO;
using DAL.DTO;
using Domain.App;

namespace BLL.App;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<BllBeverage, DalBeverage>();
        CreateMap<DalBeverage, BllBeverage>()
            .ForMember(dest => dest.ParentBeverages, 
                o => o.MapFrom(src => src.ParentBeverages!.Select(
                    beverage => new BllBeverage()
                {
                    AppUserId = beverage.AppUserId,
                    Id = beverage.Id,
                    Description = beverage.Description,
                    Name = beverage.Name
                })))
            .ForMember(dest => dest.Ingredients,
                o => o.MapFrom(src => src.Ingredients!.Select(
                    ingredient => new BllIngredient()
                    {
                        Id = ingredient.Id,
                        IngredientTypeId = ingredient.IngredientTypeId,
                        Name = ingredient.Name
                    })));
        CreateMap<BllReview, DalReview>().ReverseMap();
        CreateMap<BllComment, DalComment>().ReverseMap();
        CreateMap<BllPicture, DalPicture>().ReverseMap();
        CreateMap<BllTag, DalTag>().ReverseMap();
        CreateMap<BllTagType, DalTagType>().ReverseMap();
        CreateMap<BllIngredient, DalIngredient>().ReverseMap();
        CreateMap<BllIngredientType, DalIngredientType>().ReverseMap();
    }
}