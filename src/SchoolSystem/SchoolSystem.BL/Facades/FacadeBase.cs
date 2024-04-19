using System.Collections;
using System.Reflection;
using SchoolSystem.BL.Mappers;
using SchoolSystem.BL.Models;
using SchoolSystem.DAL.Entities;
using SchoolSystem.DAL.Mappers;
using SchoolSystem.DAL.Repositories;
using SchoolSystem.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace SchoolSystem.BL.Facades;

public abstract class
    FacadeBase<TEntity, TListModel, TDetailModel, TEntityMapper>(
        IUnitOfWorkFactory unitOfWorkFactory,
        IModelMapper<TEntity, TListModel, TDetailModel> modelMapper)
    : IFacade<TEntity, TListModel, TDetailModel>
    where TEntity : class, IEntity
    where TListModel : IModel
    where TDetailModel : class, IModel
    where TEntityMapper : IEntityMapper<TEntity>, new()
{
    // Mappers for converting between entity and model representations
    protected readonly IModelMapper<TEntity, TListModel, TDetailModel> ModelMapper = modelMapper;
    // Factory for creating units of work
    protected readonly IUnitOfWorkFactory UnitOfWorkFactory = unitOfWorkFactory;
    // Navigation properties to include in detail queries
    protected virtual ICollection<string> IncludesNavigationPathDetail => new List<string>();

    // Deletes an entity by its ID
    public async Task DeleteAsync(Guid id)
    {
        await using IUnitOfWork UnitOfWork = UnitOfWorkFactory.Create();
        try
        {
            await UnitOfWork.GetRepository<TEntity, TEntityMapper>().DeleteEntityAsync(id).ConfigureAwait(false);
            await UnitOfWork.CommitAsync().ConfigureAwait(false);
        }
        catch (DbUpdateException e)
        {
            throw new InvalidOperationException("Entity Delete Failed.", e);
        }
    }

    // Retrieves a detail model for a specific entity ID
    public virtual async Task<TDetailModel?> GetAsync(Guid id)
    {
        await using IUnitOfWork UnitOfWork = UnitOfWorkFactory.Create();

        IQueryable<TEntity> query = UnitOfWork.GetRepository<TEntity, TEntityMapper>().Get();

        foreach (string includePath in IncludesNavigationPathDetail)
        {
            query = query.Include(includePath);
        }

        TEntity? entity = await query.SingleOrDefaultAsync(e => e.Id == id).ConfigureAwait(false);

        return entity is null
            ? null
            : ModelMapper.MapToDetailModel(entity);
    }

    // Retrieves a list model representation of all entities
    // Always use paging in production
    public virtual async Task<IEnumerable<TListModel>> GetAsync()
    {
        await using IUnitOfWork UnitOfWork = UnitOfWorkFactory.Create();
        List<TEntity> entities = await UnitOfWork
            .GetRepository<TEntity, TEntityMapper>()
            .Get()
            .ToListAsync().ConfigureAwait(false);

        return ModelMapper.MapToListModel(entities);
    }

    // Saves a model (insert or update)
    public virtual async Task<TDetailModel> SaveAsync(TDetailModel model)
    {
        TDetailModel res;
        
        // Ensure collections are not set, as they're not supported for insert/update
        GuardCollectionsAreNotSet(model);

        TEntity entity = ModelMapper.MapToEntity(model);

        IUnitOfWork UnitOfWork = UnitOfWorkFactory.Create();
        IRepository<TEntity> repository = UnitOfWork.GetRepository<TEntity, TEntityMapper>();

        if (await repository.ExistsEntityAsync(entity).ConfigureAwait(false))
        {
            TEntity updatedEntity = await repository.UpdateEntityAsync(entity).ConfigureAwait(false);
            res = ModelMapper.MapToDetailModel(updatedEntity);
        }
        else
        {
            entity.Id = Guid.NewGuid();
            TEntity insertedEntity = repository.InsertEntityAsync(entity);
            res = ModelMapper.MapToDetailModel(insertedEntity);
        }

        await UnitOfWork.CommitAsync().ConfigureAwait(false);

        return res;
    }
    
    // Checks if the detail model has any non-empty collection properties and throws if it does
    private static void GuardCollectionsAreNotSet(TDetailModel model)
    {
        IEnumerable<PropertyInfo> collectionProperties = model
            .GetType()
            .GetProperties()
            .Where(i => typeof(ICollection).IsAssignableFrom(i.PropertyType));

        foreach (PropertyInfo collectionProperty in collectionProperties)
        {
            if (collectionProperty.GetValue(model) is ICollection { Count: > 0 })
            {
                throw new InvalidOperationException(
                    "The Existing Infrastructure of the Business Logic (BL) and Data Access Layer (DAL) Prohibits the Insertion or Updating of Models That Contain Related Collections.");
            }
        }
    }
}