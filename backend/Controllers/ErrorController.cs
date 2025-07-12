using Microsoft.AspNetCore.Mvc;

public class ErrorController : Controller
{
    [Route("Error/404")]
    public IActionResult Error404()
    {
        return View("NotFound"); // Menampilkan Views/Shared/NotFound.cshtml
    }

    [Route("Error/{code}")]
    public IActionResult ErrorGeneric(int code)
    {
        if (code == 404)
            return RedirectToAction(nameof(Error404));

        // Bisa ditambahkan halaman error lain (500, dll)
        return View("Error"); // fallback
    }
}
