using Microsoft.EntityFrameworkCore;
using SchoolSystem.BL.Mappers;
using SchoolSystem.BL.Models;
using SchoolSystem.DAL.Entities;
using SchoolSystem.DAL.Mappers;
using SchoolSystem.DAL.Repositories;
using SchoolSystem.DAL.UnitOfWork;

namespace SchoolSystem.BL.Facades;

public class EnrolledFacade(
    IUnitOfWorkFactory unitOfWorkFactory,
    EnrolledModelMapper enrolledModelMapper)
    :
        FacadeBase<EnrolledEntity, EnrolledSubjectsListModel, EnrolledDetailModel,
            EnrolledEntityMapper>(unitOfWorkFactory, enrolledModelMapper), IEnrolledFacade

{
    public async Task SaveAsync(EnrolledSubjectsListModel model, Guid id)
    {
        EnrolledEntity entity = enrolledModelMapper.MapToEntity(model, id);

        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        IRepository<EnrolledEntity> repository = uow.GetRepository<EnrolledEntity, EnrolledEntityMapper>();

        repository.InsertEntityAsync(entity);

        await uow.CommitAsync();


    }


    public async Task SaveAsync(EnrolledDetailModel model, Guid id)
    {
        EnrolledEntity entity = enrolledModelMapper.MapToEntity(model, id);
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();

        IRepository<EnrolledEntity> repository =
            uow.GetRepository<EnrolledEntity,EnrolledEntityMapper>();

        repository.InsertEntityAsync(entity);
        await uow.CommitAsync();
    }



    public async Task<EnrolledSubjectsListModel?> GetByIdAsync(Guid id)
    {
        await using var unitOfWork = UnitOfWorkFactory.Create();
        var entity = await unitOfWork.GetRepository<EnrolledEntity, EnrolledEntityMapper>().Get()
            .Include(e => e.Subject)
                .ThenInclude(subject => subject.Activities)
            .Include(e => e.Student)
            .SingleOrDefaultAsync(e => e.Id == id);// Compare Id of Student With id In Param

        var mapper = new EnrolledModelMapper();

        return entity != null ? mapper.MapToListModel(entity) : null;
    }

    public async Task<IEnumerable<EnrolledSubjectsListModel>> GetEnrolledSubjectsByStudentIdAsync(Guid studentId)
    {
        await using var unitOfWork = UnitOfWorkFactory.Create();
        var entities = await unitOfWork.GetRepository<EnrolledEntity, EnrolledEntityMapper>().Get()
            .Include(e => e.Subject)
                .ThenInclude(subject => subject.Activities)
            .Include(e => e.Student)
            .Where(e => e.StudentId == studentId) // Filtr podle StudentId
            .ToListAsync();

        var mapper = new EnrolledModelMapper();
        return entities.Select(e => mapper.MapToListModel(e)).ToList();
    }


    public async Task<IEnumerable<EnrolledSubjectsListModel>> SearchBySubjectNameAsync(string? subjectName = null)
    {
        await using var unitOfWork = UnitOfWorkFactory.Create();
        IQueryable<EnrolledEntity> query = unitOfWork.GetRepository<EnrolledEntity, EnrolledEntityMapper>().Get();

        if (!string.IsNullOrEmpty(subjectName))
        {
            query = query.Where(e => EF.Functions.Like(e.Subject.Name, $"%{subjectName}%"));
        }

        List<EnrolledEntity> entities = await query.ToListAsync();

        return entities.Select(entity => enrolledModelMapper.MapToListModel(entity)).ToList();
    }

    public async Task<IEnumerable<EnrolledSubjectsListModel>> SortEnrolledSubjectsAscAsync()
    {
        await using var unitOfWork = UnitOfWorkFactory.Create();
        IQueryable<EnrolledEntity> query = unitOfWork.GetRepository<EnrolledEntity, EnrolledEntityMapper>().Get();

        query = query.OrderBy(e => e.Subject);

        List<EnrolledEntity> entities = await query.ToListAsync();

        return entities.Select(entity => ModelMapper.MapToListModel(entity)).ToList();
    }


}

