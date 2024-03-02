using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Migration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EnrolledEntity",
                table: "EnrolledEntity");

            migrationBuilder.DropIndex(
                name: "IX_EnrolledEntity_StudentId",
                table: "EnrolledEntity");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "EnrolledEntity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EnrolledEntity",
                table: "EnrolledEntity",
                columns: new[] { "StudentId", "SubjectId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EnrolledEntity",
                table: "EnrolledEntity");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "EnrolledEntity",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_EnrolledEntity",
                table: "EnrolledEntity",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolledEntity_StudentId",
                table: "EnrolledEntity",
                column: "StudentId");
        }
    }
}
