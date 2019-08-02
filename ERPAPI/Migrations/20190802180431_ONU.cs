using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class ONU : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CONSOLIDATED_LISTM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    dateGenerated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CONSOLIDATED_LISTM", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DESIGNATIONM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VALUE = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DESIGNATIONM", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ENTITIESM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ENTITIESM", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "INDIVIDUALSM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INDIVIDUALSM", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LAST_DAY_UPDATEDM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VALUE = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LAST_DAY_UPDATEDM", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LIST_TYPEM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VALUE = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LIST_TYPEM", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NATIONALITYM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VALUE = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NATIONALITYM", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TITLEM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VALUE = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TITLEM", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ENTITYM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DATAID = table.Column<string>(nullable: true),
                    VERSIONNUM = table.Column<string>(nullable: true),
                    FIRST_NAME = table.Column<string>(nullable: true),
                    UN_LIST_TYPE = table.Column<string>(nullable: true),
                    REFERENCE_NUMBER = table.Column<string>(nullable: true),
                    LISTED_ON = table.Column<DateTime>(nullable: false),
                    SUBMITTED_ON = table.Column<DateTime>(nullable: false),
                    SUBMITTED_ONSpecified = table.Column<bool>(nullable: false),
                    NAME_ORIGINAL_SCRIPT = table.Column<string>(nullable: true),
                    COMMENTS1 = table.Column<string>(nullable: true),
                    LIST_TYPEId = table.Column<int>(nullable: true),
                    LAST_DAY_UPDATED = table.Column<string>(nullable: true),
                    SORT_KEY = table.Column<string>(nullable: true),
                    SORT_KEY_LAST_MOD = table.Column<string>(nullable: true),
                    DELISTED_ON = table.Column<DateTime>(nullable: false),
                    DELISTED_ONSpecified = table.Column<bool>(nullable: false),
                    CONSOLIDATED_LISTMId = table.Column<int>(nullable: true),
                    ENTITIESMId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ENTITYM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ENTITYM_CONSOLIDATED_LISTM_CONSOLIDATED_LISTMId",
                        column: x => x.CONSOLIDATED_LISTMId,
                        principalTable: "CONSOLIDATED_LISTM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ENTITYM_ENTITIESM_ENTITIESMId",
                        column: x => x.ENTITIESMId,
                        principalTable: "ENTITIESM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ENTITYM_LIST_TYPEM_LIST_TYPEId",
                        column: x => x.LIST_TYPEId,
                        principalTable: "LIST_TYPEM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "INDIVIDUALM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DATAID = table.Column<string>(nullable: true),
                    VERSIONNUM = table.Column<string>(nullable: true),
                    FIRST_NAME = table.Column<string>(nullable: true),
                    SECOND_NAME = table.Column<string>(nullable: true),
                    THIRD_NAME = table.Column<string>(nullable: true),
                    FOURTH_NAME = table.Column<string>(nullable: true),
                    UN_LIST_TYPE = table.Column<string>(nullable: true),
                    REFERENCE_NUMBER = table.Column<string>(nullable: true),
                    LISTED_ON = table.Column<DateTime>(nullable: false),
                    GENDER = table.Column<string>(nullable: true),
                    SUBMITTED_BY = table.Column<string>(nullable: true),
                    NAME_ORIGINAL_SCRIPT = table.Column<string>(nullable: true),
                    COMMENTS1 = table.Column<string>(nullable: true),
                    NATIONALITY2 = table.Column<string>(nullable: true),
                    TITLE = table.Column<string>(nullable: true),
                    DESIGNATION = table.Column<string>(nullable: true),
                    NATIONALITY = table.Column<string>(nullable: true),
                    LIST_TYPEId = table.Column<int>(nullable: true),
                    LAST_DAY_UPDATED = table.Column<string>(nullable: true),
                    SORT_KEY = table.Column<string>(nullable: true),
                    SORT_KEY_LAST_MOD = table.Column<string>(nullable: true),
                    DELISTED_ON = table.Column<DateTime>(nullable: false),
                    DELISTED_ONSpecified = table.Column<bool>(nullable: false),
                    CONSOLIDATED_LISTMId = table.Column<int>(nullable: true),
                    INDIVIDUALSMId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INDIVIDUALM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_INDIVIDUALM_CONSOLIDATED_LISTM_CONSOLIDATED_LISTMId",
                        column: x => x.CONSOLIDATED_LISTMId,
                        principalTable: "CONSOLIDATED_LISTM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_INDIVIDUALM_INDIVIDUALSM_INDIVIDUALSMId",
                        column: x => x.INDIVIDUALSMId,
                        principalTable: "INDIVIDUALSM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_INDIVIDUALM_LIST_TYPEM_LIST_TYPEId",
                        column: x => x.LIST_TYPEId,
                        principalTable: "LIST_TYPEM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ENTITY_ADDRESSM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    STREET = table.Column<string>(nullable: true),
                    CITY = table.Column<string>(nullable: true),
                    STATE_PROVINCE = table.Column<string>(nullable: true),
                    ZIP_CODE = table.Column<string>(nullable: true),
                    COUNTRY = table.Column<string>(nullable: true),
                    NOTE = table.Column<string>(nullable: true),
                    ENTITYMId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ENTITY_ADDRESSM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ENTITY_ADDRESSM_ENTITYM_ENTITYMId",
                        column: x => x.ENTITYMId,
                        principalTable: "ENTITYM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ENTITY_ALIASM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    QUALITY = table.Column<string>(nullable: true),
                    ALIAS_NAME = table.Column<string>(nullable: true),
                    ENTITYMId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ENTITY_ALIASM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ENTITY_ALIASM_ENTITYM_ENTITYMId",
                        column: x => x.ENTITYMId,
                        principalTable: "ENTITYM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "INDIVIDUAL_ADDRESSM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    STREET = table.Column<string>(nullable: true),
                    CITY = table.Column<string>(nullable: true),
                    STATE_PROVINCE = table.Column<string>(nullable: true),
                    ZIP_CODE = table.Column<string>(nullable: true),
                    COUNTRY = table.Column<string>(nullable: true),
                    NOTE = table.Column<string>(nullable: true),
                    INDIVIDUALMId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INDIVIDUAL_ADDRESSM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_INDIVIDUAL_ADDRESSM_INDIVIDUALM_INDIVIDUALMId",
                        column: x => x.INDIVIDUALMId,
                        principalTable: "INDIVIDUALM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "INDIVIDUAL_ALIASM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    QUALITY = table.Column<string>(nullable: true),
                    ALIAS_NAME = table.Column<string>(nullable: true),
                    DATE_OF_BIRTH = table.Column<string>(nullable: true),
                    CITY_OF_BIRTH = table.Column<string>(nullable: true),
                    COUNTRY_OF_BIRTH = table.Column<string>(nullable: true),
                    NOTE = table.Column<string>(nullable: true),
                    INDIVIDUALMId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INDIVIDUAL_ALIASM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_INDIVIDUAL_ALIASM_INDIVIDUALM_INDIVIDUALMId",
                        column: x => x.INDIVIDUALMId,
                        principalTable: "INDIVIDUALM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "INDIVIDUAL_DATE_OF_BIRTHM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TYPE_OF_DATE = table.Column<string>(nullable: true),
                    NOTE = table.Column<string>(nullable: true),
                    Items = table.Column<string>(nullable: true),
                    ItemsElementName = table.Column<string>(nullable: true),
                    INDIVIDUALMId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INDIVIDUAL_DATE_OF_BIRTHM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_INDIVIDUAL_DATE_OF_BIRTHM_INDIVIDUALM_INDIVIDUALMId",
                        column: x => x.INDIVIDUALMId,
                        principalTable: "INDIVIDUALM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "INDIVIDUAL_DOCUMENTM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TYPE_OF_DOCUMENT = table.Column<string>(nullable: true),
                    TYPE_OF_DOCUMENT2 = table.Column<string>(nullable: true),
                    NUMBER = table.Column<string>(nullable: true),
                    ISSUING_COUNTRY = table.Column<string>(nullable: true),
                    DATE_OF_ISSUE = table.Column<string>(nullable: true),
                    CITY_OF_ISSUE = table.Column<string>(nullable: true),
                    COUNTRY_OF_ISSUE = table.Column<string>(nullable: true),
                    NOTE = table.Column<string>(nullable: true),
                    INDIVIDUALMId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INDIVIDUAL_DOCUMENTM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_INDIVIDUAL_DOCUMENTM_INDIVIDUALM_INDIVIDUALMId",
                        column: x => x.INDIVIDUALMId,
                        principalTable: "INDIVIDUALM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "INDIVIDUAL_PLACE_OF_BIRTHM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CITY = table.Column<string>(nullable: true),
                    STATE_PROVINCE = table.Column<string>(nullable: true),
                    COUNTRY = table.Column<string>(nullable: true),
                    NOTE = table.Column<string>(nullable: true),
                    INDIVIDUALMId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INDIVIDUAL_PLACE_OF_BIRTHM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_INDIVIDUAL_PLACE_OF_BIRTHM_INDIVIDUALM_INDIVIDUALMId",
                        column: x => x.INDIVIDUALMId,
                        principalTable: "INDIVIDUALM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ENTITY_ADDRESSM_ENTITYMId",
                table: "ENTITY_ADDRESSM",
                column: "ENTITYMId");

            migrationBuilder.CreateIndex(
                name: "IX_ENTITY_ALIASM_ENTITYMId",
                table: "ENTITY_ALIASM",
                column: "ENTITYMId");

            migrationBuilder.CreateIndex(
                name: "IX_ENTITYM_CONSOLIDATED_LISTMId",
                table: "ENTITYM",
                column: "CONSOLIDATED_LISTMId");

            migrationBuilder.CreateIndex(
                name: "IX_ENTITYM_ENTITIESMId",
                table: "ENTITYM",
                column: "ENTITIESMId");

            migrationBuilder.CreateIndex(
                name: "IX_ENTITYM_LIST_TYPEId",
                table: "ENTITYM",
                column: "LIST_TYPEId");

            migrationBuilder.CreateIndex(
                name: "IX_INDIVIDUAL_ADDRESSM_INDIVIDUALMId",
                table: "INDIVIDUAL_ADDRESSM",
                column: "INDIVIDUALMId");

            migrationBuilder.CreateIndex(
                name: "IX_INDIVIDUAL_ALIASM_INDIVIDUALMId",
                table: "INDIVIDUAL_ALIASM",
                column: "INDIVIDUALMId");

            migrationBuilder.CreateIndex(
                name: "IX_INDIVIDUAL_DATE_OF_BIRTHM_INDIVIDUALMId",
                table: "INDIVIDUAL_DATE_OF_BIRTHM",
                column: "INDIVIDUALMId");

            migrationBuilder.CreateIndex(
                name: "IX_INDIVIDUAL_DOCUMENTM_INDIVIDUALMId",
                table: "INDIVIDUAL_DOCUMENTM",
                column: "INDIVIDUALMId");

            migrationBuilder.CreateIndex(
                name: "IX_INDIVIDUAL_PLACE_OF_BIRTHM_INDIVIDUALMId",
                table: "INDIVIDUAL_PLACE_OF_BIRTHM",
                column: "INDIVIDUALMId");

            migrationBuilder.CreateIndex(
                name: "IX_INDIVIDUALM_CONSOLIDATED_LISTMId",
                table: "INDIVIDUALM",
                column: "CONSOLIDATED_LISTMId");

            migrationBuilder.CreateIndex(
                name: "IX_INDIVIDUALM_INDIVIDUALSMId",
                table: "INDIVIDUALM",
                column: "INDIVIDUALSMId");

            migrationBuilder.CreateIndex(
                name: "IX_INDIVIDUALM_LIST_TYPEId",
                table: "INDIVIDUALM",
                column: "LIST_TYPEId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DESIGNATIONM");

            migrationBuilder.DropTable(
                name: "ENTITY_ADDRESSM");

            migrationBuilder.DropTable(
                name: "ENTITY_ALIASM");

            migrationBuilder.DropTable(
                name: "INDIVIDUAL_ADDRESSM");

            migrationBuilder.DropTable(
                name: "INDIVIDUAL_ALIASM");

            migrationBuilder.DropTable(
                name: "INDIVIDUAL_DATE_OF_BIRTHM");

            migrationBuilder.DropTable(
                name: "INDIVIDUAL_DOCUMENTM");

            migrationBuilder.DropTable(
                name: "INDIVIDUAL_PLACE_OF_BIRTHM");

            migrationBuilder.DropTable(
                name: "LAST_DAY_UPDATEDM");

            migrationBuilder.DropTable(
                name: "NATIONALITYM");

            migrationBuilder.DropTable(
                name: "TITLEM");

            migrationBuilder.DropTable(
                name: "ENTITYM");

            migrationBuilder.DropTable(
                name: "INDIVIDUALM");

            migrationBuilder.DropTable(
                name: "ENTITIESM");

            migrationBuilder.DropTable(
                name: "CONSOLIDATED_LISTM");

            migrationBuilder.DropTable(
                name: "INDIVIDUALSM");

            migrationBuilder.DropTable(
                name: "LIST_TYPEM");
        }
    }
}
