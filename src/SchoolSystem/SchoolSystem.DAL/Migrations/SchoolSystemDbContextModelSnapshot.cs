﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SchoolSystem.DAL;

#nullable disable

namespace SchoolSystem.DAL.Migrations
{
    [DbContext(typeof(SchoolSystemDbContext))]
    partial class SchoolSystemDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.2");

            modelBuilder.Entity("SchoolSystem.DAL.Entities.ActivityEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("ActivityType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("End")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Place")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Start")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SubjectId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SubjectId");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("SchoolSystem.DAL.Entities.EnrolledEntity", b =>
                {
                    b.Property<Guid>("StudentId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SubjectId")
                        .HasColumnType("TEXT");

                    b.HasKey("StudentId", "SubjectId");

                    b.HasIndex("SubjectId");

                    b.ToTable("Enrolleds");
                });

            modelBuilder.Entity("SchoolSystem.DAL.Entities.EvaluationEntity", b =>
                {
                    b.Property<Guid>("StudentId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ActivityId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Note")
                        .HasColumnType("TEXT");

                    b.Property<int>("Score")
                        .HasColumnType("INTEGER");

                    b.HasKey("StudentId", "ActivityId");

                    b.HasIndex("ActivityId");

                    b.ToTable("Evaluations");
                });

            modelBuilder.Entity("SchoolSystem.DAL.Entities.StudentEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("SchoolSystem.DAL.Entities.SubjectEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Abbreviation")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("SchoolSystem.DAL.Entities.ActivityEntity", b =>
                {
                    b.HasOne("SchoolSystem.DAL.Entities.SubjectEntity", "Subject")
                        .WithMany("Activities")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("SchoolSystem.DAL.Entities.EnrolledEntity", b =>
                {
                    b.HasOne("SchoolSystem.DAL.Entities.StudentEntity", "Student")
                        .WithMany("Enrolleds")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SchoolSystem.DAL.Entities.SubjectEntity", "Subject")
                        .WithMany("Enrolleds")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Student");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("SchoolSystem.DAL.Entities.EvaluationEntity", b =>
                {
                    b.HasOne("SchoolSystem.DAL.Entities.ActivityEntity", "Activity")
                        .WithMany("Evaluations")
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SchoolSystem.DAL.Entities.StudentEntity", "Student")
                        .WithMany("Evaluations")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Activity");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("SchoolSystem.DAL.Entities.ActivityEntity", b =>
                {
                    b.Navigation("Evaluations");
                });

            modelBuilder.Entity("SchoolSystem.DAL.Entities.StudentEntity", b =>
                {
                    b.Navigation("Enrolleds");

                    b.Navigation("Evaluations");
                });

            modelBuilder.Entity("SchoolSystem.DAL.Entities.SubjectEntity", b =>
                {
                    b.Navigation("Activities");

                    b.Navigation("Enrolleds");
                });
#pragma warning restore 612, 618
        }
    }
}
