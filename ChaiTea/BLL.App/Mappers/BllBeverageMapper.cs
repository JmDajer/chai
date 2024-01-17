using AutoMapper;
using BLL.DTO;
using DAL.Base;
using DAL.DTO;

namespace BLL.App.Mappers;

public class BllBeverageMapper : BaseMapper<DalBeverage, BllBeverage>
{
    public BllBeverageMapper(IMapper mapper) : base(mapper)
    {
    }
}