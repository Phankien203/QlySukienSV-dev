using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QlySukienSV.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SuKiens",
                keyColumn: "Id",
                keyValue: 1,
                column: "TrangThai",
                value: "HoanThanh");

            migrationBuilder.UpdateData(
                table: "SuKiens",
                keyColumn: "Id",
                keyValue: 2,
                column: "TrangThai",
                value: "HoanThanh");

            migrationBuilder.UpdateData(
                table: "SuKiens",
                keyColumn: "Id",
                keyValue: 3,
                column: "TrangThai",
                value: "HoanThanh");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SuKiens",
                keyColumn: "Id",
                keyValue: 1,
                column: "TrangThai",
                value: "SapDienRa");

            migrationBuilder.UpdateData(
                table: "SuKiens",
                keyColumn: "Id",
                keyValue: 2,
                column: "TrangThai",
                value: "SapDienRa");

            migrationBuilder.UpdateData(
                table: "SuKiens",
                keyColumn: "Id",
                keyValue: 3,
                column: "TrangThai",
                value: "SapDienRa");
        }
    }
}
