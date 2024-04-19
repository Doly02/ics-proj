using SchoolSystem.BL.Mappers;
using SchoolSystem.Common.Tests;
using SchoolSystem.Common.Tests.Factories;
using SchoolSystem.DAL;
using SchoolSystem.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;
using Microsoft.EntityFrameworkCore.Internal;
using System.Diagnostics;


public class FacadeTestsBase : IAsyncLifetime
{
    protected FacadeTestsBase(ITestOutputHelper output)
    {
        XUnitTestOutputConverter converter = new(output);
        Console.SetOut(converter);

        // DbContextFactory = new DbContextTestingInMemoryFactory(GetType().Name, seedTestingData: true);
        // DbContextFactory = new DbContextLocalDBTestingFactory(GetType().FullName!, seedTestingData: true);
        DbContextFactory = new DbContextSqLiteTestingFactory(GetType().FullName!, seedTestingData: true);

        SubjectModelMapper = new SubjectModelMapper();
        StudentModelMapper = new StudentModelMapper();
        ActivityModelMapper = new ActivityModelMapper();
        EnrolledModelMapper = new EnrolledModelMapper();
        EvaluationModelMapper = new EvaluationModelMapper();

        UnitOfWorkFactory = new UnitOfWorkFactory(DbContextFactory);
    }

    protected IDbContextFactory<SchoolSystemDbContext> DbContextFactory { get; }

    protected SubjectModelMapper SubjectModelMapper { get; }
    protected StudentModelMapper StudentModelMapper { get; }
    protected ActivityModelMapper ActivityModelMapper { get; }
    protected EnrolledModelMapper EnrolledModelMapper { get; }
    protected EvaluationModelMapper EvaluationModelMapper { get; }
    protected UnitOfWorkFactory UnitOfWorkFactory { get; }

    public async Task InitializeAsync()
    {
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        await dbx.Database.EnsureDeletedAsync();
        await dbx.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        await dbx.Database.EnsureDeletedAsync();
    }
}
