using SchoolSystem.BL.Mappers;
using SchoolSystem.BL.Models;
using SchoolSystem.DAL.Entities;
using SchoolSystem.DAL.Mappers;
using SchoolSystem.DAL.UnitOfWork;

namespace SchoolSystem.BL.Facades;

public class SubjectFacade(   
    IUnitOfWorkFactory unitOfWorkFactory,
    StudentModelMapper modelMapper)
    :
FacadeBase<StudentEntity, StudentListModel, StudentDetailModel, StudentEntityMapper>(
    unitOfWorkFactory, modelMapper), IStudentFacade;
