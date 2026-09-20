using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CmsApi.Server.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class appBusinessModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Apps",
                schema: "cms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    TemplateId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Apps_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalSchema: "cms",
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Apps_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "cms",
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Apps_Templates_TemplateId",
                        column: x => x.TemplateId,
                        principalSchema: "cms",
                        principalTable: "Templates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Platforms",
                schema: "cms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platforms", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Apps_LanguageId",
                schema: "cms",
                table: "Apps",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Apps_ProjectId",
                schema: "cms",
                table: "Apps",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Apps_TemplateId",
                schema: "cms",
                table: "Apps",
                column: "TemplateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Apps",
                schema: "cms");

            migrationBuilder.DropTable(
                name: "Platforms",
                schema: "cms");
        }
    }
}
