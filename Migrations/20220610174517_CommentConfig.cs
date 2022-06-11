using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StackOverflowEntities.Migrations
{
    public partial class CommentConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionModels_QuestionModels_ReplyId",
                table: "QuestionsRepliesComments");

            migrationBuilder.DropIndex(
                name: "IX_QuestionModels_ReplyId",
                table: "QuestionsRepliesComments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_QuestionModels_ReplyId",
                table: "QuestionsRepliesComments",
                column: "ReplyId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionModels_QuestionModels_ReplyId",
                table: "QuestionsRepliesComments",
                column: "ReplyId",
                principalTable: "QuestionsRepliesComments",
                principalColumn: "Id");
        }
    }
}
