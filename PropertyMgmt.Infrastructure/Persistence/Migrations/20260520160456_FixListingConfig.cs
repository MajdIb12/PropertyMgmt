using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyMgmt.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixListingConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "22a04e11-afb3-422b-89b8-0d1308553ca5", new DateTime(2026, 5, 20, 15, 3, 34, 458, DateTimeKind.Utc).AddTicks(736), "AQAAAAIAAYagAAAAEM5ERzOYdVsLZqMcJuyliQuoiueqIvXn2cdsYl6WlKgV3uytiTAQv88pxMs42wZ9TQ==", "88b8ddb5-e20b-46f9-9e11-28718f27ad29" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f6c78196-7f38-4913-8c8e-09bfde5d11b7", new DateTime(2026, 5, 20, 15, 3, 34, 497, DateTimeKind.Utc).AddTicks(3320), "AQAAAAIAAYagAAAAELlsQPyMK9gC4Dvxe/LdlsVfmCkZyzYTfF1ZTAy5xRfB4Uw6geHDk8rQ8L0L9Uhd8A==", "bfed07a4-9119-452e-88c5-a9295ba97fbd" });
        }
    }
}
