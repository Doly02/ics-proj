using Microsoft.EntityFrameworkCore;
using SchoolSystem.BL.Mappers;
using SchoolSystem.BL.Models;
using SchoolSystem.DAL.Entities;
using SchoolSystem.DAL.Mappers;
using SchoolSystem.DAL.UnitOfWork;

namespace SchoolSystem.BL.Facades;

public class EnrolledFacade(
    IUnitOfWorkFactory unitOfWorkFactory,
    EnrolledModelMapper enrolledModelMapper)
    :
        FacadeBase<EnrolledEntity, EnrolledSubjectsListModel, SubjectDetailModel,
            EnrolledEntityMapper>(unitOfWorkFactory, enrolledModelMapper), IEnrolledFacade

{
    public async Task<EnrolledSubjectsListModel?> GetByIdAsync(Guid id)
    {
        await using var unitOfWork = UnitOfWorkFactory.Create();
        var entity = await unitOfWork.GetRepository<EnrolledEntity, EnrolledEntityMapper>().Get()
            .Include(e => e.Subject)
                .ThenInclude(subject => subject.Activities)
            .Include(e => e.Student)
            .SingleOrDefaultAsync(e => e.Id == id);

        var mapper = new EnrolledModelMapper();

        return entity != null ? mapper.MapToListModel(entity) : null;
    }

    public async Task<IEnumerable<EnrolledSubjectsListModel>> FilterByStudentNameAsync(string studentName)
    {
        await using var uow = UnitOfWorkFactory.Create();
        IQueryable<EnrolledEntity> query = uow.GetRepository<EnrolledEntity, EnrolledEntityMapper>().Get()
            .Include(e => e.Student)
            .Include(e => e.Subject)
                .ThenInclude(subject => subject.Activities)
            .Where(e => EF.Functions.Like(e.Student.Name, $"%{studentName}%"));

        foreach (string includePath in IncludesNavigationPathDetail)
        {
            query = query.Include(includePath);
        }

        List<EnrolledEntity> entities = await query.ToListAsync();

        return entities.Select(entity => ModelMapper.MapToListModel(entity)).ToList();
    }

    public async Task<IEnumerable<EnrolledSubjectsListModel>> SearchAsync(string? studentName = null, string? subjectName = null, Guid? subjectId = null)
    {
        await using var uow = UnitOfWorkFactory.Create();
        IQueryable<EnrolledEntity> query = uow.GetRepository<EnrolledEntity, EnrolledEntityMapper>().Get()
            .Include(e => e.Student)
            .Include(e => e.Subject)
                .ThenInclude(subject => subject.Activities);

        // Aplikace filtrů, pokud jsou poskytnuty
        if (!string.IsNullOrEmpty(studentName))
        {
            query = query.Where(e => EF.Functions.Like(e.Student.Name, $"%{studentName}%"));
        }

        if (!string.IsNullOrEmpty(subjectName))
        {
            query = query.Where(e => EF.Functions.Like(e.Subject.Name, $"%{subjectName}%"));
        }

        if (subjectId.HasValue)
        {
            query = query.Where(e => e.SubjectId == subjectId.Value);
        }

        foreach (string includePath in IncludesNavigationPathDetail)
        {
            query = query.Include(includePath);
        }

        List<EnrolledEntity> entities = await query.ToListAsync();

        return entities.Select(entity => ModelMapper.MapToListModel(entity)).ToList();
    }


}

