using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyMgmt.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddChatEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Conversations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    TenantId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConversationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SenderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    TenantId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMessages_AspNetUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatMessages_Conversations_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "Conversations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_ConversationId_CreatedAt",
                table: "ChatMessages",
                columns: new[] { "ConversationId", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_SenderId",
                table: "ChatMessages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_TenantId_BookingId",
                table: "Conversations",
                columns: new[] { "TenantId", "BookingId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatMessages");

            migrationBuilder.DropTable(
                name: "Conversations");

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
        }
    }
}
