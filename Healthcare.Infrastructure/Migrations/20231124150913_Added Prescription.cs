using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Healthcare.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedPrescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PrescriptionId",
                table: "MedicalReports",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "InvestigationTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    ContentFormat = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOnUtcTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestigationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Prescriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MedicalReportId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsTaken = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Investigations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MedicalReportId = table.Column<Guid>(type: "uuid", nullable: false),
                    InvestigationTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    Conclusion = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MedicalReportId1 = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Investigations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Investigations_InvestigationTypes_InvestigationTypeId",
                        column: x => x.InvestigationTypeId,
                        principalTable: "InvestigationTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Investigations_MedicalReports_MedicalReportId",
                        column: x => x.MedicalReportId,
                        principalTable: "MedicalReports",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Investigations_MedicalReports_MedicalReportId1",
                        column: x => x.MedicalReportId1,
                        principalTable: "MedicalReports",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicalReports_PrescriptionId",
                table: "MedicalReports",
                column: "PrescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_MedicalReportId",
                table: "Appointments",
                column: "MedicalReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Investigations_InvestigationTypeId",
                table: "Investigations",
                column: "InvestigationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Investigations_MedicalReportId",
                table: "Investigations",
                column: "MedicalReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Investigations_MedicalReportId1",
                table: "Investigations",
                column: "MedicalReportId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_MedicalReports_MedicalReportId",
                table: "Appointments",
                column: "MedicalReportId",
                principalTable: "MedicalReports",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalReports_Prescriptions_PrescriptionId",
                table: "MedicalReports",
                column: "PrescriptionId",
                principalTable: "Prescriptions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_MedicalReports_MedicalReportId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalReports_Prescriptions_PrescriptionId",
                table: "MedicalReports");

            migrationBuilder.DropTable(
                name: "Investigations");

            migrationBuilder.DropTable(
                name: "Prescriptions");

            migrationBuilder.DropTable(
                name: "InvestigationTypes");

            migrationBuilder.DropIndex(
                name: "IX_MedicalReports_PrescriptionId",
                table: "MedicalReports");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_MedicalReportId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "PrescriptionId",
                table: "MedicalReports");
        }
    }
}
