using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class JournalEntryandJournalEntryLine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Party",
                columns: table => new
                {
                    PartyId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PartyType = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Website = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Fax = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Party", x => x.PartyId);
                });

            migrationBuilder.CreateTable(
                name: "JournalEntry",
                columns: table => new
                {
                    JournalEntryId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GeneralLedgerHeaderId = table.Column<int>(nullable: true),
                    PartyId = table.Column<int>(nullable: true),
                    VoucherType = table.Column<int>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Memo = table.Column<string>(nullable: true),
                    ReferenceNo = table.Column<string>(nullable: true),
                    Posted = table.Column<bool>(nullable: true),
                    GeneralLedgerHeaderId1 = table.Column<long>(nullable: true),
                    PartyId1 = table.Column<long>(nullable: true),
                    IdPaymentCode = table.Column<long>(nullable: false),
                    IdTypeofPayment = table.Column<long>(nullable: false),
                    CreatedUser = table.Column<string>(nullable: false),
                    ModifiedUser = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalEntry", x => x.JournalEntryId);
                    table.ForeignKey(
                        name: "FK_JournalEntry_GeneralLedgerHeader_GeneralLedgerHeaderId1",
                        column: x => x.GeneralLedgerHeaderId1,
                        principalTable: "GeneralLedgerHeader",
                        principalColumn: "GeneralLedgerHeaderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JournalEntry_Party_PartyId1",
                        column: x => x.PartyId1,
                        principalTable: "Party",
                        principalColumn: "PartyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JournalEntryLine",
                columns: table => new
                {
                    JournalEntryLineId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    JournalEntryId = table.Column<long>(nullable: false),
                    AccountId = table.Column<int>(nullable: false),
                    DrCr = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    Memo = table.Column<string>(nullable: true),
                    AccountId1 = table.Column<long>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalEntryLine", x => x.JournalEntryLineId);
                    table.ForeignKey(
                        name: "FK_JournalEntryLine_Account_AccountId1",
                        column: x => x.AccountId1,
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JournalEntryLine_JournalEntry_JournalEntryId",
                        column: x => x.JournalEntryId,
                        principalTable: "JournalEntry",
                        principalColumn: "JournalEntryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntry_GeneralLedgerHeaderId1",
                table: "JournalEntry",
                column: "GeneralLedgerHeaderId1");

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntry_PartyId1",
                table: "JournalEntry",
                column: "PartyId1");

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntryLine_AccountId1",
                table: "JournalEntryLine",
                column: "AccountId1");

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntryLine_JournalEntryId",
                table: "JournalEntryLine",
                column: "JournalEntryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JournalEntryLine");

            migrationBuilder.DropTable(
                name: "JournalEntry");

            migrationBuilder.DropTable(
                name: "Party");
        }
    }
}
