using AutoMapper;
using BLL.DTO;
using DAL.Base;
using DAL.DTO;

namespace BLL.App.Mappers;

public class BllTagTypeMapper : BaseMapper<DalTagType, BllTagType>
{
    public BllTagTypeMapper(IMapper mapper) : base(mapper)
    {
    }
}