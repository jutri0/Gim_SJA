using GymAJT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace GymAJT.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public string login(string user, string pass)
        {
            List<Usuarios> resultados = new List<Usuarios>();

            string rpta = "";

            var userParam = new SqlParameter("@user", user);
            var passParam = new SqlParameter("@pass", pass);

            using (MiDbContext db = new MiDbContext())
            {
                // Ejecutar el procedimiento almacenado utilizando FromSqlRaw
                resultados = db.Set<Usuarios>().FromSqlRaw("EXEC LoginGym @user, @pass", userParam, passParam).ToList();

                if (resultados.Count > 0)
                {
                    var pk = Convert.ToInt32(resultados[0].IdUsuarioPk);
                    HttpContext.Session.SetInt32("idUsuario", pk);
                    rpta = "Ok";
                }

            }

            return rpta;
        }
    }
}
