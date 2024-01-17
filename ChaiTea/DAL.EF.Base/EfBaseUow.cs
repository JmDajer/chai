using DAL.Contracts.Base;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Base;

public class EfBaseUow<TDbContext> : IBaseUow
    where TDbContext: DbContext
{
    protected readonly TDbContext UowDbContext;

    protected EfBaseUow(TDbContext dataContext)
    {
        UowDbContext = dataContext;
    }

    public virtual async Task<int> SaveChangesAsync()
    {
        return await UowDbContext.SaveChangesAsync();
    }
}