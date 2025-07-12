using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using backend.Models;
using backend.Services;
using System.Text.Json;
using Microsoft.Extensions.Options;
using backend.Data; // pastikan pakai namespace yang benar
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;


namespace backend.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class SqlController : Controller
    {
        private readonly HanaService _hanaService;
        private readonly GlobalSettings _settings;

        public SqlController(HanaService hanaService, IOptions<GlobalSettings> settings)
        {
            _hanaService = hanaService;
            _settings = settings.Value;
        }

        [HttpPost("execute")]
        public IActionResult ExecuteSql([FromBody] SqlRequest request)
        {
            try
            {
                if (Regex.IsMatch(request.Query, @"\b(DELETE|UPDATE|INSERT|TRUNCATE)\b", RegexOptions.IgnoreCase))
                {
                    return BadRequest(new { error = "Query contains forbidden operations (DELETE, UPDATE, INSERT, TRUNCATE)." });
                }

                string query = request.Query.Trim();
                int limitRows = request.LimitRows;
                if (!Regex.IsMatch(query, @"\s+LIMIT\s+\d+", RegexOptions.IgnoreCase))
                {
                    query += $" LIMIT {limitRows}";
                }
                // return Ok(query);

                // Console.WriteLine(JsonSerializer.Serialize(request, new JsonSerializerOptions { WriteIndented = true }));
                Console.WriteLine(JsonSerializer.Serialize<SqlRequest>(request, new JsonSerializerOptions { WriteIndented = true }));
                var result = _hanaService.ExecuteQuery(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // return BadRequest(new { error = ex.Message });
                return BadRequest(new { error = ex.ToString() });
            }
        }


        [HttpGet("tables")]
        public async Task<IActionResult> GetTables(AppDbContext context)
        {
            // check to cache db sqlite3 first
            var searchQuery = HttpContext.Request.Query["search"].ToString();
            var limit = HttpContext.Request.Query["limit"].ToString();
            var orderTop = HttpContext.Request.Query["orderTop"].ToString();
            var customOrder = new List<string> { "OINV", "INV1", "CRD1" };
            var includeAiDescription = HttpContext.Request.Query["includeAiDescription"].ToString();

            // Query utama
            var query = from t in context.CacheTables
                        join m in context.ExternalTableListEntity on t.TableName equals m.TableName into joined
                        from m in joined.DefaultIfEmpty()
                        where t.SchemaName == _settings.MainSchema
                        select new
                        {
                            t.Id,
                            t.SchemaName,
                            t.TableName,
                            Module = m != null ? m.Module : null,
                            Description = m != null ? m.Description : null,
                            TotalColumns = m != null ? m.TotalColumns : 0,

                            // ✅ Tambahkan ini hanya jika includeAiDescription == true
                            AiDesc = includeAiDescription=="true" ? t.Description : null
                        };

            // Tambahkan filter search kalau ada
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                query = query.Where(x => x.TableName.StartsWith(searchQuery) ||
                x.Description.Contains(searchQuery)); // tidak pakai index
                // x.Description.StartsWith(searchQuery)); // pakai index
            }

            var shortNameOnly = HttpContext.Request.Query["shortNameOnly"].ToString();
            if (shortNameOnly == "true")
            {
                if (string.IsNullOrWhiteSpace(searchQuery))
                {
                    query = query.Where(x => x.TableName.Length < 6);
                }
                else
                {
                    query = query.Where(x => x.TableName.Length < 15);
                }

            }

            // Tambahkan limit kalau ada
            if (!string.IsNullOrWhiteSpace(limit))
            {
                query = query.Take(int.Parse(limit));
            }

            // Tambahkan custom order
            if (!string.IsNullOrWhiteSpace(orderTop))
            {
                query = query
                    .OrderBy(x =>
                        x.TableName == "OINV" ? 0 :
                        x.TableName == "INV1" ? 1 :
                        x.TableName == "CRD1" ? 2 :
                        x.TableName == "OOCR" ? 3 :
                        x.TableName == "CUFD" ? 4 :
                        99
                    )
                    .ThenBy(x => x.TableName);
            }

            var res = await query.ToListAsync();

            // context.CacheTables
            //     .Where(t => t.SchemaName == _settings.MainSchema)
            //     .ToList();
            // var res = await (
            //     from t in context.CacheTables
            //     join m in context.ExternalTableListEntity on t.TableName equals m.TableName into joined
            //     from m in joined.DefaultIfEmpty()
            //     where t.SchemaName == _settings.MainSchema
            //     select new
            //     {
            //         t.Id,
            //         t.SchemaName,
            //         t.TableName,
            //         Module = m != null ? m.Module : null,
            //         Description = m != null ? m.Description : null,
            //         TotalColumns = m != null ? m.TotalColumns : 0,
            //     }
            // ).ToListAsync();

            var cacheOnly = HttpContext.Request.Query["cacheOnly"].ToString();
            if (res.Count > 0 || cacheOnly == "true")
                {
                    // jika ada, return data dari cache
                    var grouped = res
                    .GroupBy(x => new
                    {
                        Module = string.IsNullOrEmpty(x.Module) ? "Undefined" : x.Module,
                        Schema = x.SchemaName
                    })
                    .Select(g => new GroupedResponse
                    {
                        Module = g.Key.Module,
                        Schema = g.Key.Schema,
                        // Description = g.FirstOrDefault()?.Description ?? "-",
                        Tables = g.Select(t => new TableDetail
                        {
                            Id = t.Id,
                            TableName = t.TableName,
                            TotalColumns = t.TotalColumns,
                            Description = t.Description ?? "-",
                            AiDesc = includeAiDescription == "true" ? t.AiDesc : null
                        }).ToList()
                    })
                    .ToList();

                    return Ok(grouped);
                }
                else
                {
                    var tables = _hanaService.GetTables(_settings.MainSchema);

                    foreach (var t in tables)
                    {
                        var schemaName = t["SCHEMA_NAME"]?.ToString() ?? string.Empty;
                        var tableName = t["TABLE_NAME"]?.ToString() ?? string.Empty;

                        var exists = context.CacheTables.Any(x =>
                            x.SchemaName == schemaName && x.TableName == tableName);

                        if (!exists)
                        {
                            context.CacheTables.Add(new CacheTablesEntity
                            {
                                SchemaName = schemaName,
                                TableName = tableName,
                            });
                        }
                    }
                    context.SaveChanges(); // simpan semua setelah loop


                    // return Ok(tables);
                    // ubah menggunkan template dari CacheTablesEntity
                    var cacheTables = tables.Select(t => new CacheTablesEntity
                    {
                        SchemaName = t["SCHEMA_NAME"]?.ToString() ?? string.Empty,
                        TableName = t["TABLE_NAME"]?.ToString() ?? string.Empty
                    }).ToList();

                    Console.WriteLine("Mengambil data dari HanaDB dan menyimpannya ke cache SQLite");
                    return Ok(new
                    {
                        source = "HanaDB",
                        data = cacheTables
                    });
                }

        }

        [HttpGet("columns/{tableName}")] // langung dari cache
        public IActionResult GetColumnNames(string tableName)
        {
            var columns = _hanaService.GetColumnNames(_settings.MainSchema, tableName);
            if (columns == null || columns.Count == 0)
            {
                return NotFound(new { error = "Table not found or has no columns." });
            }

            return Ok(columns);
        }
    }
}
