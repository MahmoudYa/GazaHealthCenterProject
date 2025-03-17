using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GazaHealthCenter_2.Data.Migrations
{
    public partial class consu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DepartmentModel",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConsultationRequestModel",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentId = table.Column<long>(type: "bigint", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    PatientId = table.Column<long>(type: "bigint", nullable: false),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsultationRequestModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsultationRequestModel_DepartmentModel_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "DepartmentModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DoctorModel",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Specialty = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    DepartmentId = table.Column<long>(type: "bigint", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorModel_DepartmentModel_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "DepartmentModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConsultationResponseModel",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConsultationRequestId = table.Column<long>(type: "bigint", nullable: false),
                    DoctorId = table.Column<long>(type: "bigint", nullable: false),
                    ResponseText = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    ResponseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsultationResponseModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsultationResponseModel_ConsultationRequestModel_ConsultationRequestId",
                        column: x => x.ConsultationRequestId,
                        principalTable: "ConsultationRequestModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConsultationResponseModel_DoctorModel_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "DoctorModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConsultationRequestModel_DepartmentId",
                table: "ConsultationRequestModel",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultationResponseModel_ConsultationRequestId",
                table: "ConsultationResponseModel",
                column: "ConsultationRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultationResponseModel_DoctorId",
                table: "ConsultationResponseModel",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorModel_DepartmentId",
                table: "DoctorModel",
                column: "DepartmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsultationResponseModel");

            migrationBuilder.DropTable(
                name: "ConsultationRequestModel");

            migrationBuilder.DropTable(
                name: "DoctorModel");

            migrationBuilder.DropTable(
                name: "DepartmentModel");
        }
    }
}
