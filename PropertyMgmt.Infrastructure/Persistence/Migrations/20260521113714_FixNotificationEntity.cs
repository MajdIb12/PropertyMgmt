using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyMgmt.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixNotificationEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Notifications",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "TenantId",
                table: "Notifications",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "Notifications",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<bool>(
                name: "IsRead",
                table: "Notifications",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2e75b17e-6fc7-4921-a124-ce5118654185", new DateTime(2026, 5, 21, 11, 37, 13, 955, DateTimeKind.Utc).AddTicks(4361), "AQAAAAIAAYagAAAAEKoo1QdVApHbgOd3+eiScoHPEakWUX9SpD8w4ADq+usWWtFwO3yYyd+PQmLoGNCIzA==", "5876264d-72c1-4d03-ba4c-429e59bafd2b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "aec5e4b8-5b20-4673-b324-04e29653f895", new DateTime(2026, 5, 21, 11, 37, 14, 13, DateTimeKind.Utc).AddTicks(5608), "AQAAAAIAAYagAAAAEDZ782s+Yy71XhlIAEssk94QmLxh+El9ICp1SVBmGbHqV5LFxTfHzCNhxWv9kJdQug==", "214b6830-8acc-4451-9046-cc41e286a825" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "04e14b51-96b3-4a4d-b322-8fe640ac16bb", new DateTime(2026, 5, 21, 11, 37, 14, 53, DateTimeKind.Utc).AddTicks(361), "AQAAAAIAAYagAAAAECLfo5Cuux0eLnCyyu1L9T/4/AADkV4ZFfaQmoC076zfHRrk/kj5RRaItU6YqWC6Og==", "5a1212d2-b78a-4433-8ced-40706a543b54" });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_UserId",
                table: "Notifications",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AspNetUsers_UserId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "TenantId",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<bool>(
                name: "IsRead",
                table: "Notifications",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0d98104f-a639-4385-aba6-2d23b8b5e235", new DateTime(2026, 5, 20, 16, 4, 55, 493, DateTimeKind.Utc).AddTicks(865), "AQAAAAIAAYagAAAAEHeND6FzdLcsBkg3xy8Cz5nDtZrMvwy+DbE9vLYttx4g0o04Tb3FGKT65E9KmDim+w==", "a3d4c558-390d-4c43-9bbe-99dccbe3a718" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8a5f93dd-b107-4bc3-b594-467b17cb61ba", new DateTime(2026, 5, 20, 16, 4, 55, 547, DateTimeKind.Utc).AddTicks(5842), "AQAAAAIAAYagAAAAEHTk/y9FOQ9FX6aRofm/BHRz6U5YCj3kyCnaANG3Hhiap4aJpEeAaWktaFIpqGRDwA==", "5f6cfb3d-894c-4c1c-b71b-d8e7c30fabb2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2002ad1e-bfe0-4b79-9b56-ad0d0d955888", new DateTime(2026, 5, 20, 16, 4, 55, 586, DateTimeKind.Utc).AddTicks(4770), "AQAAAAIAAYagAAAAEF03kjY4Wr1WAHcK/6RF0hRVbBylBm8tsXcPSwVrh+WeVCWOI+YcinlW3kb03Rk2jA==", "95f1559d-f559-495e-98ac-b8f05f50d030" });
        }
    }
}
