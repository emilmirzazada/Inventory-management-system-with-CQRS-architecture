using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sintra.Infrastructure.Persistence.Migrations
{
    public partial class salam1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeCreditBalanceTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<string>(nullable: true),
                    RecieverEmployeeId = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RecieveDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeCreditBalanceTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeCreditBalanceTransactions_Users_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeCreditBalanceTransactions_Users_RecieverEmployeeId",
                        column: x => x.RecieverEmployeeId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeCreditBalanceTransactions_EmployeeId",
                table: "EmployeeCreditBalanceTransactions",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeCreditBalanceTransactions_RecieverEmployeeId",
                table: "EmployeeCreditBalanceTransactions",
                column: "RecieverEmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeCreditBalanceTransactions");
        }
    }
}
