using GymAJT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace GymAJT.Controllers
{
    public class AlumnoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            return View();
        }

        public JsonResult GetAlumnos()
        {
            try
            {
                var alumnos = new List<Alumnos>();
                using (MiDbContext db = new MiDbContext())
                {
                    alumnos = db.Set<Alumnos>().FromSqlRaw("EXEC getAlumnosSelect").ToList();
                }

                if (alumnos.Any())
                {
                    var dataJson = alumnos;
                    return Json(new { dataJson, message = "Alumnos encontrados" });
                }
                else
                {
                    return Json(new { message = "No se encontraron Alumnos." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateAlumno([FromBody] Alumnos alumno)
        {
            try
            {
                using (MiDbContext db = new MiDbContext())
                {
                    db.Database.ExecuteSqlRaw("EXEC insertAlumno @codigo, @nombres, @curso, @correo, @correoPaternal",
                                new SqlParameter("@codigo", alumno.Codigo),
                                new SqlParameter("@nombres", alumno.Nombres),
                                new SqlParameter("@curso", alumno.Curso),
                                new SqlParameter("@correo", alumno.Correo),
                                new SqlParameter("@correoPaternal", alumno.CorreoPaternal));
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
