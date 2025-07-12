using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddCacheTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Schemas");

            migrationBuilder.CreateTable(
                name: "CacheTables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SchemaName = table.Column<string>(type: "TEXT", nullable: false),
                    TableName = table.Column<string>(type: "TEXT", nullable: false),
                    TableOid = table.Column<long>(type: "INTEGER", nullable: false),
                    FixedPartSize = table.Column<int>(type: "INTEGER", nullable: false),
                    IsLogged = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsSystemTable = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsColumnTable = table.Column<bool>(type: "INTEGER", nullable: false),
                    TableType = table.Column<string>(type: "TEXT", nullable: false),
                    IsInsertOnly = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsTenantSharedData = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsTenantSharedMetadata = table.Column<bool>(type: "INTEGER", nullable: false),
                    SessionType = table.Column<string>(type: "TEXT", nullable: false),
                    IsTemporary = table.Column<bool>(type: "INTEGER", nullable: false),
                    TemporaryTableType = table.Column<string>(type: "TEXT", nullable: false),
                    IsUserDefinedType = table.Column<bool>(type: "INTEGER", nullable: false),
                    HasPrimaryKey = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsReplica = table.Column<bool>(type: "INTEGER", nullable: false),
                    HasStructuredPrivilegeCheck = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsSeriesTable = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CacheTables", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CacheTables");

            migrationBuilder.CreateTable(
                name: "Schemas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FixedPartSize = table.Column<int>(type: "INTEGER", nullable: false),
                    HasPrimaryKey = table.Column<bool>(type: "INTEGER", nullable: false),
                    HasStructuredPrivilegeCheck = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsColumnTable = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsInsertOnly = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsLogged = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsReplica = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsSeriesTable = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsSystemTable = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsTemporary = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsTenantSharedData = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsTenantSharedMetadata = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsUserDefinedType = table.Column<bool>(type: "INTEGER", nullable: false),
                    SchemaName = table.Column<string>(type: "TEXT", nullable: false),
                    SessionType = table.Column<string>(type: "TEXT", nullable: false),
                    TableName = table.Column<string>(type: "TEXT", nullable: false),
                    TableOid = table.Column<long>(type: "INTEGER", nullable: false),
                    TableType = table.Column<string>(type: "TEXT", nullable: false),
                    TemporaryTableType = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schemas", x => x.Id);
                });
        }
    }
}
