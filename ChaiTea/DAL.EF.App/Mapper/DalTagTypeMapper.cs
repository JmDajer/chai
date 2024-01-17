using AutoMapper;
using DAL.Base;
using DAL.DTO;
using Domain.App;

namespace DAL.EF.App.Mapper;

public class DalTagTypeMapper : BaseMapper<TagType, DalTagType>
{
    public DalTagTypeMapper(IMapper mapper) : base(mapper)
    {
    }
}