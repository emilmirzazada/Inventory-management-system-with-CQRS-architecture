using Microsoft.EntityFrameworkCore.Migrations;

namespace Sintra.Infrastructure.Persistence.Migrations
{
    public partial class orderandtransactionispaid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "Transactions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "Orders",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "Orders");
        }
    }
}
