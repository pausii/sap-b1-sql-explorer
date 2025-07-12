using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class addDesc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ExternalIndexTableEntity",
                table: "ExternalIndexTableEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExternalColumnTable",
                table: "ExternalColumnTable");

            migrationBuilder.RenameTable(
                name: "ExternalIndexTableEntity",
                newName: "ExtIndexTable");

            migrationBuilder.RenameTable(
                name: "ExternalColumnTable",
                newName: "ExtColumnTable");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CacheTables",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExtIndexTable",
                table: "ExtIndexTable",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExtColumnTable",
                table: "ExtColumnTable",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ExtIndexTable",
                table: "ExtIndexTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExtColumnTable",
                table: "ExtColumnTable");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "CacheTables");

            migrationBuilder.RenameTable(
                name: "ExtIndexTable",
                newName: "ExternalIndexTableEntity");

            migrationBuilder.RenameTable(
                name: "ExtColumnTable",
                newName: "ExternalColumnTable");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExternalIndexTableEntity",
                table: "ExternalIndexTableEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExternalColumnTable",
                table: "ExternalColumnTable",
                column: "Id");
        }
    }
}
