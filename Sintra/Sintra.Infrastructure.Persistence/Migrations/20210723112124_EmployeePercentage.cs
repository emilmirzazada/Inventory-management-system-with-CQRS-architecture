using Microsoft.EntityFrameworkCore.Migrations;

namespace Sintra.Infrastructure.Persistence.Migrations
{
    public partial class EmployeePercentage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "MaxAmount",
                table: "Users",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MinAmount",
                table: "Users",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Percentage",
                table: "Users",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxAmount",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MinAmount",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Percentage",
                table: "Users");
        }
    }
}
