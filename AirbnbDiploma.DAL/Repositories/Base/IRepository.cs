namespace AirbnbDiploma.DAL.Repositories.Base;

public interface IRepository<TEntity, TKey>
{
    Task AddAsync(TEntity entity);

    Task<IEnumerable<TEntity>> GetAllAsync(int skip, int count);

    Task<TEntity> GetByIdAsync(TKey id);

    Task UpdateAsync(TEntity entity);

    Task DeleteAsync(TKey id);
}