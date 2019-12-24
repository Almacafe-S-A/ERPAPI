using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class fieldEstadoNamejournalentrydeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //solo utilizarla si su tabla en UNIPOLE el campo EstadoName es Int 
            // migrationBuilder.DropColumn(
            //    name: "EstadoName",
            //   table: "JournalEntry");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //solo utilizarla si su tabla en UNIPOLE el campo EstadoName es Int 

            //migrationBuilder.AddColumn<string>(
            //    name: "EstadoName",
            //   table: "JournalEntry",
            //   nullable: true);
        }
    }
}
