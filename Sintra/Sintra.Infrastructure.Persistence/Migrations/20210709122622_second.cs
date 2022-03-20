using Microsoft.EntityFrameworkCore.Migrations;

namespace Sintra.Infrastructure.Persistence.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreditCalls_CreditCallStatuses_StatusId",
                table: "CreditCalls");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpirationCalls_ExpirationCallStatuses_StatusId",
                table: "ExpirationCalls");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_NotificationStatuses_StatusId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_NotificationTypes_TypeId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_NotificationUsers_NotificationUserStatuses_StatusId",
                table: "NotificationUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductType_ProductTypeId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTransfers_TransferStatus_TransferStatusId",
                table: "ProductTransfers");

            migrationBuilder.DropTable(
                name: "CreditCallStatuses");

            migrationBuilder.DropTable(
                name: "ExpirationCallStatuses");

            migrationBuilder.DropTable(
                name: "NotificationStatuses");

            migrationBuilder.DropTable(
                name: "NotificationTypes");

            migrationBuilder.DropTable(
                name: "NotificationUserStatuses");

            migrationBuilder.DropTable(
                name: "ProductType");

            migrationBuilder.DropTable(
                name: "TransferStatus");

            migrationBuilder.DropIndex(
                name: "IX_ProductTransfers_TransferStatusId",
                table: "ProductTransfers");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductTypeId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_NotificationUsers_StatusId",
                table: "NotificationUsers");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_StatusId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_TypeId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_ExpirationCalls_StatusId",
                table: "ExpirationCalls");

            migrationBuilder.DropIndex(
                name: "IX_CreditCalls_StatusId",
                table: "CreditCalls");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CreditCallStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCallStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExpirationCallStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpirationCallStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationUserStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationUserStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransferStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferStatus", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductTransfers_TransferStatusId",
                table: "ProductTransfers",
                column: "TransferStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductTypeId",
                table: "Products",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationUsers_StatusId",
                table: "NotificationUsers",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_StatusId",
                table: "Notifications",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_TypeId",
                table: "Notifications",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpirationCalls_StatusId",
                table: "ExpirationCalls",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditCalls_StatusId",
                table: "CreditCalls",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_CreditCalls_CreditCallStatuses_StatusId",
                table: "CreditCalls",
                column: "StatusId",
                principalTable: "CreditCallStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpirationCalls_ExpirationCallStatuses_StatusId",
                table: "ExpirationCalls",
                column: "StatusId",
                principalTable: "ExpirationCallStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_NotificationStatuses_StatusId",
                table: "Notifications",
                column: "StatusId",
                principalTable: "NotificationStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_NotificationTypes_TypeId",
                table: "Notifications",
                column: "TypeId",
                principalTable: "NotificationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationUsers_NotificationUserStatuses_StatusId",
                table: "NotificationUsers",
                column: "StatusId",
                principalTable: "NotificationUserStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductType_ProductTypeId",
                table: "Products",
                column: "ProductTypeId",
                principalTable: "ProductType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTransfers_TransferStatus_TransferStatusId",
                table: "ProductTransfers",
                column: "TransferStatusId",
                principalTable: "TransferStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
