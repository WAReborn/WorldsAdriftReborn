using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorldsAdriftServer.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Server = table.Column<string>(type: "TEXT", nullable: false),
                    serverIdentifier = table.Column<string>(type: "TEXT", nullable: false),
                    Cosmetics = table.Column<string>(type: "TEXT", nullable: false),
                    UniversalColors = table.Column<string>(type: "TEXT", nullable: false),
                    IsMale = table.Column<bool>(type: "INTEGER", nullable: false),
                    SeenIntro = table.Column<bool>(type: "INTEGER", nullable: false),
                    SkippedTutorial = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Characters");
        }
    }
}
