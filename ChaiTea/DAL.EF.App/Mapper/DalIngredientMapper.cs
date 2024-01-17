using AutoMapper;
using DAL.Base;
using DAL.DTO;
using Domain.App;

namespace DAL.EF.App.Mapper;

public class DalIngredientMapper : BaseMapper<Ingredient, DalIngredient>
{
    public DalIngredientMapper(IMapper mapper) : base(mapper)
    {
    }
}