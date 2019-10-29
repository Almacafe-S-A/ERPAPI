using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CierresTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CierresAccounting",
                columns: table => new
                {
                    CierreAccountingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<long>(nullable: false),
                    ParentAccountId = table.Column<int>(nullable: true),
                    CompanyInfoId = table.Column<long>(nullable: false),
                    AccountBalance = table.Column<double>(nullable: false),
                    Description = table.Column<string>(maxLength: 5000, nullable: true),
                    IsCash = table.Column<bool>(nullable: false),
                    AccountClasses = table.Column<int>(nullable: false),
                    IsContraAccount = table.Column<bool>(nullable: false),
                    TypeAccountId = table.Column<long>(nullable: false),
                    BlockedInJournal = table.Column<bool>(nullable: false),
                    AccountCode = table.Column<string>(maxLength: 50, nullable: false),
                    IdEstado = table.Column<long>(nullable: true),
                    Estado = table.Column<string>(nullable: true),
                    HierarchyAccount = table.Column<long>(nullable: false),
                    AccountName = table.Column<string>(maxLength: 200, nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    RowVersion = table.Column<byte[]>(type: "timestamp", maxLength: 8, nullable: true),
                    ParentAccountAccountId = table.Column<long>(nullable: true),
                    FechaCierre = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CierresAccounting", x => x.CierreAccountingId);
                    table.ForeignKey(
                        name: "FK_CierresAccounting_CompanyInfo_CompanyInfoId",
                        column: x => x.CompanyInfoId,
                        principalTable: "CompanyInfo",
                        principalColumn: "CompanyInfoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CierresAccounting_Accounting_ParentAccountAccountId",
                        column: x => x.ParentAccountAccountId,
                        principalTable: "Accounting",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CierresJournal",
                columns: table => new
                {
                    JournalEntryId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FechaCierre = table.Column<DateTime>(nullable: false),
                    GeneralLedgerHeaderId = table.Column<int>(nullable: true),
                    PartyTypeId = table.Column<int>(nullable: false),
                    PartyTypeName = table.Column<string>(nullable: true),
                    DocumentId = table.Column<long>(nullable: false),
                    PartyId = table.Column<int>(nullable: true),
                    VoucherType = table.Column<int>(nullable: true),
                    TypeJournalName = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    DatePosted = table.Column<DateTime>(nullable: false),
                    Memo = table.Column<string>(nullable: true),
                    ReferenceNo = table.Column<string>(nullable: true),
                    Posted = table.Column<bool>(nullable: true),
                    GeneralLedgerHeaderId1 = table.Column<long>(nullable: true),
                    PartyId1 = table.Column<long>(nullable: true),
                    IdPaymentCode = table.Column<int>(nullable: false),
                    IdTypeofPayment = table.Column<int>(nullable: false),
                    EstadoId = table.Column<long>(nullable: true),
                    EstadoName = table.Column<string>(nullable: true),
                    TotalDebit = table.Column<double>(nullable: false),
                    TotalCredit = table.Column<double>(nullable: false),
                    TypeOfAdjustmentId = table.Column<int>(nullable: false),
                    TypeOfAdjustmentName = table.Column<string>(nullable: true),
                    CreatedUser = table.Column<string>(nullable: false),
                    ModifiedUser = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CierresJournal", x => x.JournalEntryId);
                    table.ForeignKey(
                        name: "FK_CierresJournal_GeneralLedgerHeader_GeneralLedgerHeaderId1",
                        column: x => x.GeneralLedgerHeaderId1,
                        principalTable: "GeneralLedgerHeader",
                        principalColumn: "GeneralLedgerHeaderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CierresJournal_Party_PartyId1",
                        column: x => x.PartyId1,
                        principalTable: "Party",
                        principalColumn: "PartyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CierresAccounting_CompanyInfoId",
                table: "CierresAccounting",
                column: "CompanyInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_CierresAccounting_ParentAccountAccountId",
                table: "CierresAccounting",
                column: "ParentAccountAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CierresJournal_GeneralLedgerHeaderId1",
                table: "CierresJournal",
                column: "GeneralLedgerHeaderId1");

            migrationBuilder.CreateIndex(
                name: "IX_CierresJournal_PartyId1",
                table: "CierresJournal",
                column: "PartyId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CierresAccounting");

            migrationBuilder.DropTable(
                name: "CierresJournal");
        }
    }
}
