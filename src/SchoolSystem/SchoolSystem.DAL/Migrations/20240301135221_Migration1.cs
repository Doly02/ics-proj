using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Migration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Subjects_SubjectEntityId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Subjects_SubjectId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_EnrolledEntity_Students_StudentId",
                table: "EnrolledEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_EnrolledEntity_Subjects_SubjectId",
                table: "EnrolledEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_Students_StudentEntityId",
                table: "Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_Students_StudentId",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_StudentEntityId",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Activities_SubjectEntityId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "StudentEntityId",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "SubjectEntityId",
                table: "Activities");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Subjects_SubjectId",
                table: "Activities",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EnrolledEntity_Students_StudentId",
                table: "EnrolledEntity",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EnrolledEntity_Subjects_SubjectId",
                table: "EnrolledEntity",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_Students_StudentId",
                table: "Evaluations",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Subjects_SubjectId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_EnrolledEntity_Students_StudentId",
                table: "EnrolledEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_EnrolledEntity_Subjects_SubjectId",
                table: "EnrolledEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_Students_StudentId",
                table: "Evaluations");

            migrationBuilder.AddColumn<Guid>(
                name: "StudentEntityId",
                table: "Evaluations",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SubjectEntityId",
                table: "Activities",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_StudentEntityId",
                table: "Evaluations",
                column: "StudentEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_SubjectEntityId",
                table: "Activities",
                column: "SubjectEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Subjects_SubjectEntityId",
                table: "Activities",
                column: "SubjectEntityId",
                principalTable: "Subjects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Subjects_SubjectId",
                table: "Activities",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EnrolledEntity_Students_StudentId",
                table: "EnrolledEntity",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EnrolledEntity_Subjects_SubjectId",
                table: "EnrolledEntity",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_Students_StudentEntityId",
                table: "Evaluations",
                column: "StudentEntityId",
                principalTable: "Students",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_Students_StudentId",
                table: "Evaluations",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
