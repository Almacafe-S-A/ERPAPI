using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CierresJournalEntryLines : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "JournalEntryId",
                table: "CierresJournal",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "CierresJournalEntryLine",
                columns: table => new
                {
                    CierresJournalEntryLineId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    JournalEntryLineId = table.Column<long>(nullable: false),
                    JournalEntryId = table.Column<long>(nullable: false),
                    CostCenterId = table.Column<long>(maxLength: 30, nullable: false),
                    CostCenterName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 60, nullable: true),
                    AccountId = table.Column<int>(nullable: false),
                    AccountName = table.Column<string>(nullable: true),
                    Debit = table.Column<double>(nullable: false),
                    Credit = table.Column<double>(nullable: false),
                    DebitSy = table.Column<double>(nullable: false),
                    CreditSy = table.Column<double>(nullable: false),
                    DebitME = table.Column<double>(nullable: false),
                    CreditME = table.Column<double>(nullable: false),
                    Memo = table.Column<string>(nullable: true),
                    AccountId1 = table.Column<long>(nullable: true),
                    CreatedUser = table.Column<string>(nullable: false),
                    ModifiedUser = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CierresJournalEntryLine", x => x.CierresJournalEntryLineId);
                    table.ForeignKey(
                        name: "FK_CierresJournalEntryLine_Accounting_AccountId1",
                        column: x => x.AccountId1,
                        principalTable: "Accounting",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CierresJournalEntryLine_JournalEntry_JournalEntryId",
                        column: x => x.JournalEntryId,
                        principalTable: "JournalEntry",
                        principalColumn: "JournalEntryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CierresJournalEntryLine_AccountId1",
                table: "CierresJournalEntryLine",
                column: "AccountId1");

            migrationBuilder.CreateIndex(
                name: "IX_CierresJournalEntryLine_JournalEntryId",
                table: "CierresJournalEntryLine",
                column: "JournalEntryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CierresJournalEntryLine");

            migrationBuilder.DropColumn(
                name: "JournalEntryId",
                table: "CierresJournal");
        }
    }
}
