using SchoolSystem.DAL.Entities;

namespace SchoolSystem.DAL.Repositories;


public interface IRepository<TEntity> where TEntity : class, IEntity
{
    /// <summary>
    /// Gets IQueryable<TEntity>, which allows queries to be executed on the TEntity collection.
    /// </summary>
    /// 
    /// <returns>IQueryable<TEntity> for further querying.</returns>
    IQueryable<TEntity> Get();
    
    /// <summary>
    /// Asynchronously deletes the entity according to the specified ID.
    /// </summary>
    /// <param name="entityID">The ID of the entity to be deleted.</param>
    /// <returns>Task for asynchronous operation.</returns>
    Task DeleteEntityAsync(Guid entityID);
    
    /// <summary>
    /// Asynchronously checks if the entity exists in the database.
    /// </summary>
    /// <param name="entity">Entity to be verified.</param>
    /// <returns>ValueTask<bool>Returns true if the entity exists; false otherwise. </returns>
    ValueTask<bool> ExistsEntityAsync(TEntity entity);
    
    /// <summary>
    /// Inserts a new entity into the database.
    /// </summary>
    /// <param name="entity">Entity to be inserted.</param>
    /// <returns>Inserted entity.</returns>
    TEntity InsertEntityAsync(TEntity entity);
    
    /// <summary>
    /// Asynchronously updates an existing entity in the database.
    /// </summary>
    /// <param name="entity">Entity to be updated.</param>
    /// <returns>Task<TEntity> returning the updated entity.</returns>
    Task<TEntity> UpdateEntityAsync(TEntity entity);
}

