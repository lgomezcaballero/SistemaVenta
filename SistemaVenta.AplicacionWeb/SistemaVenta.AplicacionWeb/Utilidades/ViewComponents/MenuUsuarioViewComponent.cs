
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace SistemaVenta.AplicacionWeb.Utilidades.ViewComponents
{
    public class MenuUsuarioViewComponent : ViewComponent
    {


#pragma warning disable CS1998 // El método asincrónico carece de operadores "await" y se ejecutará de forma sincrónica
        public async Task<IViewComponentResult> InvokeAsync() { 
        
            ClaimsPrincipal claimUser = HttpContext.User;

            string nombreUsuario = "";
            string urlFotoUsuario = "";

#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            if (claimUser.Identity.IsAuthenticated) { 
#pragma warning disable CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL
                nombreUsuario = claimUser.Claims
                    .Where(c => c.Type == ClaimTypes.Name)
                    .Select(c => c.Value).SingleOrDefault();
#pragma warning restore CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL

#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
                urlFotoUsuario = ((ClaimsIdentity)claimUser.Identity).FindFirst("UrlFoto").Value;
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            }
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            ViewData["nombreUsuario"] = nombreUsuario;
            ViewData["urlFotoUsuario"] = urlFotoUsuario;

            return View();


        }
#pragma warning restore CS1998 // El método asincrónico carece de operadores "await" y se ejecutará de forma sincrónica
    }
}
