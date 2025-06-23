using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using backend.Models;
using backend.Services;

namespace backend.Controllers
{
    [Authorize]
    [Route("execute_sql")]
    public class SqlController : Controller
    {
        private readonly HanaService _hanaService;

        public SqlController(HanaService hanaService)
        {
            _hanaService = hanaService;
        }

        [HttpPost]
        public IActionResult ExecuteSql([FromBody] SqlRequest request)
        {
            try
            {
                var result = _hanaService.ExecuteQuery(request.SqlQuery);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
