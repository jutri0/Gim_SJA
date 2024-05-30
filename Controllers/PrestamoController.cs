using GymAJT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace GymAJT.Controllers
{
    public class PrestamoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetAlumnos()
        {
            try
            {

                var Alumno = new List<ViewModelAlumno>();
                using (MiDbContext db = new MiDbContext())
                {
                    Alumno = db.Set<ViewModelAlumno>().FromSqlRaw("EXEC getAlumnosSelect").ToList();
                }

                if (Alumno.Any())
                {
                    var dataJson = Alumno;
                    return Json(new { dataJson, message = "Alumnos encontrados" });
                }
                else
                {
                    return Json(new { message = "No se encontraron Alumnos." });
                }
            }
            catch (Exception ex)
            {
                // Maneja la excepción, registra información de depuración o devuelve un mensaje de error controlado.
                return Json(new { error = ex.Message });
            }
        }

        public JsonResult GetEquipos()
        {
            try
            {

                var Equipos = new List<ViewModelEquipo>();
                using (MiDbContext db = new MiDbContext())
                {
                    Equipos = db.Set<ViewModelEquipo>().FromSqlRaw("EXEC getEquiposSelect").ToList();
                }

                if (Equipos.Any())
                {
                    var dataJson = Equipos;
                    return Json(new { dataJson, message = "Equipos encontrados" });
                }
                else
                {
                    return Json(new { message = "No se encontraron Equipos." });
                }
            }
            catch (Exception ex)
            {
                // Maneja la excepción, registra información de depuración o devuelve un mensaje de error controlado.
                return Json(new { error = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult UpdatePrestamo([FromBody] Prestamos prestamo)
        {
            try
            {
                using (MiDbContext db = new MiDbContext())
                {
                    db.Database.ExecuteSqlRaw("EXEC insertPrestamo @alumno, @fecha, @equipo, @cantidad, @observaciones",
                                new SqlParameter("@alumno", prestamo.AlumnoPrestamo),
                                new SqlParameter("@fecha", prestamo.FechaPrestamo),
                                new SqlParameter("@equipo", prestamo.Equipo),
                                new SqlParameter("@cantidad", prestamo.CantidadEquipo),
                                new SqlParameter("@observaciones", prestamo.Observaciones));
                }

                return Json(new { message = "Se guardó con éxito" });
            }
            catch (Exception ex)
            {
                // Maneja la excepción, registra información de depuración o devuelve un mensaje de error controlado.
                return Json(new { error = ex.Message });
            }
        }
    }
    
}
