using SchoolSystem.DAL.Entities;
using Microsoft.EntityFrameworkCore;


namespace SchoolSystem.Common.Tests.Seeds;

public static class StudentSeeds
{
    public static readonly StudentEntity EmptyStudentEntity = new()
    {
        Id = default,
        Name = default!,
        Surname = default!,
        ImageUrl = default,
        Enrolleds = default!,
        Evaluations = default!
    };

    public static readonly StudentEntity StudentEntity1 = new()
    {
        Id = Guid.Parse(input: "0d4fa150-ad80-4d46-a511-4c666166ec5e"),
        Name = "John",
        Surname = "Doe",
        ImageUrl = "https://st.depositphotos.com/2672167/3849/v/450/depositphotos_38499453-stock-illustration-the-doubting-head-icon.jpg"
    };

    public static readonly StudentEntity StudentEntity2 = new()
    {
        Id = Guid.Parse(input: "87833e66-05ba-4d6b-900b-fe5ace88dbd8"),
        Name = "Jane",
        Surname = "Doe",
        ImageUrl = "https://st.depositphotos.com/2672167/3849/v/450/depositphotos_38499453-stock-illustration-the-doubting-head-icon.jpg"
    };

    //To ensure that no tests reuse these clones for non-idempotent operations
    public static readonly StudentEntity StudentEntityUpdate = StudentEntity1 with { Id = Guid.Parse("20f3f5f2-139f-4d0e-965d-267d7543344f")};
    public static readonly StudentEntity StudentEntityDelete = StudentEntity1 with { Id = Guid.Parse("7fca1488-95e4-4f92-bbfe-889017f7f071")};

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StudentEntity>().HasData(
            StudentEntity1,
            StudentEntity2,
            StudentEntityUpdate,
            StudentEntityDelete);
    }
}
