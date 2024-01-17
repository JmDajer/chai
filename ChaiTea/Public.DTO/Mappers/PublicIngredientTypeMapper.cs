using AutoMapper;
using BLL.DTO;
using DAL.Base;
using Public.DTO.v1;

namespace Public.DTO.Mappers;

public class PublicIngredientTypeMapper : BaseMapper<BllIngredientType, PublicIngredientType>
{
    public PublicIngredientTypeMapper(IMapper mapper) : base(mapper)
    {
    }
}