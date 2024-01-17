using Contracts.Base;
using DAL.Contracts.Base;
using Domain.Contracts.Base;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Base;

public class EfBaseRepository<TEntity, TDalEntity, TDbContext> :
    EfBaseRepository<TEntity, TDalEntity, Guid, TDbContext>, IBaseRepository<TDalEntity>
    where TEntity : class, IDomainEntityId
    where TDalEntity : class, IDomainEntityId
    where TDbContext : DbContext
{
    public EfBaseRepository(TDbContext dataContext, IMapper<TEntity, TDalEntity> mapper) : base(dataContext, mapper)
    {
    }
}

public class EfBaseRepository<TEntity, TDalEntity, TKey, TDbContext> : IBaseRepository<TDalEntity, TKey>
    where TEntity : class, IDomainEntityId<TKey>
    where TDalEntity : class, IDomainEntityId<TKey>
    where TKey : struct, IEquatable<TKey>
    where TDbContext: DbContext
{
    protected TDbContext RepositoryDbContext;
    protected DbSet<TEntity> RepositoryDbSet;
    protected IMapper<TEntity, TDalEntity> Mapper;

    public EfBaseRepository(TDbContext dataContext, IMapper<TEntity, TDalEntity> mapper) {
        RepositoryDbContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        RepositoryDbSet = RepositoryDbContext.Set<TEntity>();
        Mapper = mapper;
    }
    
    public virtual async Task<IEnumerable<TDalEntity>> AllAsync()
    {
        return await RepositoryDbSet
            .Select(t => Mapper.Map(t)!)
            .ToListAsync();
    }
    
    public virtual async Task<TDalEntity?> FindAsync(TKey id)
    {
        return Mapper.Map(await RepositoryDbSet.FirstOrDefaultAsync(t => t.Id.Equals(id)));
    }

    public virtual TDalEntity Add(TDalEntity entity)
    {
        return Mapper.Map(RepositoryDbSet.Add(Mapper.Map(entity)!).Entity)!;
    }

    public virtual TDalEntity? Update(TDalEntity entity)
    {
        return Mapper.Map(RepositoryDbSet.Update(Mapper.Map(entity)!).Entity)!;
    }

    public virtual void Remove(TDalEntity entity)
    {
        RepositoryDbSet.Remove(Mapper.Map(entity)!);
    }

    public virtual async Task RemoveAsync(TKey id)
    {
        var entity = await RepositoryDbSet.FirstOrDefaultAsync(t => t.Id.Equals(id));
        if (entity != null)
        {
            RepositoryDbSet.Remove(entity);
        }
    }
}