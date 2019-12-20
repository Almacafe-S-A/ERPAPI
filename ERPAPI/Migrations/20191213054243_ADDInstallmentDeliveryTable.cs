/********************************************************************************************************

 -- NAME   :  CRUDInstallmentDelivery

 -- PROPOSE:  show InstallmentDelivery from company



 REVISIONS:



 version              Date                Author                        Description

 ----------           -------------       ---------------               -------------------------------
 2.0                  17/12/2019          Marvin.Guillen                Changes to create model
 1.0                  13/12/2019          Marvin.Guillen                Changes to create model
 

 ********************************************************************************************************/

using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class ADDInstallmentDeliveryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InstallmentDelivery",
                columns: table => new
                {
                    InstallmentDeliveryId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: true),
                    CreatedUser = table.Column<string>(nullable: false),
                    ModifiedUser = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstallmentDelivery", x => x.InstallmentDeliveryId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstallmentDelivery");
        }
    }
}
