using Microsoft.EntityFrameworkCore.Migrations;

namespace Sintra.Infrastructure.Persistence.Migrations
{
    public partial class AddCommenttoService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Services",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Services");
        }
    }
}
