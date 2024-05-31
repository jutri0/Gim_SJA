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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetAlumnos()
        {
            var prestamos = new List<Alumnos>();

            using (MiDbContext db = new MiDbContext())
            {
                prestamos = db.Set<Alumnos>().FromSqlRaw("EXEC getAlumnos").ToList();
            }

            return Json(prestamos);
        }

        public IActionResult List()
        {
            return View();
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
