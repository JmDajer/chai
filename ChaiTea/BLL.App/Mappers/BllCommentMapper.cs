using AutoMapper;
using BLL.DTO;
using DAL.Base;
using DAL.DTO;

namespace BLL.App.Mappers;

public class BllCommentMapper : BaseMapper<DalComment, BllComment>
{
    public BllCommentMapper(IMapper mapper) : base(mapper)
    {
    }
}