using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class EmployeeExtraHours : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeExtraHours",
                columns: table => new
                {
                    EmployeeExtraHoursId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeId = table.Column<long>(nullable: false),
                    EmployeeName = table.Column<string>(nullable: true),
                    WorkDate = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeExtraHours", x => x.EmployeeExtraHoursId);
                });

            migrationBuilder.CreateTable(
                name: "PaymentScheduleRulesByCustomer",
                columns: table => new
                {
                    PaymentScheduleRulesByCustomerId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<long>(nullable: false),
                    ScheduleSubservicesId = table.Column<long>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentScheduleRulesByCustomer", x => x.PaymentScheduleRulesByCustomerId);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleSubservices",
                columns: table => new
                {
                    ScheduleSubservicesId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Day = table.Column<string>(nullable: true),
                    Condition = table.Column<string>(nullable: true),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    QuantityHours = table.Column<double>(nullable: false),
                    SubServiceId = table.Column<long>(nullable: false),
                    Description = table.Column<double>(nullable: false),
                    Transport = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleSubservices", x => x.ScheduleSubservicesId);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeExtraHoursDetail",
                columns: table => new
                {
                    EmployeeExtraHoursDetailId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeExtraHoursId = table.Column<long>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    QuantityHours = table.Column<double>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeExtraHoursDetail", x => x.EmployeeExtraHoursDetailId);
                    table.ForeignKey(
                        name: "FK_EmployeeExtraHoursDetail_EmployeeExtraHours_EmployeeExtraHoursId",
                        column: x => x.EmployeeExtraHoursId,
                        principalTable: "EmployeeExtraHours",
                        principalColumn: "EmployeeExtraHoursId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeExtraHoursDetail_EmployeeExtraHoursId",
                table: "EmployeeExtraHoursDetail",
                column: "EmployeeExtraHoursId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeExtraHoursDetail");

            migrationBuilder.DropTable(
                name: "PaymentScheduleRulesByCustomer");

            migrationBuilder.DropTable(
                name: "ScheduleSubservices");

            migrationBuilder.DropTable(
                name: "EmployeeExtraHours");
        }
    }
}
