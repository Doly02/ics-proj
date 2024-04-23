using SchoolSystem.DAL.Entities;
using Microsoft.EntityFrameworkCore;


namespace SchoolSystem.DAL.Seeds;

public static class StudentSeeds
{
    public static readonly StudentEntity StudentJohn = new()
    {
        Id = Guid.Parse(input: "0d4fa150-ad80-4d46-a511-4c666166ec5e"),
        Name = "John",
        Surname = "Doe",
        ImageUrl = "https://st.depositphotos.com/2672167/3849/v/450/depositphotos_38499453-stock-illustration-the-doubting-head-icon.jpg"
    };

    public static readonly StudentEntity StudentJane = new()
    {
        Id = Guid.Parse(input: "87833e66-05ba-4d6b-900b-fe5ace88dbd8"),
        Name = "Jane",
        Surname = "Doe",
        ImageUrl = "https://st.depositphotos.com/2672167/3849/v/450/depositphotos_38499453-stock-illustration-the-doubting-head-icon.jpg"
    };

    static StudentSeeds()
    {
        StudentJohn.Enrolleds.Add(EnrolledSeeds.EnrolledJohn);
        StudentJohn.Evaluations.Add(EvaluationSeeds.EvaluationJohn);
        
        StudentJane.Enrolleds.Add(EnrolledSeeds.EnrolledJane);
    }
    
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StudentEntity>().HasData(
            StudentJohn with
            {
                Enrolleds = Array.Empty<EnrolledEntity>(),
                Evaluations = Array.Empty<EvaluationEntity>()
            },
            StudentJane with
            {
                Enrolleds = Array.Empty<EnrolledEntity>(),
                Evaluations = Array.Empty<EvaluationEntity>()
            }
        );
    }
}
