using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class msg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FixedPartSize",
                table: "CacheTables");

            migrationBuilder.DropColumn(
                name: "HasPrimaryKey",
                table: "CacheTables");

            migrationBuilder.DropColumn(
                name: "HasStructuredPrivilegeCheck",
                table: "CacheTables");

            migrationBuilder.DropColumn(
                name: "IsColumnTable",
                table: "CacheTables");

            migrationBuilder.DropColumn(
                name: "IsInsertOnly",
                table: "CacheTables");

            migrationBuilder.DropColumn(
                name: "IsLogged",
                table: "CacheTables");

            migrationBuilder.DropColumn(
                name: "IsReplica",
                table: "CacheTables");

            migrationBuilder.DropColumn(
                name: "IsSeriesTable",
                table: "CacheTables");

            migrationBuilder.DropColumn(
                name: "IsSystemTable",
                table: "CacheTables");

            migrationBuilder.DropColumn(
                name: "IsTemporary",
                table: "CacheTables");

            migrationBuilder.DropColumn(
                name: "IsTenantSharedData",
                table: "CacheTables");

            migrationBuilder.DropColumn(
                name: "IsTenantSharedMetadata",
                table: "CacheTables");

            migrationBuilder.DropColumn(
                name: "IsUserDefinedType",
                table: "CacheTables");

            migrationBuilder.DropColumn(
                name: "SessionType",
                table: "CacheTables");

            migrationBuilder.DropColumn(
                name: "TableOid",
                table: "CacheTables");

            migrationBuilder.DropColumn(
                name: "TableType",
                table: "CacheTables");

            migrationBuilder.DropColumn(
                name: "TemporaryTableType",
                table: "CacheTables");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FixedPartSize",
                table: "CacheTables",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "HasPrimaryKey",
                table: "CacheTables",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasStructuredPrivilegeCheck",
                table: "CacheTables",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsColumnTable",
                table: "CacheTables",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsInsertOnly",
                table: "CacheTables",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsLogged",
                table: "CacheTables",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsReplica",
                table: "CacheTables",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSeriesTable",
                table: "CacheTables",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSystemTable",
                table: "CacheTables",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTemporary",
                table: "CacheTables",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTenantSharedData",
                table: "CacheTables",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTenantSharedMetadata",
                table: "CacheTables",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUserDefinedType",
                table: "CacheTables",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SessionType",
                table: "CacheTables",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "TableOid",
                table: "CacheTables",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "TableType",
                table: "CacheTables",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TemporaryTableType",
                table: "CacheTables",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
