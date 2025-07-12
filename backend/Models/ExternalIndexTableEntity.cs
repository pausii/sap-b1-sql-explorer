namespace backend.Models
{
    public class ExternalIndexTableEntity
    {
        // id INTEGER PRIMARY KEY AUTOINCREMENT,
        // table_name varchar(50) NOT NULL,
        // name varchar(100) NOT NULL,
        // `primary` varchar(50) NULL,
        // `unique` varchar(50) NULL,
        // type varchar(50) NULL,
        // columns varchar(255) NULL
        public int Id { get; set; }
        public string TableName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Primary { get; set; } = string.Empty;
        public string Unique { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Columns { get; set; } = string.Empty;
    }
}