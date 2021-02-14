﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Deviser.Demo.Blog.Migrations.SqlServer
{
    public partial class InitalSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "demo_blog_Category",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_demo_blog_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "demo_blog_Tag",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_demo_blog_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "demo_blog_Post",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thumbnail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCommentEnabled = table.Column<bool>(type: "bit", nullable: false),
                    BlogId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_demo_blog_Post", x => x.Id);
                    table.ForeignKey(
                        name: "FK_demo_blog_Post_demo_blog_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "demo_blog_Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "demo_blog_Comment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_demo_blog_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_demo_blog_Comment_demo_blog_Post_PostId",
                        column: x => x.PostId,
                        principalTable: "demo_blog_Post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "demo_blog_PostTag",
                columns: table => new
                {
                    PostsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_demo_blog_PostTag", x => new { x.PostsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_demo_blog_PostTag_demo_blog_Post_PostsId",
                        column: x => x.PostsId,
                        principalTable: "demo_blog_Post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_demo_blog_PostTag_demo_blog_Tag_TagsId",
                        column: x => x.TagsId,
                        principalTable: "demo_blog_Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_demo_blog_Comment_PostId",
                table: "demo_blog_Comment",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_demo_blog_Post_CategoryId",
                table: "demo_blog_Post",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_demo_blog_PostTag_TagsId",
                table: "demo_blog_PostTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_demo_blog_Tag_Name",
                table: "demo_blog_Tag",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "demo_blog_Comment");

            migrationBuilder.DropTable(
                name: "demo_blog_PostTag");

            migrationBuilder.DropTable(
                name: "demo_blog_Post");

            migrationBuilder.DropTable(
                name: "demo_blog_Tag");

            migrationBuilder.DropTable(
                name: "demo_blog_Category");
        }
    }
}
