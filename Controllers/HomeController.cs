using GymAJT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace GymAJT.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Usuarios> resultados = new List<Usuarios>();

            var userParam = new SqlParameter("@user", "Admin");
            var passParam = new SqlParameter("@pass", "12345");

            using (MiDbContext db = new MiDbContext())
            {
                resultados = db.Set<Usuarios>().FromSqlRaw("EXEC LoginGym @user, @pass", userParam, passParam).ToList();
                if (resultados.Count > 0)
                {
                    var pk = Convert.ToInt32(resultados[0].IdUsuarioPk);
                    HttpContext.Session.SetInt32("idUsuario", pk);
                }
            }
                return View();
        }

        [HttpPost]
        public JsonResult GetPrestamos()
        {
            // Ejemplo de datos, en una aplicación real estos vendrían de una base de datos
            var prestamos = new List<Prestamos>();

            using (MiDbContext db = new MiDbContext())
            {
                prestamos = db.Set<Prestamos>().FromSqlRaw("EXEC getPrestamos").ToList();
            }

            return Json(prestamos);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}