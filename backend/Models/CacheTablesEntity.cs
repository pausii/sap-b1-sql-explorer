namespace backend.Models
{
    //  "SCHEMA_NAME": "SBODEMO",
    //     "TABLE_NAME": "ABTW",
    //     "TABLE_OID": 43923158,
    //     "FIXED_PART_SIZE": 72,
    //     "IS_LOGGED": "TRUE",
    //     "IS_SYSTEM_TABLE": "FALSE",
    //     "IS_COLUMN_TABLE": "FALSE",
    //     "TABLE_TYPE": "ROW",
    //     "IS_INSERT_ONLY": "FALSE",
    //     "IS_TENANT_SHARED_DATA": "TRUE",
    //     "IS_TENANT_SHARED_METADATA": "TRUE",
    //     "SESSION_TYPE": "NONE",
    //     "IS_TEMPORARY": "FALSE",
    //     "TEMPORARY_TABLE_TYPE": "NONE",
    //     "IS_USER_DEFINED_TYPE": "FALSE",
    //     "HAS_PRIMARY_KEY": "TRUE",
    //     "IS_REPLICA": "FALSE",
    //     "HAS_STRUCTURED_PRIVILEGE_CHECK": "FALSE",
    //     "IS_SERIES_TABLE": "FALSE",
    public class CacheTablesEntity
    {
        public int Id { get; set; }
        public string SchemaName { get; set; } = string.Empty;
        public string TableName { get; set; } = string.Empty;
        public string? Description { get; set; } // Nullable to allow empty descriptions
        // public long TableOid { get; set; }
        // public int FixedPartSize { get; set; }
        // public bool IsLogged { get; set; }
        // public bool IsSystemTable { get; set; }
        // public bool IsColumnTable { get; set; }
        // public string TableType { get; set; } = string.Empty;
        // public bool IsInsertOnly { get; set; }
        // public bool IsTenantSharedData { get; set; }
        // public bool IsTenantSharedMetadata { get; set; }
        // public string SessionType { get; set; } = string.Empty;
        // public bool IsTemporary { get; set; }
        // public string TemporaryTableType { get; set; } = string.Empty;
        // public bool IsUserDefinedType { get; set; }
        // public bool HasPrimaryKey { get; set; }
        // public bool IsReplica { get; set; }
        // public bool HasStructuredPrivilegeCheck { get; set; }
        // public bool IsSeriesTable { get; set; }
    }
}