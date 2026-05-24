using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyMgmt.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixConversationConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "Conversations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Conversations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_BookingId",
                table: "Conversations",
                column: "BookingId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_Bookings_BookingId",
                table: "Conversations",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_Bookings_BookingId",
                table: "Conversations");

            migrationBuilder.DropIndex(
                name: "IX_Conversations_BookingId",
                table: "Conversations");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Conversations");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Conversations");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5992ee5b-e5ba-4c70-84c5-0ba2193c391b", new DateTime(2026, 5, 21, 11, 51, 33, 447, DateTimeKind.Utc).AddTicks(2228), "AQAAAAIAAYagAAAAELtIZnFw/KPtKbcGtkCvqIkslML3tc2EUD7LrcmH5nGwZNTPYXRH+X8MqVSD3Q9/RQ==", "1000c527-34ad-43a0-9001-b7238bda696a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1e028cda-c694-4639-a5f4-4dec12e9e61e", new DateTime(2026, 5, 21, 11, 51, 33, 498, DateTimeKind.Utc).AddTicks(2649), "AQAAAAIAAYagAAAAEAFVBfrlI3VOq88SFn3ijNlo3+ElyG2s22TWxRUFZqt9rnz3Y5uIMx0Bg10Bd5zITA==", "06062478-d126-4f46-9dad-6835b4f646dd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "12582df9-d8b8-459f-902b-e640c3fe6bc1", new DateTime(2026, 5, 21, 11, 51, 33, 537, DateTimeKind.Utc).AddTicks(3050), "AQAAAAIAAYagAAAAEKVIW8ta80PW+nY+MTUbBkNS+UqdCDCuoFxwFO1X1jpEfYW38Tw4S5U6iTZBIkRTXQ==", "b7361d6a-6640-4739-9f35-f03c643fdf8b" });
        }
    }
}
