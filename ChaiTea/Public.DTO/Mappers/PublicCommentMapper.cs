using AutoMapper;
using BLL.DTO;
using DAL.Base;
using Public.DTO.v1;

namespace Public.DTO.Mappers;

public class PublicCommentMapper : BaseMapper<BllComment, PublicComment>
{
    public PublicCommentMapper(IMapper mapper) : base(mapper)
    {
    }
}