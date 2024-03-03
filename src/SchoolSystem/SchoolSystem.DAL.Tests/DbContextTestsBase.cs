using SchoolSystem.Common.Tests;
using SchoolSystem.Common.Tests.Factories;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace SchoolSystem.DAL.Tests;

public class  DbContextTestsBase : IAsyncLifetime
{
    protected DbContextTestsBase(ITestOutputHelper output)
    {
        XUnitTestOutputConverter converter = new(output);
        Console.SetOut(converter);

        DbContextFactory = new DbContextSqLiteTestingFactory(GetType().FullName!, seedTestingData: true);
        SchoolSystemDbContextSUT = DbContextFactory.CreateDbContext();
    }

    protected IDbContextFactory<SchoolSystemDbContext> DbContextFactory { get; }
    protected SchoolSystemDbContext SchoolSystemDbContextSUT { get; }


    public async Task InitializeAsync()
    {
        await SchoolSystemDbContextSUT.Database.EnsureDeletedAsync();
        await SchoolSystemDbContextSUT.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await SchoolSystemDbContextSUT.Database.EnsureDeletedAsync();
        await SchoolSystemDbContextSUT.DisposeAsync();
    }
}
