using DAL.Contracts.Base;
using Domain.Contracts.Base;

namespace BLL.Contracts.Base;

public interface IEntityService<TEntity> : IBaseRepository<TEntity>, IEntityService<TEntity, Guid>
    where TEntity : class, IDomainEntityId
{
    
}

public interface IEntityService<TEntity, TKey> : IBaseRepository<TEntity, TKey>
    where TEntity : class, IDomainEntityId<TKey>
    where TKey : struct, IEquatable<TKey>
{
    
}