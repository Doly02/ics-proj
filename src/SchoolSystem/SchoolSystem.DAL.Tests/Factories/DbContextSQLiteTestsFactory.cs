using SchoolSystem.DAL;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Common.Tests.Factories;

public class DbContextSqLiteTestingFactory(string databaseName, bool seedTestingData = false)
    : IDbContextFactory<SchoolSystemDbContext>
{
    public SchoolSystemDbContext CreateDbContext()
    {
        DbContextOptionsBuilder<SchoolSystemDbContext> builder = new();
        builder.UseSqlite($"Data Source={databaseName};Cache=Shared");

        // builder.LogTo(Console.WriteLine); //Enable in case you want to see tests details, enabled may cause some inconsistencies in tests
        // builder.EnableSensitiveDataLogging();

        return new SchoolSystemDbContext(builder.Options, seedTestingData);
    }
}
