using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyMgmt.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixEmailNullabity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AdminEmail",
                table: "Tenants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8ee238ec-7533-4a26-85b6-a797d5e40abb", new DateTime(2026, 5, 1, 11, 26, 9, 81, DateTimeKind.Utc).AddTicks(1088), "AQAAAAIAAYagAAAAEF7UvlB7Kn82fbjlkeT05uJUkHYKfATeR2Y3PpJG0M4y0NaURs7/XVGrvPSHtNLk7g==", "ae8d563f-61ae-4a37-94f6-f3e259f1eb98" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "AdminEmail",
                table: "Tenants",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "98d0e9ed-bb45-44eb-bc84-9d4a2b1da6a1", new DateTime(2026, 4, 30, 13, 53, 5, 548, DateTimeKind.Utc).AddTicks(7212), "AQAAAAIAAYagAAAAEKA+YVfx1CuPPs9OHp6BDrcNjtgXM/dXul9YdptngrV98HthevZ2JOoVf4iFQT2wuA==", "53097338-931c-4cc9-8122-698ee2e4cadd" });
        }
    }
}
