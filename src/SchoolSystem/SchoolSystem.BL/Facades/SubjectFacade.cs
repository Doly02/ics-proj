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
    unitOfWorkFactory, modelMapper), ISubjectFacade;
