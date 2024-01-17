using BLL.Base;
using BLL.Contracts.App;
using BLL.DTO;
using Contracts.Base;
using DAL.Contracts.App;
using DAL.DTO;

namespace BLL.App.Services;

public class TagTypeService : BaseEntityService<BllTagType, DalTagType, ITagTypeRepository>,
    ITagTypeService
{
    protected IAppUow Uow;
    
    public TagTypeService(IAppUow uow, IMapper<DalTagType, BllTagType> mapper) :
        base(uow.TagTypeRepository, mapper)
    {
        Uow = uow;
    }
}