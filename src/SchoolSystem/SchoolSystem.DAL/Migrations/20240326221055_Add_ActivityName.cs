using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Add_ActivityName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnrolledEntity_Students_StudentId",
                table: "EnrolledEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_EnrolledEntity_Subjects_SubjectId",
                table: "EnrolledEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EnrolledEntity",
                table: "EnrolledEntity");

            migrationBuilder.RenameTable(
                name: "EnrolledEntity",
                newName: "Enrolleds");

            migrationBuilder.RenameIndex(
                name: "IX_EnrolledEntity_SubjectId",
                table: "Enrolleds",
                newName: "IX_Enrolleds_SubjectId");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Activities",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enrolleds",
                table: "Enrolleds",
                columns: new[] { "StudentId", "SubjectId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Enrolleds_Students_StudentId",
                table: "Enrolleds",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrolleds_Subjects_SubjectId",
                table: "Enrolleds",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrolleds_Students_StudentId",
                table: "Enrolleds");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrolleds_Subjects_SubjectId",
                table: "Enrolleds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enrolleds",
                table: "Enrolleds");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Activities");

            migrationBuilder.RenameTable(
                name: "Enrolleds",
                newName: "EnrolledEntity");

            migrationBuilder.RenameIndex(
                name: "IX_Enrolleds_SubjectId",
                table: "EnrolledEntity",
                newName: "IX_EnrolledEntity_SubjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EnrolledEntity",
                table: "EnrolledEntity",
                columns: new[] { "StudentId", "SubjectId" });

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
        }
    }
}
