using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fiorella.Migrations
{
    public partial class CreateExpertSlideTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.CreateTable(
                name: "ExpertSlides",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Profession = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoftDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpertSlides", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpertSlides");

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "Date", "Description", "Image", "SoftDeleted", "Title" },
                values: new object[] { 1, new DateTime(2024, 5, 8, 18, 19, 25, 305, DateTimeKind.Local).AddTicks(7140), "Desc1", "blog-feature-image-1.jpg", false, "Blog1" });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "Date", "Description", "Image", "SoftDeleted", "Title" },
                values: new object[] { 2, new DateTime(2024, 5, 8, 18, 19, 25, 305, DateTimeKind.Local).AddTicks(7157), "Desc2", "blog-feature-image-3.jpg", false, "Blog2" });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "Date", "Description", "Image", "SoftDeleted", "Title" },
                values: new object[] { 3, new DateTime(2024, 5, 8, 18, 19, 25, 305, DateTimeKind.Local).AddTicks(7158), "Desc3", "blog-feature-image-4.jpg", false, "Blog3" });
        }
    }
}
