using SchoolSystem.BL.Facades;
using SchoolSystem.BL.Mappers;
using SchoolSystem.DAL.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace SchoolSystem.BL;

public static class BLInstaller
{
    public static IServiceCollection AddBLServices(this IServiceCollection services)
    {
        services.AddSingleton<IUnitOfWorkFactory, UnitOfWorkFactory>();

        services.Scan(selector => selector
            .FromAssemblyOf<BusinessLogic>()
            .AddClasses(filter => filter.AssignableTo(typeof(IFacade<,,>)))
            .AsMatchingInterface()
            .WithSingletonLifetime());

        services.Scan(selector => selector
            .FromAssemblyOf<BusinessLogic>()
            .AddClasses(filter => filter.AssignableTo(typeof(IModelMapper<,,>)))
            .AsSelfWithInterfaces()
            .WithSingletonLifetime());

        return services;
    }
}
