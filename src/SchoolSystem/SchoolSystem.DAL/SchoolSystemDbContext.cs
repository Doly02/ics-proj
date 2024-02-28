using SchoolSystem.DAL.Entities;
//using SchoolSystem.DAL.Seeds;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SchoolSystem.DAL{

    public class SchoolSystemDbContext(DbContextOptions contextOptions, bool seedDemoData = false) : DbContext(contextOptions)
    {
        public DbSet<StudentEntity> Students => Set<StudentEntity>();
        public DbSet<SubjectEntity> Subjects => Set<SubjectEntity>();
        public DbSet<EvaluationEntity> Evaluations => Set<EvaluationEntity>();
        public DbSet<ActivityEntity> Activities => Set<ActivityEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ActivityEntity>()
                .HasMany(i => i.Evaluations)
                .WithOne(i => i.Activity)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SubjectEntity>()
                .HasMany<ActivityEntity>()
                .WithOne(i => i.Subject)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StudentEntity>()
                .HasMany(i => i.Subjects)
                .WithMany(i => i.Students);
                //.OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StudentEntity>()
                .HasMany<EvaluationEntity>()
                .WithOne(i => i.Student)
                .OnDelete(DeleteBehavior.Restrict);
        /*if (seedDemoData)
            {
                IngredientSeeds.Seed(modelBuilder);
                RecipeSeeds.Seed(modelBuilder);
                StudentSeeds.Seed(modelBuilder);
            }
            */
        }
    }
}