using AutoMapper;
using DAL.Base;
using DAL.DTO;
using Domain.App;

namespace DAL.EF.App.Mapper;

public class DalTagMapper : BaseMapper<Tag, DalTag>
{
    public DalTagMapper(IMapper mapper) : base(mapper)
    {
    }
}