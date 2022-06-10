using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StackOverflowEntities.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestionModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastEdited = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReplyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Reply_QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionModels_QuestionModels_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "QuestionModels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_QuestionModels_QuestionModels_Reply_QuestionId",
                        column: x => x.Reply_QuestionId,
                        principalTable: "QuestionModels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_QuestionModels_QuestionModels_ReplyId",
                        column: x => x.ReplyId,
                        principalTable: "QuestionModels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_QuestionModels_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionTag",
                columns: table => new
                {
                    QuestionsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionTag", x => new { x.QuestionsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_QuestionTag_QuestionModels_QuestionsId",
                        column: x => x.QuestionsId,
                        principalTable: "QuestionModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionModels_AuthorId",
                table: "QuestionModels",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionModels_QuestionId",
                table: "QuestionModels",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionModels_Reply_QuestionId",
                table: "QuestionModels",
                column: "Reply_QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionModels_ReplyId",
                table: "QuestionModels",
                column: "ReplyId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionTag_TagsId",
                table: "QuestionTag",
                column: "TagsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionTag");

            migrationBuilder.DropTable(
                name: "QuestionModels");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
