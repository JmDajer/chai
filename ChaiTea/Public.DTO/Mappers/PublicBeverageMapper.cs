using AutoMapper;
using BLL.DTO;
using DAL.Base;
using Public.DTO.v1;

namespace Public.DTO.Mappers;

public class PublicBeverageMapper : BaseMapper<BllBeverage, PublicBeverage>
{
    public PublicBeverageMapper(IMapper mapper) : base(mapper)
    {
    }
}