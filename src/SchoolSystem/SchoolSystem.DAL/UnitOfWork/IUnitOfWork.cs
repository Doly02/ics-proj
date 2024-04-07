using SchoolSystem.DAL.Entities;
using SchoolSystem.DAL.Mappers;
using SchoolSystem.DAL.Repositories;

namespace SchoolSystem.DAL.UnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IRepository<TEntity> GetRepository<TEntity, TEntityMapper>()
            where TEntity : class, IEntity
            where TEntityMapper : IEntityMapper<TEntity>, new();

        Task CommitAsync();
    }
}
