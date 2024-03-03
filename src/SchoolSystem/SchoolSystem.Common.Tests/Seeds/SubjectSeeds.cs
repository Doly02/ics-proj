﻿using SchoolSystem.DAL.Enums;
using SchoolSystem.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.DAL.Entities;

namespace SchoolSystem.Common.Tests.Seeds;

public static class SubjectSeeds
{
    public static readonly SubjectEntity EmptySubjectEntity = new()
    {
        Id = default,
        Name = default,
        Abbreviation = default,
        Enrolleds = default!,
        Activities = default!
    };

    public static readonly SubjectEntity ICS = new()
    {
        Id = Guid.Parse(input: "cf1300e7-2cd1-4f20-93f4-811c1d164abb"),
        Name = "C# seminar",
        Abbreviation = "ICS"
    };

    //To ensure that no tests reuse these clones for non-idempotent operations
    public static readonly SubjectEntity SubjectEntityUpdate = ICS with { Id = Guid.Parse("A2E6849D-A158-4436-980C-7FC26B60C674")};
    public static readonly SubjectEntity SubjectEntityDelete = ICS with { Id = Guid.Parse("30872EFF-CED4-4F2B-89DB-0EE83A74D279")};

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SubjectEntity>().HasData(
            ICS,
            SubjectEntityUpdate,
            SubjectEntityDelete);
    }
}