using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexesToYourEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CacheTables_Description",
                table: "CacheTables",
                column: "Description");

            migrationBuilder.CreateIndex(
                name: "IX_CacheTables_TableName",
                table: "CacheTables",
                column: "TableName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CacheTables_Description",
                table: "CacheTables");

            migrationBuilder.DropIndex(
                name: "IX_CacheTables_TableName",
                table: "CacheTables");
        }
    }
}
