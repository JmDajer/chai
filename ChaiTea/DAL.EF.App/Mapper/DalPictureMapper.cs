using AutoMapper;
using DAL.Base;
using DAL.DTO;
using Domain.App;

namespace DAL.EF.App.Mapper;

public class DalPictureMapper : BaseMapper<Picture, DalPicture>
{
    public DalPictureMapper(IMapper mapper) : base(mapper)
    {
    }
}