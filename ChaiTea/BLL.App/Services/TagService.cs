using BLL.Base;
using BLL.Contracts.App;
using BLL.DTO;
using Contracts.Base;
using DAL.Contracts.App;
using DAL.DTO;

namespace BLL.App.Services;

public class TagService : BaseEntityService<BllTag, DalTag, ITagRepository>, ITagService
{
    protected IAppUow Uow;
    
    public TagService(IAppUow uow, IMapper<DalTag, BllTag> mapper) :
        base(uow.TagRepository, mapper)
    {
        Uow = uow;
    }
}