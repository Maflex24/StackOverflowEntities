using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StackOverflowEntities.Migrations
{
    public partial class QuestionTitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "QuestionModels",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "Title",
                table: "QuestionModels");
        }
    }
}
