using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChoucairApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "system");

            migrationBuilder.CreateTable(
                name: "Status",
                schema: "system",
                columns: table => new
                {
                    Status_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status_Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Status_ID);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                schema: "system",
                columns: table => new
                {
                    Task_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Task_Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Task_Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Task_EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Task_StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Task_ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Status",
                schema: "system");

            migrationBuilder.DropTable(
                name: "Tasks",
                schema: "system");
        }
    }
}
