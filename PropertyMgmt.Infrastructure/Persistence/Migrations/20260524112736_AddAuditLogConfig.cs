using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyMgmt.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditLogConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AuditLogs",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "AuditLogs",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TableName",
                table: "AuditLogs",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PrimaryKey",
                table: "AuditLogs",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4dab032e-96af-4b13-a1a7-8c9e501c47cc", new DateTime(2026, 5, 24, 11, 27, 35, 602, DateTimeKind.Utc).AddTicks(1931), "AQAAAAIAAYagAAAAEJb/Z4D5zOVmNOVJkNbIkvxeM/3dj59apN3mYL9KAn74zT2RSpx5Gu1P6qMoyjT3Ww==", "47672ce0-eca1-46b8-9508-76477acedfca" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "358e9310-d215-486e-a25d-752fa196a47e", new DateTime(2026, 5, 24, 11, 27, 35, 657, DateTimeKind.Utc).AddTicks(3245), "AQAAAAIAAYagAAAAEGVaqBrG8nU/WKXfDVtVvZu9aK0lstGaPPkdv7YBc0pFgITKmYqN6Ia7a71rIO2FXA==", "d5d32210-1461-4234-9fa6-1a4599b48102" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "276e6fd2-c3a8-4879-9ade-7c062b0c2ea4", new DateTime(2026, 5, 24, 11, 27, 35, 695, DateTimeKind.Utc).AddTicks(8579), "AQAAAAIAAYagAAAAEPHl59LeK0vDvpM0uJn0kquoLEm2yT6o7K5EkA0uLIxLGXcTPTzcR4iRJGvhYpPe0Q==", "beea8805-ff40-4914-8f9d-ee980ed741c8" });

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_CustomerId",
                table: "Conversations",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_OwnerId",
                table: "Conversations",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_AspNetUsers_CustomerId",
                table: "Conversations",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_AspNetUsers_OwnerId",
                table: "Conversations",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_AspNetUsers_CustomerId",
                table: "Conversations");

            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_AspNetUsers_OwnerId",
                table: "Conversations");

            migrationBuilder.DropIndex(
                name: "IX_Conversations_CustomerId",
                table: "Conversations");

            migrationBuilder.DropIndex(
                name: "IX_Conversations_OwnerId",
                table: "Conversations");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AuditLogs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "AuditLogs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TableName",
                table: "AuditLogs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PrimaryKey",
                table: "AuditLogs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6f47751b-6661-4f6e-89c8-b02cb12badef", new DateTime(2026, 5, 23, 14, 46, 30, 697, DateTimeKind.Utc).AddTicks(4653), "AQAAAAIAAYagAAAAEIn583u2oblkHnXmzUUtTk7trcoKhsnmSuJsjNQhUUEl3pNCrPlRtD6Oo+J/KH1AOA==", "978a8858-747c-4e0c-89d1-cb7dd43baac8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "898f23b1-17a1-465d-bca5-3bbf5a13e290", new DateTime(2026, 5, 23, 14, 46, 30, 749, DateTimeKind.Utc).AddTicks(3025), "AQAAAAIAAYagAAAAEN7os4I2a3gELLESM6JJWgNtBTTb72Nvq48/M/8y3XUTeih2+CNJTitGrUkkMNVmoQ==", "b42f8fc7-be6d-4b2c-9f6d-c2eab7c7a395" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "77049e33-89ec-48fa-a5e3-b840f824e84e", new DateTime(2026, 5, 23, 14, 46, 30, 787, DateTimeKind.Utc).AddTicks(6829), "AQAAAAIAAYagAAAAEAZKDlzAN/R6QMILp9B+BnmT7cK/6Ac3JN7pZ8fybzS3sneTnTemL2AtJx8DtRghWQ==", "91142d10-2d43-448d-b131-1b6df51b7c83" });
        }
    }
}
