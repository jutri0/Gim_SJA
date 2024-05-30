using GymAJT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace GymAJT.Controllers
{
    public class DevolucionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            return View();
        }

        public JsonResult GetDevoluciones()
        {
            try
            {
                var devoluciones = new List<Devoluciones>();
                using (MiDbContext db = new MiDbContext())
                {
                    devoluciones = db.Set<Devoluciones>().FromSqlRaw("EXEC getDevolucionesSelect").ToList();
                }

                if (devoluciones.Any())
                {
                    var dataJson = devoluciones;
                    return Json(new { dataJson, message = "Devoluciones encontradas" });
                }
                else
                {
                    return Json(new { message = "No se encontraron Devoluciones." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateDevolucion([FromBody] Devoluciones devolucion)
        {
            try
            {
                using (MiDbContext db = new MiDbContext())
                {
                    db.Database.ExecuteSqlRaw("EXEC insertDevolucion @alumnoDevolucion, @prestamo, @fechaDevolucion, @equipo, @observaciones",
                                new SqlParameter("@alumnoDevolucion", devolucion.AlumnoDevolucion),
                                new SqlParameter("@prestamo", devolucion.Prestamo),
                                new SqlParameter("@fechaDevolucion", devolucion.FechaDevolucion),
                                new SqlParameter("@equipo", devolucion.Equipo),
                                new SqlParameter("@observaciones", devolucion.Observaciones));
                }

                return Json(new { message = "Se guardó con éxito" });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
    }
}
