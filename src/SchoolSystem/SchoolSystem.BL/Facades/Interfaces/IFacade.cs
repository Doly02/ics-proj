using SchoolSystem.BL.Models;
using SchoolSystem.DAL.Entities;

namespace SchoolSystem.BL.Facades;

// Base Interface 
public interface IFacadeBase<TEntity> where TEntity : class, IEntity
{
    Task DeleteAsync(Guid id);
}

// Extended Interface 
public interface IListModelFacade<TEntity, TListModel> : IFacadeBase<TEntity>
    where TEntity : class, IEntity
    where TListModel : IModel
{
    Task<IEnumerable<TListModel>> GetAsync();
}
// Extended Interface 
public interface IDetailModelFacade<TEntity, TDetailModel> : IFacadeBase<TEntity>
    where TEntity : class, IEntity
    where TDetailModel : class, IModel
{
    Task<TDetailModel?> GetAsync(Guid id);
    Task<TDetailModel> SaveAsync(TDetailModel model);
}

// Full Interface 
public interface IFacade<TEntity, TListModel, TDetailModel> : IListModelFacade<TEntity, TListModel>, IDetailModelFacade<TEntity, TDetailModel>
    where TEntity : class, IEntity
    where TListModel : IModel
    where TDetailModel : class, IModel
{
    // This Interface Inherits All Operations From IListModelFacade and IDetailModelFacade.
    // No Additional Methods are Needed Here.
}