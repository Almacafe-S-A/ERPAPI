using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class JournalEntryConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "BlackListCustomers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "JournalEntryConfiguration",
                columns: table => new
                {
                    JournalEntryConfigurationId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TransactionId = table.Column<long>(nullable: false),
                    Transaction = table.Column<string>(nullable: true),
                    CurrencyId = table.Column<long>(nullable: false),
                    CurrencyName = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalEntryConfiguration", x => x.JournalEntryConfigurationId);
                });

            migrationBuilder.CreateTable(
                name: "JournalEntryConfigurationLine",
                columns: table => new
                {
                    JournalEntryConfigurationLineId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    JournalEntryConfigurationId = table.Column<string>(nullable: true),
                    AccountId = table.Column<long>(nullable: false),
                    AccountName = table.Column<string>(nullable: true),
                    DebitCredit = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalEntryConfigurationLine", x => x.JournalEntryConfigurationLineId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JournalEntryConfiguration");

            migrationBuilder.DropTable(
                name: "JournalEntryConfigurationLine");

            migrationBuilder.DropColumn(
                name: "Comments",
                table: "BlackListCustomers");
        }
    }
}
