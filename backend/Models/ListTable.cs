namespace backend.Models
{
    public class ListTable
    {
        //     {
        //         "id": 0,
        //         "schemaName": "SBODEMO",
        //         "tableName": "ABTW"
        //     },
        public int Id { get; set; }
        public string SchemaName { get; set; } = string.Empty;
        public string TableName { get; set; } = string.Empty;
        public string Module { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class GroupedResponse
    {
        public string Module { get; set; }
        // public string Description { get; set; }
        public string Schema { get; set; }
        public List<TableDetail> Tables { get; set; }
    }

    public class TableDetail
    {
        public int Id { get; set; }
        public string TableName { get; set; }
        public int TotalColumns { get; set; }
        public string Description { get; set; } // ✅ tambahkan ini
        public string? AiDesc { get; set; } // Nullable untuk deskripsi AI
    }

}