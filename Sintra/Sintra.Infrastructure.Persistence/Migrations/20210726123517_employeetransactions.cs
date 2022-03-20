using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sintra.Infrastructure.Persistence.Migrations
{
    public partial class employeetransactions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeBalanceTransactions",
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
                    table.PrimaryKey("PK_EmployeeBalanceTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeBalanceTransactions_Users_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeBalanceTransactions_Users_RecieverEmployeeId",
                        column: x => x.RecieverEmployeeId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeBonusTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<string>(nullable: true),
                    PayerEmployeeId = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PayDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeBonusTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeBonusTransactions_Users_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeBonusTransactions_Users_PayerEmployeeId",
                        column: x => x.PayerEmployeeId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeBalanceTransactions_EmployeeId",
                table: "EmployeeBalanceTransactions",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeBalanceTransactions_RecieverEmployeeId",
                table: "EmployeeBalanceTransactions",
                column: "RecieverEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeBonusTransactions_EmployeeId",
                table: "EmployeeBonusTransactions",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeBonusTransactions_PayerEmployeeId",
                table: "EmployeeBonusTransactions",
                column: "PayerEmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeBalanceTransactions");

            migrationBuilder.DropTable(
                name: "EmployeeBonusTransactions");
        }
    }
}
