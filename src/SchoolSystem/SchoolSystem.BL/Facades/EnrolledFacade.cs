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
            EnrolledEntityMapper>(unitOfWorkFactory, enrolledModelMapper);

