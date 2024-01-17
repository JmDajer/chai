using AutoMapper;
using DAL.Base;
using DAL.DTO;
using Domain.App;

namespace DAL.EF.App.Mapper;

public class DalCommentMapper : BaseMapper<Comment, DalComment>
{
    public DalCommentMapper(IMapper mapper) : base(mapper)
    {
    }
}