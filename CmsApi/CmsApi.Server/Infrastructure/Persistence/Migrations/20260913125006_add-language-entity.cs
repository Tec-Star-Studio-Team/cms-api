using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CmsApi.Server.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addlanguageentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Languages",
                schema: "cms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Languages",
                schema: "cms");
        }
    }
}
