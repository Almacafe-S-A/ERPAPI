using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class OFAC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "sdnListPublshInformation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Publish_Date = table.Column<string>(nullable: true),
                    Record_Count = table.Column<int>(nullable: false),
                    Record_CountSpecified = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sdnListPublshInformation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "sdnListSdnEntryVesselInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    callSign = table.Column<string>(nullable: true),
                    vesselType = table.Column<string>(nullable: true),
                    vesselFlag = table.Column<string>(nullable: true),
                    vesselOwner = table.Column<string>(nullable: true),
                    tonnage = table.Column<int>(nullable: false),
                    tonnageSpecified = table.Column<bool>(nullable: false),
                    grossRegisteredTonnage = table.Column<int>(nullable: false),
                    grossRegisteredTonnageSpecified = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sdnListSdnEntryVesselInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "sdnList",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sdnListPublshInformationId = table.Column<int>(nullable: false),
                    publshInformationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sdnList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sdnList_sdnListPublshInformation_publshInformationId",
                        column: x => x.publshInformationId,
                        principalTable: "sdnListPublshInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "sdnListSdnEntry",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    uid = table.Column<int>(nullable: false),
                    firstName = table.Column<string>(nullable: true),
                    lastName = table.Column<string>(nullable: true),
                    title = table.Column<string>(nullable: true),
                    sdnType = table.Column<string>(nullable: true),
                    remarks = table.Column<string>(nullable: true),
                    programList = table.Column<string>(nullable: true),
                    vesselInfoId = table.Column<int>(nullable: true),
                    sdnListMId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sdnListSdnEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sdnListSdnEntry_sdnList_sdnListMId",
                        column: x => x.sdnListMId,
                        principalTable: "sdnList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_sdnListSdnEntry_sdnListSdnEntryVesselInfo_vesselInfoId",
                        column: x => x.vesselInfoId,
                        principalTable: "sdnListSdnEntryVesselInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "sdnListSdnEntryAddress",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sdnListSdnEntryMId = table.Column<int>(nullable: false),
                    uid = table.Column<int>(nullable: false),
                    address1 = table.Column<string>(nullable: true),
                    address2 = table.Column<string>(nullable: true),
                    address3 = table.Column<string>(nullable: true),
                    city = table.Column<string>(nullable: true),
                    stateOrProvince = table.Column<string>(nullable: true),
                    postalCode = table.Column<string>(nullable: true),
                    country = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sdnListSdnEntryAddress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sdnListSdnEntryAddress_sdnListSdnEntry_sdnListSdnEntryMId",
                        column: x => x.sdnListSdnEntryMId,
                        principalTable: "sdnListSdnEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sdnListSdnEntryAka",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sdnListSdnEntryMId = table.Column<int>(nullable: false),
                    uid = table.Column<int>(nullable: false),
                    type = table.Column<string>(nullable: true),
                    category = table.Column<string>(nullable: true),
                    lastName = table.Column<string>(nullable: true),
                    firstName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sdnListSdnEntryAka", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sdnListSdnEntryAka_sdnListSdnEntry_sdnListSdnEntryMId",
                        column: x => x.sdnListSdnEntryMId,
                        principalTable: "sdnListSdnEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sdnListSdnEntryCitizenship",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sdnListSdnEntryMId = table.Column<int>(nullable: false),
                    uid = table.Column<int>(nullable: false),
                    country = table.Column<string>(nullable: true),
                    mainEntry = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sdnListSdnEntryCitizenship", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sdnListSdnEntryCitizenship_sdnListSdnEntry_sdnListSdnEntryMId",
                        column: x => x.sdnListSdnEntryMId,
                        principalTable: "sdnListSdnEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sdnListSdnEntryDateOfBirthItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sdnListSdnEntryMId = table.Column<int>(nullable: false),
                    uid = table.Column<int>(nullable: false),
                    dateOfBirth = table.Column<string>(nullable: true),
                    mainEntry = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sdnListSdnEntryDateOfBirthItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sdnListSdnEntryDateOfBirthItem_sdnListSdnEntry_sdnListSdnEntryMId",
                        column: x => x.sdnListSdnEntryMId,
                        principalTable: "sdnListSdnEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sdnListSdnEntryID",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sdnListSdnEntryMId = table.Column<int>(nullable: false),
                    uid = table.Column<int>(nullable: false),
                    idType = table.Column<string>(nullable: true),
                    idNumber = table.Column<string>(nullable: true),
                    idCountry = table.Column<string>(nullable: true),
                    issueDate = table.Column<string>(nullable: true),
                    expirationDate = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sdnListSdnEntryID", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sdnListSdnEntryID_sdnListSdnEntry_sdnListSdnEntryMId",
                        column: x => x.sdnListSdnEntryMId,
                        principalTable: "sdnListSdnEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sdnListSdnEntryNationality",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sdnListSdnEntryMId = table.Column<int>(nullable: false),
                    uid = table.Column<int>(nullable: false),
                    country = table.Column<string>(nullable: true),
                    mainEntry = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sdnListSdnEntryNationality", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sdnListSdnEntryNationality_sdnListSdnEntry_sdnListSdnEntryMId",
                        column: x => x.sdnListSdnEntryMId,
                        principalTable: "sdnListSdnEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sdnListSdnEntryPlaceOfBirthItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sdnListSdnEntryMId = table.Column<int>(nullable: false),
                    uid = table.Column<int>(nullable: false),
                    placeOfBirth = table.Column<string>(nullable: true),
                    mainEntry = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sdnListSdnEntryPlaceOfBirthItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sdnListSdnEntryPlaceOfBirthItem_sdnListSdnEntry_sdnListSdnEntryMId",
                        column: x => x.sdnListSdnEntryMId,
                        principalTable: "sdnListSdnEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_sdnList_publshInformationId",
                table: "sdnList",
                column: "publshInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_sdnListSdnEntry_sdnListMId",
                table: "sdnListSdnEntry",
                column: "sdnListMId");

            migrationBuilder.CreateIndex(
                name: "IX_sdnListSdnEntry_vesselInfoId",
                table: "sdnListSdnEntry",
                column: "vesselInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_sdnListSdnEntryAddress_sdnListSdnEntryMId",
                table: "sdnListSdnEntryAddress",
                column: "sdnListSdnEntryMId");

            migrationBuilder.CreateIndex(
                name: "IX_sdnListSdnEntryAka_sdnListSdnEntryMId",
                table: "sdnListSdnEntryAka",
                column: "sdnListSdnEntryMId");

            migrationBuilder.CreateIndex(
                name: "IX_sdnListSdnEntryCitizenship_sdnListSdnEntryMId",
                table: "sdnListSdnEntryCitizenship",
                column: "sdnListSdnEntryMId");

            migrationBuilder.CreateIndex(
                name: "IX_sdnListSdnEntryDateOfBirthItem_sdnListSdnEntryMId",
                table: "sdnListSdnEntryDateOfBirthItem",
                column: "sdnListSdnEntryMId");

            migrationBuilder.CreateIndex(
                name: "IX_sdnListSdnEntryID_sdnListSdnEntryMId",
                table: "sdnListSdnEntryID",
                column: "sdnListSdnEntryMId");

            migrationBuilder.CreateIndex(
                name: "IX_sdnListSdnEntryNationality_sdnListSdnEntryMId",
                table: "sdnListSdnEntryNationality",
                column: "sdnListSdnEntryMId");

            migrationBuilder.CreateIndex(
                name: "IX_sdnListSdnEntryPlaceOfBirthItem_sdnListSdnEntryMId",
                table: "sdnListSdnEntryPlaceOfBirthItem",
                column: "sdnListSdnEntryMId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sdnListSdnEntryAddress");

            migrationBuilder.DropTable(
                name: "sdnListSdnEntryAka");

            migrationBuilder.DropTable(
                name: "sdnListSdnEntryCitizenship");

            migrationBuilder.DropTable(
                name: "sdnListSdnEntryDateOfBirthItem");

            migrationBuilder.DropTable(
                name: "sdnListSdnEntryID");

            migrationBuilder.DropTable(
                name: "sdnListSdnEntryNationality");

            migrationBuilder.DropTable(
                name: "sdnListSdnEntryPlaceOfBirthItem");

            migrationBuilder.DropTable(
                name: "sdnListSdnEntry");

            migrationBuilder.DropTable(
                name: "sdnList");

            migrationBuilder.DropTable(
                name: "sdnListSdnEntryVesselInfo");

            migrationBuilder.DropTable(
                name: "sdnListPublshInformation");
        }
    }
}
