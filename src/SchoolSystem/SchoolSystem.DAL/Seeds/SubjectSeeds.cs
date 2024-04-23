using SchoolSystem.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace SchoolSystem.DAL.Seeds;

public static class SubjectSeeds
{
    public static readonly SubjectEntity ICS = new()
    {
        Id = Guid.Parse(input: "cf1300e7-2cd1-4f20-93f4-811c1d164abb"),
        Name = "C# seminar",
        Abbreviation = "ICS"
    };

    static SubjectSeeds()
    {
        ICS.Activities.Add(ActivitySeeds.Project);
        ICS.Enrolleds.Add(EnrolledSeeds.EnrolledJane);
        ICS.Enrolleds.Add(EnrolledSeeds.EnrolledJohn);
    }
    
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SubjectEntity>().HasData(
            ICS with
            {
                Activities = Array.Empty<ActivityEntity>(),
                Enrolleds = Array.Empty<EnrolledEntity>()
            }
        );
    }
}
