using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeHeard.Migrations
{
    public partial class AddActivityResultCreatedTimestamp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityResults_UserProfiles_UserProfileId",
                table: "ActivityResults");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserProfileId",
                table: "ActivityResults",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "ActivityResults",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityResults_UserProfiles_UserProfileId",
                table: "ActivityResults",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityResults_UserProfiles_UserProfileId",
                table: "ActivityResults");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ActivityResults");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserProfileId",
                table: "ActivityResults",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityResults_UserProfiles_UserProfileId",
                table: "ActivityResults",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
