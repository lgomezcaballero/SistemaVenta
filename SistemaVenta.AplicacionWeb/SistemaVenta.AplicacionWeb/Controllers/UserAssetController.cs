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
    public class UserAssetController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserAssetService _userAssetServicio;

        public UserAssetController(IMapper mapper,
            IUserAssetService userAssetServicio)
        {
            _mapper = mapper;
            _userAssetServicio = userAssetServicio;

        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<VMUserAsset> vmUserAssetLista = _mapper.Map<List<VMUserAsset>>(await _userAssetServicio.Lista());

            return StatusCode(StatusCodes.Status200OK, new { data = vmUserAssetLista });
        }
        [HttpPost]
        public async Task<IActionResult> Crear([FromForm] IFormFile imagen1, IFormFile imagen2, [FromForm] string modelo)
        {

            GenericResponse<VMUserAsset> gResponse = new GenericResponse<VMUserAsset>();

            try
            {
                VMUserAsset vmUserAsset = JsonConvert.DeserializeObject<VMUserAsset>(modelo);

                string nombreImagen1 = "";
                string nombreImagen2 = "";
                Stream imagenStream1 = null;
                Stream imagenStream2 = null;

                if (imagen1 != null)
                {
                    string nombre_en_codigo = Guid.NewGuid().ToString("N");
                    string extension = Path.GetExtension(imagen1.FileName);
                    nombreImagen1 = string.Concat(nombre_en_codigo, extension);
                    imagenStream1 = imagen1.OpenReadStream();
                }

                if (imagen2 != null)
                {
                    string nombre_en_codigo = Guid.NewGuid().ToString("N");
                    string extension = Path.GetExtension(imagen2.FileName);
                    nombreImagen2 = string.Concat(nombre_en_codigo, extension);
                    imagenStream2 = imagen2.OpenReadStream();
                }

                UserAsset userAsset_creado = await _userAssetServicio.Crear(_mapper.Map<UserAsset>(vmUserAsset), imagenStream1, imagenStream2, nombreImagen1, nombreImagen2 );

                vmUserAsset = _mapper.Map<VMUserAsset>(userAsset_creado);

                gResponse.Estado = true;
                gResponse.Objeto = vmUserAsset;

            }
            catch (Exception ex)
            {
                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, gResponse);
        }


        [HttpPut]
        public async Task<IActionResult> Editar([FromForm] IFormFile imagen1, [FromForm] IFormFile imagen2, [FromForm] string modelo)
        {

            GenericResponse<VMUserAsset> gResponse = new GenericResponse<VMUserAsset>();

            try
            {
#pragma warning disable CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL
                VMUserAsset vmUserAsset = JsonConvert.DeserializeObject<VMUserAsset>(modelo);
#pragma warning restore CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL

                string nombreImagen1 = string.Empty;
                string nombreImagen2 = string.Empty;
#pragma warning disable CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL
                Stream imagenStream1 = null;
#pragma warning restore CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL
#pragma warning disable CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL
                Stream imagenStream2 = null;
#pragma warning restore CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL

                if (imagen1 != null)
                {
                    string nombre_en_codigo = Guid.NewGuid().ToString("N");
                    string extension = Path.GetExtension(imagen1.FileName);
                    nombreImagen1 = string.Concat(nombre_en_codigo, extension);
                    imagenStream1 = imagen1.OpenReadStream();
                }

                if (imagen2 != null)
                {
                    string nombre_en_codigo2 = Guid.NewGuid().ToString("N");
                    string extension2 = Path.GetExtension(imagen2.FileName);
                    nombreImagen2 = string.Concat(nombre_en_codigo2, extension2);
                    imagenStream2 = imagen2.OpenReadStream();
                }

#pragma warning disable CS8604 // Posible argumento de referencia nulo
                UserAsset userAsset_editado = await _userAssetServicio.Editar(_mapper.Map<UserAsset>(vmUserAsset), imagenStream1, nombreImagen1, imagenStream2, nombreImagen2);
#pragma warning restore CS8604 // Posible argumento de referencia nulo

                vmUserAsset = _mapper.Map<VMUserAsset>(userAsset_editado);

                gResponse.Estado = true;
                gResponse.Objeto = vmUserAsset;

            }
            catch (Exception ex)
            {
                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, gResponse);
        }


        [HttpDelete]
        public async Task<IActionResult> Eliminar(int idUserAsset)
        {
            GenericResponse<string> gResponse = new GenericResponse<string>();

            try
            {

                gResponse.Estado = await _userAssetServicio.Eliminar(idUserAsset);
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
