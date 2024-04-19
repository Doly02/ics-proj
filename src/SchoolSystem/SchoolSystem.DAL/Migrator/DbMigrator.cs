using SchoolSystem.DAL.Options;
using Microsoft.EntityFrameworkCore;

namespace SchoolSystem.DAL.Migrator;

public class DbMigrator(IDbContextFactory<SchoolSystemDbContext> dbContextFactory, DALOptions options)
    : IDbMigrator
{
    public void Migrate() => MigrateAsync(CancellationToken.None).GetAwaiter().GetResult();

    public async Task MigrateAsync(CancellationToken cancellationToken)
    {
        await using SchoolSystemDbContext dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);

        if(options.RecreateDatabaseEachTime)
        {
            await dbContext.Database.EnsureDeletedAsync(cancellationToken);
        }

        // Ensures that database is created applying the latest state
        // Application of migration later on may fail
        // If you want to use migrations, you should create database by calling  dbContext.Database.MigrateAsync(cancellationToken) instead
        await dbContext.Database.EnsureCreatedAsync(cancellationToken);
    }
}
