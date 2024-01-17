using AutoMapper;
using BLL.DTO;
using DAL.Base;
using Public.DTO.v1;

namespace Public.DTO.Mappers;

public class PublicIngredientMapper : BaseMapper<BllIngredient, PublicIngredient>
{
    public PublicIngredientMapper(IMapper mapper) : base(mapper)
    {
    }
}