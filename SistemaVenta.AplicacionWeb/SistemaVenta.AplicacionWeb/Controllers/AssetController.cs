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
    public class AssetController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAssetService _assetServicio;

        public AssetController(IMapper mapper,
            IAssetService assetServicio)
        {
            _mapper = mapper;
            _assetServicio = assetServicio;

        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<VMAsset> vmAssetLista = _mapper.Map<List<VMAsset>>(await _assetServicio.Lista());

            return StatusCode(StatusCodes.Status200OK, new { data = vmAssetLista });
        }
        [HttpPost]
        public async Task<IActionResult> Crear([FromForm] IFormFile imagen, [FromForm] string modelo)
        {

            GenericResponse<VMAsset> gResponse = new GenericResponse<VMAsset>();

            try
            {
#pragma warning disable CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL
                VMAsset vmAsset = JsonConvert.DeserializeObject<VMAsset>(modelo);
#pragma warning restore CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL

                string pictureName = "";
#pragma warning disable CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL
                Stream pictureStream = null;
#pragma warning restore CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL

                if (imagen != null)
                {
                    string nombre_en_codigo = Guid.NewGuid().ToString("N");
                    string extension = Path.GetExtension(imagen.FileName);
                    pictureName = string.Concat(nombre_en_codigo, extension);
                    pictureStream = imagen.OpenReadStream();
                }

#pragma warning disable CS8604 // Posible argumento de referencia nulo
                Asset asset_creado = await _assetServicio.Crear(_mapper.Map<Asset>(vmAsset), pictureStream, pictureName);
#pragma warning restore CS8604 // Posible argumento de referencia nulo

                vmAsset = _mapper.Map<VMAsset>(asset_creado);

                gResponse.Estado = true;
                gResponse.Objeto = vmAsset;

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

            GenericResponse<VMAsset> gResponse = new GenericResponse<VMAsset>();

            try
            {
#pragma warning disable CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL
                VMAsset vmAsset = JsonConvert.DeserializeObject<VMAsset>(modelo);
#pragma warning restore CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL

                string pictureName = "";
#pragma warning disable CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL
                Stream pictureStream = null;
#pragma warning restore CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL

                if (imagen != null)
                {
                    string nombre_en_codigo = Guid.NewGuid().ToString("N");
                    string extension = Path.GetExtension(imagen.FileName);
                    pictureName = string.Concat(nombre_en_codigo, extension);
                    pictureStream = imagen.OpenReadStream();
                }

#pragma warning disable CS8604 // Posible argumento de referencia nulo
                Asset asset_editado = await _assetServicio.Editar(_mapper.Map<Asset>(vmAsset), pictureStream, pictureName);
#pragma warning restore CS8604 // Posible argumento de referencia nulo

                vmAsset = _mapper.Map<VMAsset>(asset_editado);

                gResponse.Estado = true;
                gResponse.Objeto = vmAsset;

            }
            catch (Exception ex)
            {
                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, gResponse);
        }

        [HttpDelete]
        public async Task<IActionResult> Eliminar(int IdAsset)
        {
            GenericResponse<string> gResponse = new GenericResponse<string>();

            try
            {

                gResponse.Estado = await _assetServicio.Eliminar(IdAsset);
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
