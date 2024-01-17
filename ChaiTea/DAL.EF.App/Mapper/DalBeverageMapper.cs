using AutoMapper;
using DAL.Base;
using DAL.DTO;
using Domain.App;

namespace DAL.EF.App.Mapper;

public class DalBeverageMapper : BaseMapper<Beverage, DalBeverage>
{
    public DalBeverageMapper(IMapper mapper) : base(mapper)
    {
    }
}