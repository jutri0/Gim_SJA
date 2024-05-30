using GymAJT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace GymAJT.Controllers
{
    public class EquipoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult List()
        {
            return View();
        }
        public JsonResult GetEquipos()
        {
            try
            {
                var equipos = new List<Equipos>();
                using (MiDbContext db = new MiDbContext())
                {
                    equipos = db.Set<Equipos>().FromSqlRaw("EXEC getEquiposSelect").ToList();
                }

                if (equipos.Any())
                {
                    var dataJson = equipos;
                    return Json(new { dataJson, message = "Equipos encontrados" });
                }
                else
                {
                    return Json(new { message = "No se encontraron Equipos." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateEquipo([FromForm] Equipos equipo, IFormFile? foto)
        {
            try
            {
                byte[]? fotoBytes = null;
                if (foto != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        foto.CopyTo(ms);
                        fotoBytes = ms.ToArray();
                    }
                }

                using (MiDbContext db = new MiDbContext())
                {
                    db.Database.ExecuteSqlRaw("EXEC insertEquipo @nombre, @cantidad, @cantidadMax, @observaciones, @fotoEquipo",
                                new SqlParameter("@nombre", equipo.Nombre ?? (object)DBNull.Value),
                                new SqlParameter("@cantidad", equipo.Cantidad ?? (object)DBNull.Value),
                                new SqlParameter("@cantidadMax", equipo.CantidadMax ?? (object)DBNull.Value),
                                new SqlParameter("@observaciones", equipo.Observaciones ?? (object)DBNull.Value),
                                new SqlParameter("@fotoEquipo", equipo.FotoEquipo ?? (object)DBNull.Value));
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
