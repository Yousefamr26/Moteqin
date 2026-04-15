using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moteqin.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ititl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupMembers_AspNetUsers_UserId",
                table: "GroupMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupMembers_Groups_GroupId",
                table: "GroupMembers");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Groups",
                newName: "Description");

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "Groups",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "GroupMembers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_CreatedByUserId",
                table: "Groups",
                column: "CreatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupMembers_AspNetUsers_UserId",
                table: "GroupMembers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupMembers_Groups_GroupId",
                table: "GroupMembers",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_AspNetUsers_CreatedByUserId",
                table: "Groups",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupMembers_AspNetUsers_UserId",
                table: "GroupMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupMembers_Groups_GroupId",
                table: "GroupMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_AspNetUsers_CreatedByUserId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_CreatedByUserId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "GroupMembers");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Groups",
                newName: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupMembers_AspNetUsers_UserId",
                table: "GroupMembers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupMembers_Groups_GroupId",
                table: "GroupMembers",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
