using Microsoft.AspNetCore.Mvc;


using AutoMapper;
using Newtonsoft.Json;
using SistemaVenta.AplicacionWeb.Models.ViewModels;
using SistemaVenta.AplicacionWeb.Utilidades.Response;
using SistemaVenta.BLL.Interfaces;
using SistemaVenta.Entity;
using Microsoft.AspNetCore.Authorization;

namespace SistemaVenta.AplicacionWeb.Controllers
{
    [Authorize]
    public class NegocioController : Controller
    {

        private readonly IMapper _mapper;
        private readonly INegocioService _negocioService;

        public NegocioController(IMapper mapper, INegocioService negocioService)
        {
            _mapper = mapper;
            _negocioService = negocioService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            GenericResponse<VMNegocio> gResponse = new GenericResponse<VMNegocio>();

            try
            {
                VMNegocio vmNegocio = _mapper.Map<VMNegocio>(await _negocioService.Obtener());

                gResponse.Estado = true;
                gResponse.Objeto = vmNegocio;
            }
            catch(Exception ex) {
                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message;
            }


            return StatusCode(StatusCodes.Status200OK, gResponse);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarCambios([FromForm]IFormFile logo, [FromForm]string modelo)
        {
            GenericResponse<VMNegocio> gResponse = new GenericResponse<VMNegocio>();

            try
            {
#pragma warning disable CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL
                VMNegocio vmNegocio = JsonConvert.DeserializeObject<VMNegocio>(modelo);
#pragma warning restore CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL

                string nombreLogo = "";
#pragma warning disable CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL
                Stream logoStream = null;
#pragma warning restore CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL

                if (logo != null) { 
                
                    string nombre_en_codigo = Guid.NewGuid().ToString("N");
                    string extension = Path.GetExtension(logo.FileName);
                    nombreLogo = string.Concat(nombre_en_codigo, extension);
                    logoStream = logo.OpenReadStream();
                }

#pragma warning disable CS8604 // Posible argumento de referencia nulo
                Negocio negocio_editado = await _negocioService.GuardarCambios(_mapper.Map<Negocio>(vmNegocio)
                    , logoStream, nombreLogo);
#pragma warning restore CS8604 // Posible argumento de referencia nulo


                vmNegocio = _mapper.Map<VMNegocio>(negocio_editado);

                gResponse.Estado = true;
                gResponse.Objeto = vmNegocio;
            }
            catch (Exception ex)
            {
                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message;
            }


            return StatusCode(StatusCodes.Status200OK, gResponse);
        }



    }
}
