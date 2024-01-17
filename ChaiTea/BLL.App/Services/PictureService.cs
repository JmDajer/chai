using BLL.Base;
using BLL.Contracts.App;
using BLL.DTO;
using Contracts.Base;
using DAL.Contracts.App;
using DAL.DTO;

namespace BLL.App.Services;

public class PictureService : BaseEntityService<BllPicture, DalPicture, IPictureRepository>,
    IPictureService
{
    protected IAppUow Uow;
    
    public PictureService(IAppUow uow, IMapper<DalPicture, BllPicture> mapper) :
        base(uow.PictureRepository, mapper)
    {
        Uow = uow;
    }
}