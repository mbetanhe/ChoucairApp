using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChoucairApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CampoUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserID",
                schema: "system",
                table: "Tasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserID",
                schema: "system",
                table: "Tasks");
        }
    }
}
