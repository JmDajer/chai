using AutoMapper;
using BLL.DTO;
using DAL.Base;
using DAL.DTO;

namespace BLL.App.Mappers;

public class BllTagMapper :  BaseMapper<DalTag, BllTag>
{
    public BllTagMapper(IMapper mapper) : base(mapper)
    {
    }
}