namespace backend.Models
{
    public class SqlRequest
    {
        public string Query { get; set; } = string.Empty;
        public int LimitRows { get; set; } = 100; // Default limit to 100 rows
    }
}
