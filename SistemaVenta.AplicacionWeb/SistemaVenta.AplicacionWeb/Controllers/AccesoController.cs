using Microsoft.AspNetCore.Mvc;

using SistemaVenta.AplicacionWeb.Models.ViewModels;
using SistemaVenta.BLL.Interfaces;
using SistemaVenta.Entity;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;



namespace SistemaVenta.AplicacionWeb.Controllers
{
    public class AccesoController : Controller
    {
        private readonly IUsuarioService _usuarioServicio;
        public AccesoController(IUsuarioService usuarioServicio)
        {
            _usuarioServicio = usuarioServicio;
        }

        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;


#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            if (claimUser.Identity.IsAuthenticated) {
                return RedirectToAction("Index", "Home");
            }
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            return View();
        }

        public IActionResult RestablecerClave()
        {
            
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(VMUsuarioLogin modelo)
        {


#pragma warning disable CS8604 // Posible argumento de referencia nulo
#pragma warning disable CS8604 // Posible argumento de referencia nulo
            Usuario usuario_encontrado = await _usuarioServicio.ObtenerPorCredenciales(modelo.Correo, modelo.Clave);
#pragma warning restore CS8604 // Posible argumento de referencia nulo
#pragma warning restore CS8604 // Posible argumento de referencia nulo


            if (usuario_encontrado == null) {
                ViewData["Mensaje"] = "No se encontraron coincidencias";
                return View();
            }
            ViewData["Mensaje"] = null;


#pragma warning disable CS8604 // Posible argumento de referencia nulo
#pragma warning disable CS8604 // Posible argumento de referencia nulo
#pragma warning disable CS8604 // Posible argumento de referencia nulo
            List<Claim> claims = new List<Claim>() { 
                new Claim(ClaimTypes.Name, usuario_encontrado.Nombre),
                new Claim(ClaimTypes.NameIdentifier, usuario_encontrado.IdUsuario.ToString()),
                new Claim(ClaimTypes.Role, usuario_encontrado.IdRol.ToString()),
                new Claim("UrlFoto", usuario_encontrado.UrlFoto),
            };
#pragma warning restore CS8604 // Posible argumento de referencia nulo
#pragma warning restore CS8604 // Posible argumento de referencia nulo
#pragma warning restore CS8604 // Posible argumento de referencia nulo


            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            AuthenticationProperties properties = new AuthenticationProperties() { 
                AllowRefresh = true,
                IsPersistent = modelo.MantenerSesion
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties
                );

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> RestablecerClave(VMUsuarioLogin modelo)
        {

            try
            {
                string urlPlantillaCorreo = $"{this.Request.Scheme}://{this.Request.Host}/Plantilla/RestablecerClave?clave=[clave]";


#pragma warning disable CS8604 // Posible argumento de referencia nulo
                bool resultado = await _usuarioServicio.RestablecerClave(modelo.Correo, urlPlantillaCorreo);
#pragma warning restore CS8604 // Posible argumento de referencia nulo


                if (resultado) {
                    ViewData["Mensaje"] = "Listo, su contraseña fue restablecida. Revise su correo";
                    ViewData["MensajeError"] = null;
                }
                else {
                    ViewData["MensajeError"] = "Tenemos problemas. Por favor inténtelo de nuevo más tarde";
                    ViewData["Mensaje"] = null;
                }

            }
            catch(Exception ex) {
                ViewData["MensajeError"] =ex.Message;
                ViewData["Mensaje"] = null;
            }

            return View();
        }

    }
}
