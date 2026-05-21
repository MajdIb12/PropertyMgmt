using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyMgmt.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUserSeedData2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8c8c2e03-4699-404a-a65f-3b6765873df2", new DateTime(2026, 5, 20, 15, 3, 34, 400, DateTimeKind.Utc).AddTicks(254), "AQAAAAIAAYagAAAAEK6/5p8cPaNXtvmfJTUZD2JziUPgpMrZZyERspros0eG7quqoZsMSBQ5qXZ6B4+HwA==", "09fe3355-dc82-4c2b-aea7-212a0db57fe3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp", "TenantId" },
                values: new object[] { "22a04e11-afb3-422b-89b8-0d1308553ca5", new DateTime(2026, 5, 20, 15, 3, 34, 458, DateTimeKind.Utc).AddTicks(736), "AQAAAAIAAYagAAAAEM5ERzOYdVsLZqMcJuyliQuoiueqIvXn2cdsYl6WlKgV3uytiTAQv88pxMs42wZ9TQ==", "88b8ddb5-e20b-46f9-9e11-28718f27ad29", "A1B2C3D4-E5F6-47A8-9B0C-1D2E3F4G5H6I" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp", "TenantId" },
                values: new object[] { "f6c78196-7f38-4913-8c8e-09bfde5d11b7", new DateTime(2026, 5, 20, 15, 3, 34, 497, DateTimeKind.Utc).AddTicks(3320), "AQAAAAIAAYagAAAAELlsQPyMK9gC4Dvxe/LdlsVfmCkZyzYTfF1ZTAy5xRfB4Uw6geHDk8rQ8L0L9Uhd8A==", "bfed07a4-9119-452e-88c5-a9295ba97fbd", "A1B2C3D4-E5F6-47A8-9B0C-1D2E3F4G5H6I" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dbc472ef-8afe-4f5a-a1fb-f0de58004ee7", new DateTime(2026, 5, 20, 14, 44, 52, 29, DateTimeKind.Utc).AddTicks(3083), "AQAAAAIAAYagAAAAEOXcoTWkmX/KVktXpJKPfJTgI9b6p9LBdh1Lsql2p53vg4QlvF3oNyXyYoltjMfC+Q==", "0d558bb6-0f54-4543-8cde-12b3ba0a2373" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp", "TenantId" },
                values: new object[] { "55eb925c-5bec-4efe-bed8-fd71b953beca", new DateTime(2026, 5, 20, 14, 44, 52, 82, DateTimeKind.Utc).AddTicks(8612), "AQAAAAIAAYagAAAAEK+0Dl/HEEsWfpqpAfJVRooElBpZCoAb3n7hkXmz6Z71ChBjuCvmX747fzPdGAa+yQ==", "2c89b12c-3ec4-4750-aea4-8e150158220f", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp", "TenantId" },
                values: new object[] { "7def33d7-52e0-4cc5-b5ad-a6b450406778", new DateTime(2026, 5, 20, 14, 44, 52, 123, DateTimeKind.Utc).AddTicks(278), "AQAAAAIAAYagAAAAEBqoZ9m3IYnrgK9DzntPGRVpWesmej1tB7Io/DXxmLUfevYwMCP55vV536iIUlr4eQ==", "7f398034-b386-4772-b2f9-4fb1fba32961", null });
        }
    }
}
