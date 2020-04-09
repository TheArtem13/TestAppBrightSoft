using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "directoryObjects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    Budjet = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_directoryObjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ObjectVersions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    VersionType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObjectVersions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "directoryObjectVersions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DirectoryObjectId = table.Column<int>(nullable: true),
                    VersionId = table.Column<int>(nullable: true),
                    Value = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_directoryObjectVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_directoryObjectVersions_directoryObjects_DirectoryObjectId",
                        column: x => x.DirectoryObjectId,
                        principalTable: "directoryObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_directoryObjectVersions_ObjectVersions_VersionId",
                        column: x => x.VersionId,
                        principalTable: "ObjectVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_directoryObjectVersions_DirectoryObjectId",
                table: "directoryObjectVersions",
                column: "DirectoryObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_directoryObjectVersions_VersionId",
                table: "directoryObjectVersions",
                column: "VersionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "directoryObjectVersions");

            migrationBuilder.DropTable(
                name: "directoryObjects");

            migrationBuilder.DropTable(
                name: "ObjectVersions");
        }
    }
}
