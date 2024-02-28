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
                .WithOne(i => i.Student)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<IngredientEntity>()
                .HasMany<StudentEntity>()
                .WithOne(i => i.Ingredient)
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