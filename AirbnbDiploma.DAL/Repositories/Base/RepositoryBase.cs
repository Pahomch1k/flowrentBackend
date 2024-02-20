using AirbnbDiploma.Core.Entities.Base;
using AirbnbDiploma.Core.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace AirbnbDiploma.DAL.Repositories.Base;

public abstract class RepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
{
    protected RepositoryBase(AppDbContext context)
    {
        Context = context;
        MainCollection = context.Set<TEntity>();
    }

    protected AppDbContext Context { get; init; }

    protected DbSet<TEntity> MainCollection { get; init; }


    public virtual async Task AddAsync(TEntity entity)
    {
        await ValidateAsync(entity);
        MainCollection.Add(entity);
    }

    public virtual async Task DeleteAsync(TKey id)
    {
        var entity = await MainCollection.FirstOrDefaultAsync(e => e.Id.Equals(id));
        ThrowIfNull(id, entity);
        MainCollection.Remove(entity!);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(int skip, int count)
    {
        return await MainCollection
            .Skip(skip)
            .Take(count)
            .ToListAsync();
    }

    public virtual async Task<TEntity> GetByIdAsync(TKey id)
    {
        var entity = await MainCollection.FirstOrDefaultAsync(e => e.Id.Equals(id));
        ThrowIfNull(id, entity);
        return entity;
    }

    public virtual async Task UpdateAsync(TEntity entity)
    {
        var oldEntity = await GetByIdAsync(entity.Id);
        await ValidateAsync(entity);

        Context.Entry(oldEntity).CurrentValues.SetValues(entity);
        throw new NotImplementedException();
    }

    protected virtual Task ValidateAsync(TEntity entity)
    {
        return Task.CompletedTask;
    }

    protected virtual void ThrowIfNull(TKey id, object? entity)
    {
        if (entity is null)
        {
            throw new NotFoundException($"{typeof(TEntity).Name} with id {id} not found");
        }
    }
}
