using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StackOverflowEntities.Migrations
{
    public partial class NonNullableQuestionIdInComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionModels_QuestionModels_QuestionId",
                table: "QuestionModels");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionModels_QuestionModels_QuestionId",
                table: "QuestionModels",
                column: "QuestionId",
                principalTable: "QuestionModels",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionModels_QuestionModels_QuestionId",
                table: "QuestionModels");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionModels_QuestionModels_QuestionId",
                table: "QuestionModels",
                column: "QuestionId",
                principalTable: "QuestionModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
