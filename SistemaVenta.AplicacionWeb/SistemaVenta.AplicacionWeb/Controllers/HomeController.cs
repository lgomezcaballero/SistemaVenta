using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaVenta.AplicacionWeb.Models;
using System.Diagnostics;

using System.Security.Claims;


using AutoMapper;
using SistemaVenta.AplicacionWeb.Models.ViewModels;
using SistemaVenta.AplicacionWeb.Utilidades.Response;
using SistemaVenta.BLL.Interfaces;
using SistemaVenta.Entity;

namespace SistemaVenta.AplicacionWeb.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {
     
        private readonly IUsuarioService _usuarioServicio;
        private readonly IMapper _mapper;

        public HomeController(IUsuarioService usuarioServicio, IMapper mapper)
        {
            _usuarioServicio = usuarioServicio;
            _mapper = mapper;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Perfil()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ObtenerUsuario()
        {
            GenericResponse<VMUsuario> response = new GenericResponse<VMUsuario>();
            try
            {
                ClaimsPrincipal claimUser = HttpContext.User;

#pragma warning disable CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL
                string idUsuario = claimUser.Claims
                    .Where(c => c.Type == ClaimTypes.NameIdentifier)
                    .Select(c => c.Value).SingleOrDefault();
#pragma warning restore CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL

#pragma warning disable CS8604 // Posible argumento de referencia nulo
                VMUsuario usuario = _mapper.Map<VMUsuario>(await _usuarioServicio.ObtenerPorId(int.Parse(idUsuario)));
#pragma warning restore CS8604 // Posible argumento de referencia nulo

                response.Estado = true;
                response.Objeto = usuario;
            }
            catch (Exception ex) {
                response.Estado = false;
                response.Mensaje = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarPerfil([FromBody] VMUsuario modelo)
        {
            GenericResponse<VMUsuario> response = new GenericResponse<VMUsuario>();
            try
            {
                ClaimsPrincipal claimUser = HttpContext.User;

#pragma warning disable CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL
                string idUsuario = claimUser.Claims
                    .Where(c => c.Type == ClaimTypes.NameIdentifier)
                    .Select(c => c.Value).SingleOrDefault();
#pragma warning restore CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL

                Usuario entidad = _mapper.Map<Usuario>(modelo);

#pragma warning disable CS8604 // Posible argumento de referencia nulo
                entidad.IdUsuario = int.Parse(idUsuario);
#pragma warning restore CS8604 // Posible argumento de referencia nulo

                bool resultado = await _usuarioServicio.GuardarPefil(entidad);

                response.Estado = resultado;
            }
            catch (Exception ex)
            {
                response.Estado = false;
                response.Mensaje = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpPost]
        public async Task<IActionResult> CambiarClave([FromBody] VMCambiarClave modelo)
        {
            GenericResponse<bool> response = new GenericResponse<bool>();
            try
            {
                ClaimsPrincipal claimUser = HttpContext.User;

#pragma warning disable CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL
                string idUsuario = claimUser.Claims
                    .Where(c => c.Type == ClaimTypes.NameIdentifier)
                    .Select(c => c.Value).SingleOrDefault();
#pragma warning restore CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL

#pragma warning disable CS8604 // Posible argumento de referencia nulo
#pragma warning disable CS8604 // Posible argumento de referencia nulo
#pragma warning disable CS8604 // Posible argumento de referencia nulo
                bool resultado = await _usuarioServicio.CambiarClave(
                    int.Parse(idUsuario),
                    modelo.claveActual,
                    modelo.claveNueva
                    );
#pragma warning restore CS8604 // Posible argumento de referencia nulo
#pragma warning restore CS8604 // Posible argumento de referencia nulo
#pragma warning restore CS8604 // Posible argumento de referencia nulo

                response.Estado = resultado;
            }
            catch (Exception ex)
            {
                response.Estado = false;
                response.Mensaje = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, response);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Acceso");
        }
    }
}