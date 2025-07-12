namespace backend.Models
{
    public class ExternalColumnTable
    {
        // id INTEGER PRIMARY KEY AUTOINCREMENT,
        // table_name varchar(50) NOT NULL,
        // `order` INTEGER NULL,
        // column_name varchar(100) NOT NULL,
        // description TEXT NULL,
        // sql_type varchar(50) NULL,
        // length INTEGER NULL,
        // decimals INTEGER NULL,
        // relation varchar(50) NULL,
        // default_value TEXT NULL,
        // constraints varchar(255) NULL

        public int Id { get; set; }
        public string TableName { get; set; } = string.Empty;
        public int Order { get; set; }
        public string ColumnName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string SqlType
        { get; set; } = string.Empty;
        public int Length { get; set; }
        public int Decimals { get; set; }
        public string Relation { get; set; } = string.Empty;
        public string DefaultValue { get; set; } = string.Empty;
        public string Constraints { get; set; } = string.Empty;
    }
}