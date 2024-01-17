using AutoMapper;
using BLL.DTO;
using DAL.Base;
using DAL.DTO;

namespace BLL.App.Mappers;

public class BllIngredientTypeMapper :  BaseMapper<DalIngredientType, BllIngredientType>
{
    public BllIngredientTypeMapper(IMapper mapper) : base(mapper)
    {
    }
}