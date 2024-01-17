using AutoMapper;
using BLL.DTO;
using DAL.Base;
using DAL.DTO;

namespace BLL.App.Mappers;

public class BllPictureMapper : BaseMapper<DalPicture, BllPicture>
{
    public BllPictureMapper(IMapper mapper) : base(mapper)
    {
    }
}