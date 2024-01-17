using AutoMapper;
using BLL.DTO;
using DAL.Base;
using DAL.DTO;

namespace BLL.App.Mappers;

public class BllIngredientMapper :  BaseMapper<DalIngredient, BllIngredient>
{
    public BllIngredientMapper(IMapper mapper) : base(mapper)
    {
    }
}