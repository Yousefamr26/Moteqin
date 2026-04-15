using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moteqin.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixFeedbackNullables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "status",
                table: "Feedbacks",
                newName: "Status");

            migrationBuilder.AlterColumn<string>(
                name: "AudioUrl",
                table: "Feedbacks",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Feedbacks",
                newName: "status");

            migrationBuilder.AlterColumn<string>(
                name: "AudioUrl",
                table: "Feedbacks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
