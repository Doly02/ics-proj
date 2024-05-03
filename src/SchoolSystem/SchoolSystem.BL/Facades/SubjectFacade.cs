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
    public async Task<IEnumerable<SubjectListModel>> SearchAsync(string? Search = null)
    {
        await using var uow = UnitOfWorkFactory.Create();
        IQueryable<SubjectEntity> query = uow.GetRepository<SubjectEntity, SubjectEntityMapper>().Get();

        if (!string.IsNullOrEmpty(Search))
        {
            query = query.Where(e => EF.Functions.Like(e.Name, $"%{Search}%") || EF.Functions.Like(e.Abbreviation, $"%{Search}%"));
        }
        /*
        foreach (string includePath in IncludesNavigationPathDetail)
        {
            query = query.Include(includePath);
        }
        */
        List<SubjectEntity> entities = await query.ToListAsync();

        return entities.Select(entity => modelMapper.MapToListModel(entity)).ToList();
    }

    public async Task<IEnumerable<SubjectListModel>> GetSortedByNameAscAsync()
    {
        await using var uow = UnitOfWorkFactory.Create();

        IQueryable<SubjectEntity> query = uow.GetRepository<SubjectEntity, SubjectEntityMapper>().Get();

        // Sort the subjects by name in ascending order
        query = query.OrderBy(e => e.Name);
        //query = query.OrderByDescending(e => e.Name);

        List<SubjectEntity> entities = await query.ToListAsync();

        return entities.Select(entity => modelMapper.MapToListModel(entity)).ToList();
    }

    public async Task<IEnumerable<SubjectListModel>> GetSortedByNameDescAsync()
    {
        await using var uow = UnitOfWorkFactory.Create();

        IQueryable<SubjectEntity> query = uow.GetRepository<SubjectEntity, SubjectEntityMapper>().Get();

        // Sort the subjects by name in ascending order
        query = query.OrderBy(e => e.Name);
        //query = query.OrderByDescending(e => e.Name);

        List<SubjectEntity> entities = await query.ToListAsync();

        return entities.Select(entity => modelMapper.MapToListModel(entity)).ToList();
    }
}
