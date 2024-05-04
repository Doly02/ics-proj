using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using SchoolSystem.BL.Mappers;
using SchoolSystem.BL.Models;
using SchoolSystem.DAL.Entities;
using SchoolSystem.DAL.Mappers;
using SchoolSystem.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.DAL;

namespace SchoolSystem.BL.Facades;

public class StudentFacade(
    IUnitOfWorkFactory unitOfWorkFactory,
    StudentModelMapper modelMapper)
    :
        FacadeBase<StudentEntity, StudentListModel, StudentDetailModel, StudentEntityMapper>(
            unitOfWorkFactory, modelMapper), IStudentFacade
{
    /// <summary>
    /// Asynchronously Searches for Students Based on a Name and/or a Unique Identifier.
    /// Method Supports Searching by Splitting the Name Into Terms and Looking for Matches in Both the 'Name' and 'Surname' Fields.
    /// Also Supports Filtering by a Student's Unique Identifier (Id).
    /// </summary>
    /// <param name="Name">Optional. The Name to Search For. Can Be a Full name, a Partial name, or a Combination of First and Last Names.
    /// If Null, the Search Will Skip Name Matching.</param>
    /// <param name="Id">Optional. The Unique Identifier of a Student. If Null, the Search Will Skip Id Matching.</param>
    /// <returns>A Task That Represents The Asynchronous Operation and Contains a List of Students Matching the Criteria, Mapped to a List Model.</returns>
    public async Task<IEnumerable<StudentListModel>> SearchAsync(string? Name = null,
        Guid? Id = null)
    {
        await using var uow = UnitOfWorkFactory.Create();
        // Start the Query Using a Generic Repository and a Specific Entity Mapper
        IQueryable<StudentEntity> query =
            uow.GetRepository<StudentEntity, StudentEntityMapper>().Get();

        // Look For a Match In Name And Surname 
        if (!string.IsNullOrEmpty(Name))
        {
            var terms = Name.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var term in terms)
            {
                query = query.Where(e =>
                    EF.Functions.Like(e.Name, $"%{term}%") ||
                    EF.Functions.Like(e.Surname, $"%{term}%"));
            }
        }

        if (Id.HasValue)
        {
            query = query.Where(e => e.Id == Id.Value);
        }

        // Include Navigation Properties for Detailed Querying
        foreach (string includePath in IncludesNavigationPathDetail)
        {
            query = query.Include(includePath);
        }

        // Execute the Query and Map the Results to a List Model Using a Model Mapper
        List<StudentEntity> entities = await query.ToListAsync();

        // Map Each Entity to a StudentListModel Using the Provided Model Mapper
        return entities.Select(entity => modelMapper.MapToListModel(entity)).ToList();
    }
    /// <summary>
    /// Asynchronously retrieves a list of students sorted by surname in ascending order.
    /// </summary>
    /// <returns>A Task that represents the asynchronous operation and contains a list of students sorted by surname ascending.</returns>
    public async Task<ObservableCollection<StudentListModel>> GetStudentsSortedBySurnameAscendingAsync(bool name)
    {
        // Create an instance to map entities to models.
        var studMapper = new StudentModelMapper();
        
        await using var uow = UnitOfWorkFactory.Create();
        var repository = uow.GetRepository<StudentEntity, StudentEntityMapper>();
        List<StudentEntity> ascStudents;
        if (name)
        {
            ascStudents = await repository.Get()
                .OrderBy(a => a.Name.ToLower())
                .ToListAsync();
            
        }
        else
        {
            ascStudents = await repository.Get()
                .OrderBy(a => a.Surname.ToLower())
                .ToListAsync();
        }

        return new ObservableCollection<StudentListModel>(ascStudents.Select(studMapper.MapToListModel));
    }

    /// <summary>
    /// Asynchronously retrieves a list of students sorted by surname in descending order.
    /// </summary>
    /// <returns>A Task that represents the asynchronous operation and contains a list of students sorted by surname descending.</returns>
    public async Task<ObservableCollection<StudentListModel>> GetStudentsSortedBySurnameDescendingAsync(bool name)
    {
        var studMapper = new StudentModelMapper();
    
        await using var uow = UnitOfWorkFactory.Create();
        var repository = uow.GetRepository<StudentEntity, StudentEntityMapper>();
        List<StudentEntity> desStudents;
        if (name)
        {
            desStudents = await repository.Get()
                .OrderByDescending(a => a.Name.ToLower())
                .ToListAsync();
            
        }
        else
        {
            desStudents = await repository.Get()
                .OrderByDescending(a => a.Surname.ToLower())
                .ToListAsync();
            
        }

        return new ObservableCollection<StudentListModel>(desStudents.Select(studMapper.MapToListModel));
    }
}