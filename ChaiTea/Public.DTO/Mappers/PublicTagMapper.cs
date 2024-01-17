using AutoMapper;
using BLL.DTO;
using DAL.Base;
using Public.DTO.v1;

namespace Public.DTO.Mappers;

public class PublicTagMapper : BaseMapper<BllTag, PublicTag>
{
    public PublicTagMapper(IMapper mapper) : base(mapper)
    {
    }
}