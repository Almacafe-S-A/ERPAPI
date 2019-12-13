/********************************************************************************************************

 -- NAME   :  CRUDMeasure

 -- PROPOSE:  show Measure from company



 REVISIONS:



 version              Date                Author                        Description

 ----------           -------------       ---------------               -------------------------------
 1.0                  12/12/2019          Marvin.Guillen                Changes to create model
 

 ********************************************************************************************************/


using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CreateMeasureTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Measure",
                columns: table => new
                {
                    MeasurelId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    High = table.Column<decimal>(nullable: true),
                    width = table.Column<decimal>(nullable: true),
                    thickness = table.Column<decimal>(nullable: true),
                    quantity = table.Column<decimal>(nullable: true),
                    faces = table.Column<decimal>(nullable: true),
                    IdEstado = table.Column<long>(nullable: true),
                    CreatedUser = table.Column<string>(nullable: false),
                    ModifiedUser = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measure", x => x.MeasurelId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Measure");
        }
    }
}
