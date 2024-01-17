using BLL.Contracts.Base;
using Contracts.Base;
using DAL.Contracts.Base;
using Domain.Contracts.Base;

namespace BLL.Base;

public class
    BaseEntityService<TBllEntity, TDalEntity, TRepository> :
        BaseEntityService<TBllEntity, TDalEntity, TRepository, Guid>, IEntityService<TBllEntity>
    where TBllEntity : class, IDomainEntityId
    where TDalEntity : class, IDomainEntityId
    where TRepository : IBaseRepository<TDalEntity>
{
    public BaseEntityService(TRepository repository, IMapper<TDalEntity, TBllEntity> mapper) : base(repository, mapper)
    {
    }
}

public class BaseEntityService<TBllEntity, TDalEntity, TRepository, TKey> : IEntityService<TBllEntity, TKey>
    where TBllEntity : class, IDomainEntityId<TKey>
    where TDalEntity : class, IDomainEntityId<TKey>
    where TRepository : IBaseRepository<TDalEntity, TKey>
    where TKey : struct, IEquatable<TKey>
{
    protected readonly TRepository Repository;
    protected readonly IMapper<TDalEntity, TBllEntity> Mapper;

    public BaseEntityService(TRepository repository, IMapper<TDalEntity, TBllEntity> mapper)
    {
        Repository = repository;
        Mapper = mapper;
    }

    public async Task<IEnumerable<TBllEntity>> AllAsync()
    {
        return (await Repository.AllAsync()).Select(e => Mapper.Map(e)!);
    }

    public async Task<TBllEntity?> FindAsync(TKey id)
    {
        return Mapper.Map(await Repository.FindAsync(id));
    }

    public TBllEntity Add(TBllEntity entity)
    {
        return Mapper.Map(Repository.Add(Mapper.Map(entity)!))!;
    }

    public TBllEntity Update(TBllEntity entity)
    {
        return Mapper.Map(Repository.Update(Mapper.Map(entity)!))!;
    }

    public void Remove(TBllEntity entity)
    {
        Repository.Remove(Mapper.Map(entity)!);
    }

    public async Task RemoveAsync(TKey id)
    {
        await Repository.RemoveAsync(id);
    }
}