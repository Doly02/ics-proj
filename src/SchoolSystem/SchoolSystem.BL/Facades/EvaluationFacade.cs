using SchoolSystem.BL.Mappers;
using SchoolSystem.BL.Models;
using SchoolSystem.DAL.Entities;
using SchoolSystem.DAL.Mappers;
using SchoolSystem.DAL.UnitOfWork;

namespace SchoolSystem.BL.Facades;

public class EvaluationFacade(
    IUnitOfWorkFactory unitOfWorkFactory,
    EvaluationModelMapper evaluationModelMapper)
    :
        FacadeBase<EvaluationEntity, ActivityEvaluationListModel, EvaluationDetailModel,
            EvaluationEntityMapper>(unitOfWorkFactory, evaluationModelMapper);
