using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fiorella.Migrations
{
    public partial class CreateBlogTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SoftDeleted",
                table: "SurpriseLists",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SoftDeleted",
                table: "Surprise",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SoftDeleted",
                table: "SlidersInfo",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SoftDeleted",
                table: "Sliders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SoftDeleted",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SoftDeleted",
                table: "ProductImages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SoftDeleted",
                table: "Positions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SoftDeleted",
                table: "Experts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SoftDeleted",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoftDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DropColumn(
                name: "SoftDeleted",
                table: "SurpriseLists");

            migrationBuilder.DropColumn(
                name: "SoftDeleted",
                table: "Surprise");

            migrationBuilder.DropColumn(
                name: "SoftDeleted",
                table: "SlidersInfo");

            migrationBuilder.DropColumn(
                name: "SoftDeleted",
                table: "Sliders");

            migrationBuilder.DropColumn(
                name: "SoftDeleted",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SoftDeleted",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "SoftDeleted",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "SoftDeleted",
                table: "Experts");

            migrationBuilder.DropColumn(
                name: "SoftDeleted",
                table: "Categories");
        }
    }
}
