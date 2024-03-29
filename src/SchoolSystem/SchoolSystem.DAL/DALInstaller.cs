using SchoolSystem.DAL.Factories;
using SchoolSystem.DAL.Mappers;
using SchoolSystem.DAL.Migrator;
using SchoolSystem.DAL.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;

namespace SchoolSystem.DAL;

public static class DALInstaller
{
    public static IServiceCollection AddDALServices(this IServiceCollection services, DALOptions options)
    {
        services.AddSingleton(options);

        if (options is null)
        {
            throw new InvalidOperationException("No persistence provider configured");
        }

        if (string.IsNullOrEmpty(options.DatabaseDirectory))
        {
            throw new InvalidOperationException($"{nameof(options.DatabaseDirectory)} is not set");
        }
        if (string.IsNullOrEmpty(options.DatabaseName))
        {
            throw new InvalidOperationException($"{nameof(options.DatabaseName)} is not set");
        }

        services.AddSingleton<IDbContextFactory<SchoolSystemDbContext>>(_ =>
            new DbContextSqLiteFactory(options.DatabaseFilePath, options?.SeedDemoData ?? false));
        services.AddSingleton<IDbMigrator, DbMigrator>();

        services.AddSingleton<ActivityEntityMapper>();
        services.AddSingleton<EnrolledEntityMapper>();
        services.AddSingleton<EvaluationEntityMapper>();
        services.AddSingleton<StudentEntityMapper>();
        services.AddSingleton<SubjectEntityMapper>();

        return services;
    }
}
