using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeHeard.Migrations
{
    public partial class AddUserProfileRecordingRecords : Migration
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
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "RecordingRecord",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    Chosen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Result = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordingRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecordingRecord_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecordingRecord_UserProfileId",
                table: "RecordingRecord",
                column: "UserProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityResults_UserProfiles_UserProfileId",
                table: "ActivityResults",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityResults_UserProfiles_UserProfileId",
                table: "ActivityResults");

            migrationBuilder.DropTable(
                name: "RecordingRecord");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserProfileId",
                table: "ActivityResults",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityResults_UserProfiles_UserProfileId",
                table: "ActivityResults",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id");
        }
    }
}
