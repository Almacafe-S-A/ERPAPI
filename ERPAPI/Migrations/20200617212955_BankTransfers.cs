using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class BankTransfers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Propias",
                table: "InsurancePolicy",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "BankAccountTransfers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TransactionDate = table.Column<DateTime>(nullable: false),
                    SourceBank = table.Column<string>(nullable: true),
                    SourceCurrency = table.Column<string>(nullable: true),
                    TargetBank = table.Column<string>(nullable: true),
                    TargetCurrency = table.Column<string>(nullable: true),
                    SourceAccountManagementId = table.Column<long>(nullable: false),
                    DestinationAccountManagementId = table.Column<long>(nullable: false),
                    SourceAmount = table.Column<decimal>(type: "Money", nullable: false),
                    DestinationAmount = table.Column<decimal>(type: "Money", nullable: false),
                    ExchangeRateId = table.Column<long>(nullable: true),
                    Rate = table.Column<decimal>(type: "Money", nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    JournalEntryId = table.Column<long>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccountTransfers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankAccountTransfers_AccountManagement_DestinationAccountManagementId",
                        column: x => x.DestinationAccountManagementId,
                        principalTable: "AccountManagement",
                        principalColumn: "AccountManagementId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BankAccountTransfers_ExchangeRate_ExchangeRateId",
                        column: x => x.ExchangeRateId,
                        principalTable: "ExchangeRate",
                        principalColumn: "ExchangeRateId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BankAccountTransfers_JournalEntry_JournalEntryId",
                        column: x => x.JournalEntryId,
                        principalTable: "JournalEntry",
                        principalColumn: "JournalEntryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BankAccountTransfers_AccountManagement_SourceAccountManagementId",
                        column: x => x.SourceAccountManagementId,
                        principalTable: "AccountManagement",
                        principalColumn: "AccountManagementId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankAccountTransfers_DestinationAccountManagementId",
                table: "BankAccountTransfers",
                column: "DestinationAccountManagementId");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccountTransfers_ExchangeRateId",
                table: "BankAccountTransfers",
                column: "ExchangeRateId");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccountTransfers_JournalEntryId",
                table: "BankAccountTransfers",
                column: "JournalEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccountTransfers_SourceAccountManagementId",
                table: "BankAccountTransfers",
                column: "SourceAccountManagementId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankAccountTransfers");

            migrationBuilder.AlterColumn<bool>(
                name: "Propias",
                table: "InsurancePolicy",
                nullable: true,
                oldClrType: typeof(bool));
        }
    }
}
