using SchoolSystem.DAL.Entities;
using SchoolSystem.DAL.Mappers;
using Microsoft.EntityFrameworkCore;
namespace SchoolSystem.DAL.Repositories;

/// <summary>
/// Provides a generic repository for CRUD operations (accessing, adding, updating, and deleting entities in the database)
/// </summary>
/// <typeparam name="TEntity">The type of the entity this repository manages. Must implement IEntity.</typeparam>
public class Repository<TEntity>(
    DbContext dbContext,
    IEntityMapper<TEntity> entityMapper)
    : IRepository<TEntity>
    where TEntity : class, IEntity
{
    // Initializes a DbSet for the TEntity to perform operations on the database
    private readonly DbSet<TEntity> _dbSet = dbContext.Set<TEntity>();

    // Returns an IQueryable for TEntity, allowing for LINQ queries on the entity set
    public IQueryable<TEntity> Get() => _dbSet;

    // Asynchronously checks if an entity exists in the database by its Id.
    public async ValueTask<bool> ExistsEntityAsync(TEntity entity)
        => entity.Id != Guid.Empty
           && await _dbSet.AnyAsync(e => e.Id == entity.Id).ConfigureAwait(false);

    // Adds a new entity to the database asynchronously and returns the added entity.
    public TEntity InsertEntityAsync(TEntity entity)
        => _dbSet.Add(entity).Entity;

    // Updates an existing entity in the database asynchronously
    // It first fetches the existing entity by its Id
    // maps the changes from the input entity to the existing entity, and then returns the updated entity
    public async Task<TEntity> UpdateEntityAsync(TEntity entity)
    {
        TEntity existingEntity = await _dbSet.SingleAsync(e => e.Id == entity.Id).ConfigureAwait(false);
        entityMapper.MapToExistingEntity(existingEntity, entity);
        return existingEntity;
    }

    // Deletes an entity from the database asynchronously based on its Id
    public async Task DeleteEntityAsync(Guid entityId)
        => _dbSet.Remove(await _dbSet.SingleAsync(i => i.Id == entityId).ConfigureAwait(false));
}
