using Microsoft.EntityFrameworkCore;
using SchoolSystem.BL.Mappers;
using SchoolSystem.BL.Models;
using SchoolSystem.DAL.Entities;
using SchoolSystem.DAL.Mappers;
using SchoolSystem.DAL.UnitOfWork;

namespace SchoolSystem.BL.Facades;

public class SubjectFacade(
    IUnitOfWorkFactory unitOfWorkFactory,
    SubjectModelMapper modelMapper)
    :
FacadeBase<SubjectEntity, SubjectListModel, SubjectDetailModel, SubjectEntityMapper>(
    unitOfWorkFactory, modelMapper), ISubjectFacade
{
    public async Task<IEnumerable<SubjectListModel>> SearchAsync(string? Name = null, Guid? Id = null)
    {
        await using var uow = UnitOfWorkFactory.Create();
        IQueryable<SubjectEntity> query = uow.GetRepository<SubjectEntity, SubjectEntityMapper>().Get();

        if (!string.IsNullOrEmpty(Name))
        {
            query = query.Where(e => EF.Functions.Like(e.Name, $"%{Name}%"));
        }

        if (Id.HasValue)
        {
            query = query.Where(e => e.Id == Id.Value);
        }

        foreach (string includePath in IncludesNavigationPathDetail)
        {
            query = query.Include(includePath);
        }

        List<SubjectEntity> entities = await query.ToListAsync();

        return entities.Select(entity => modelMapper.MapToListModel(entity)).ToList();
    }
}
