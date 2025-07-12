namespace backend.Models
{
    public class ExternalTableListEntity
    {
        // id INTEGER PRIMARY KEY AUTOINCREMENT,
        //     table_name varchar(50) NOT NULL,
        //     product varchar(50) NULL,
        //     version varchar(10) NULL,
        //     total_columns INTEGER NULL,
        //     total_indexs INTEGER NULL,
        //     description TEXT NULL,
        //     module varchar(255) NULL
        public int Id { get; set; }
        public string TableName { get; set; } = string.Empty;
        public string Product { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
        public int TotalColumns { get; set; }
        public int TotalIndexes { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Module { get; set; } = string.Empty;
    }
}