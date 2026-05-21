using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PropertyMgmt.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUserSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dbc472ef-8afe-4f5a-a1fb-f0de58004ee7", new DateTime(2026, 5, 20, 14, 44, 52, 29, DateTimeKind.Utc).AddTicks(3083), "AQAAAAIAAYagAAAAEOXcoTWkmX/KVktXpJKPfJTgI9b6p9LBdh1Lsql2p53vg4QlvF3oNyXyYoltjMfC+Q==", "0d558bb6-0f54-4543-8cde-12b3ba0a2373" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "DeletedAt", "Email", "EmailConfirmed", "FirstName", "FullName", "IsDeleted", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TenantId", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000002"), 0, "55eb925c-5bec-4efe-bed8-fd71b953beca", new DateTime(2026, 5, 20, 14, 44, 52, 82, DateTimeKind.Utc).AddTicks(8612), null, "usertest@propertymgmt.com", true, "User", "User test", false, "test", false, null, "USERTEST@PROPERTYMGMT.COM", "USERTEST", "AQAAAAIAAYagAAAAEK+0Dl/HEEsWfpqpAfJVRooElBpZCoAb3n7hkXmz6Z71ChBjuCvmX747fzPdGAa+yQ==", null, false, "2c89b12c-3ec4-4750-aea4-8e150158220f", null, false, "usertest", "User" },
                    { new Guid("00000000-0000-0000-0000-000000000003"), 0, "7def33d7-52e0-4cc5-b5ad-a6b450406778", new DateTime(2026, 5, 20, 14, 44, 52, 123, DateTimeKind.Utc).AddTicks(278), null, "ownertest@propertymgmt.com", true, "Owner", "Owner test", false, "test", false, null, "OWNERTEST@PROPERTYMGMT.COM", "OWNERTEST", "AQAAAAIAAYagAAAAEBqoZ9m3IYnrgK9DzntPGRVpWesmej1tB7Io/DXxmLUfevYwMCP55vV536iIUlr4eQ==", null, false, "7f398034-b386-4772-b2f9-4fb1fba32961", null, false, "ownertest", "User" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fee369f2-7b35-41ea-a976-616c22adfede", new DateTime(2026, 5, 20, 9, 43, 53, 749, DateTimeKind.Utc).AddTicks(6618), "AQAAAAIAAYagAAAAEPdiaQyaOm67oD5oFgWUhq2fH+sKhg5qwxGjCmIOYuIKWwAEDfCmiNUij77ILUgiTg==", "24f8d4ce-122e-49b0-9fcc-1aaed5cce4a6" });
        }
    }
}
