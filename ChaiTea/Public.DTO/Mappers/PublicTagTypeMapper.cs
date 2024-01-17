using AutoMapper;
using BLL.DTO;
using DAL.Base;
using Public.DTO.v1;

namespace Public.DTO.Mappers;

public class PublicTagTypeMapper : BaseMapper<BllTagType, PublicTagType>
{
    public PublicTagTypeMapper(IMapper mapper) : base(mapper)
    {
    }
}