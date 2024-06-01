using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fiorella.Migrations
{
    public partial class CreateSurpriseSurpriseListTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Surprise",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surprise", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SurpriseLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SurpriseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurpriseLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurpriseLists_Surprise_SurpriseId",
                        column: x => x.SurpriseId,
                        principalTable: "Surprise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SurpriseLists_SurpriseId",
                table: "SurpriseLists",
                column: "SurpriseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SurpriseLists");

            migrationBuilder.DropTable(
                name: "Surprise");
        }
    }
}
