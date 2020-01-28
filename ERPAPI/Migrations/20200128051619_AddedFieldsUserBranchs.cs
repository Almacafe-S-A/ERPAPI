using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AddedFieldsUserBranchs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "UserBranch",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedUser",
                table: "UserBranch",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "UserBranch",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedUser",
                table: "UserBranch",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "UserBranch");

            migrationBuilder.DropColumn(
                name: "CreatedUser",
                table: "UserBranch");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "UserBranch");

            migrationBuilder.DropColumn(
                name: "ModifiedUser",
                table: "UserBranch");
        }
    }
}
