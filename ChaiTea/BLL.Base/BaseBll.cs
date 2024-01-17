using BLL.Contracts.Base;
using DAL.Contracts.Base;

namespace BLL.Base;

public abstract class BaseBll<TUow> : IBaseBll
    where TUow : IBaseUow
{
    protected readonly TUow Uow;

    protected BaseBll(TUow uow)
    {
        Uow = uow;
    }

    public virtual async Task<int> SaveChangesAsync()
    {
        return await Uow.SaveChangesAsync();
    }
}