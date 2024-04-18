using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BinaAz.Migrations
{
    /// <inheritdoc />
    public partial class addmiginit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "RoomCount",
                table: "Products",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8b592ae7-62bf-4656-9e67-3edc01cb3c58", "AQAAAAIAAYagAAAAEMA+JFIfFPX6WIL3wm/q1Ng1C8C8yQkglFEqPstk5AANj3zrIWbMuc1tqN2NlUHtfw==", "aa7a1e20-5a2a-4b9f-91a5-863110dfbadd" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoomCount",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9d809efd-14fd-4284-9ee0-b953de35d58c", "AQAAAAIAAYagAAAAEEInGkSDdGS+ps2LEvQEyhXx1D8tP7iFD4xEwtwOkHrpLK08Qz9ojU1/5bwaM4GyZQ==", "afb8d2cb-d751-4b18-b5a6-40607d1569e8" });
        }
    }
}
