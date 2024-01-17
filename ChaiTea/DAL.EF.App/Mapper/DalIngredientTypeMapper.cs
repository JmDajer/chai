using AutoMapper;
using DAL.Base;
using DAL.DTO;
using Domain.App;

namespace DAL.EF.App.Mapper;

public class DalIngredientTypeMapper : BaseMapper<IngredientType, DalIngredientType>
{
    public DalIngredientTypeMapper(IMapper mapper) : base(mapper)
    {
    }
}