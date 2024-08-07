using SchoolSystem.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace SchoolSystem.DAL{

    public class SchoolSystemDbContext(DbContextOptions contextOptions, bool seedDemoData = false) : DbContext(contextOptions)
    {
        public DbSet<StudentEntity> Students => Set<StudentEntity>();
        public DbSet<SubjectEntity> Subjects => Set<SubjectEntity>();
        public DbSet<EvaluationEntity> Evaluations => Set<EvaluationEntity>();
        public DbSet<ActivityEntity> Activities => Set<ActivityEntity>();
        public DbSet<EnrolledEntity> Enrolleds => Set<EnrolledEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<EvaluationEntity>()
                .HasKey(e => new { e.StudentId, e.ActivityId });

            modelBuilder.Entity<EnrolledEntity>()
                .HasKey(e => new { e.StudentId, e.SubjectId });

            modelBuilder.Entity<ActivityEntity>()
                .HasMany(i => i.Evaluations)
                .WithOne(i => i.Activity)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SubjectEntity>()
                .HasMany(i => i.Activities)
                .WithOne(i => i.Subject)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<SubjectEntity>()
                .HasMany(i => i.Enrolleds)
                .WithOne(i => i.Subject)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StudentEntity>()
                .HasMany(i => i.Enrolleds)
                .WithOne(i => i.Student)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StudentEntity>()
                .HasMany(i => i.Evaluations)
                .WithOne(i => i.Student)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
