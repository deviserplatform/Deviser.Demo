using Microsoft.EntityFrameworkCore.Migrations;

namespace Deviser.Demo.Blog.Migrations.MySql
{
    public partial class BlogSchema_000001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "demo_blog_Post");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "demo_blog_Post",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: false);
        }
    }
}
