using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GymAJT.Filters
{
    public class Seguridad : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var usu = context.HttpContext.Session.GetString("idUsuario");
            if (usu == null)
            {
                context.Result = new RedirectResult("/Login");
            }
        }
    }
}
