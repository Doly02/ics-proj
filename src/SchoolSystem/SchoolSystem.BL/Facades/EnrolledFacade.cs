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
            .SingleOrDefaultAsync(e => e.Id == id);// Compare Id of Student With id In Param

        var mapper = new EnrolledModelMapper();

        return entity != null ? mapper.MapToListModel(entity) : null;
    }


    public async Task<IEnumerable<EnrolledSubjectsListModel>> SearchBySubjectNameAsync(string subjectName)
    {
        await using var unitOfWork = UnitOfWorkFactory.Create();
        IQueryable<EnrolledEntity> query = unitOfWork.GetRepository<EnrolledEntity, EnrolledEntityMapper>().Get()
            .Include(e => e.Subject)
            .Where(e => EF.Functions.Like(e.Subject.Name, $"%{subjectName}%"));

        var entities = await query.ToListAsync();

        return entities.Select(entity => ModelMapper.MapToListModel(entity)).ToList();
    }


}

