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
    public class ManagerController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IManagerService _managerServicio;

        public ManagerController(IMapper mapper,
            IManagerService managerServicio)
        {
            _mapper = mapper;
            _managerServicio = managerServicio;

        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<VMManager> vmManagerLista = _mapper.Map<List<VMManager>>(await _managerServicio.Lista());

            return StatusCode(StatusCodes.Status200OK, new { data = vmManagerLista });
        }
        [HttpPost]
        public async Task<IActionResult> Crear([FromForm] IFormFile imagen, [FromForm] string modelo)
        {

            GenericResponse<VMManager> gResponse = new GenericResponse<VMManager>();

            try
            {
#pragma warning disable CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL
                VMManager vmManager = JsonConvert.DeserializeObject<VMManager>(modelo);
#pragma warning restore CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL

                string nombreImagen = "";
#pragma warning disable CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL
                Stream imagenStream = null;
#pragma warning restore CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL

                if (imagen != null)
                {
                    string nombre_en_codigo = Guid.NewGuid().ToString("N");
                    string extension = Path.GetExtension(imagen.FileName);
                    nombreImagen = string.Concat(nombre_en_codigo, extension);
                    imagenStream = imagen.OpenReadStream();
                }

#pragma warning disable CS8604 // Posible argumento de referencia nulo
                Manager manager_creado = await _managerServicio.Crear(_mapper.Map<Manager>(vmManager), imagenStream, nombreImagen);
#pragma warning restore CS8604 // Posible argumento de referencia nulo

                vmManager = _mapper.Map<VMManager>(manager_creado);

                gResponse.Estado = true;
                gResponse.Objeto = vmManager;

            }
            catch (Exception ex)
            {
                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, gResponse);
        }
        [HttpPut]
        public async Task<IActionResult> Editar([FromForm] IFormFile imagen, [FromForm] string modelo)
        {

            GenericResponse<VMManager> gResponse = new GenericResponse<VMManager>();

            try
            {
#pragma warning disable CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL
                VMManager vmManager = JsonConvert.DeserializeObject<VMManager>(modelo);
#pragma warning restore CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL

                string nombreImagen = "";
#pragma warning disable CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL
                Stream imagenStream = null;
#pragma warning restore CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL

                if (imagen != null)
                {
                    string nombre_en_codigo = Guid.NewGuid().ToString("N");
                    string extension = Path.GetExtension(imagen.FileName);
                    nombreImagen = string.Concat(nombre_en_codigo, extension);
                    imagenStream = imagen.OpenReadStream();
                }

#pragma warning disable CS8604 // Posible argumento de referencia nulo
                Manager manager_editado = await _managerServicio.Editar(_mapper.Map<Manager>(vmManager), imagenStream, nombreImagen);
#pragma warning restore CS8604 // Posible argumento de referencia nulo

                vmManager = _mapper.Map<VMManager>(manager_editado);

                gResponse.Estado = true;
                gResponse.Objeto = vmManager;

            }
            catch (Exception ex)
            {
                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, gResponse);
        }

        [HttpDelete]
        public async Task<IActionResult> Eliminar(int idManager)
        {
            GenericResponse<string> gResponse = new GenericResponse<string>();

            try
            {

                gResponse.Estado = await _managerServicio.Eliminar(idManager);
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
