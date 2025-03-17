using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GazaHealthCenter_2.Data.Migrations
{
    public partial class sess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PatientNotes",
                table: "PsychologicalSessionModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientWhatsApp",
                table: "PsychologicalSessionModel",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PatientNotes",
                table: "PsychologicalSessionModel");

            migrationBuilder.DropColumn(
                name: "PatientWhatsApp",
                table: "PsychologicalSessionModel");
        }
    }
}
