using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using SistemaVenta.AplicacionWeb.Models.ViewModels;
using SistemaVenta.AplicacionWeb.Utilidades.Response;
using SistemaVenta.BLL.Interfaces;
using SistemaVenta.Entity;
using Microsoft.AspNetCore.Authorization;

namespace SistemaVenta.AplicacionWeb.Controllers
{
    [Authorize]
    public class AssetTypeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAssetTypeService _assetTypeServicio;
        public AssetTypeController(IMapper mapper, IAssetTypeService assetTypeServicio)
        {
            _mapper = mapper;
            _assetTypeServicio = assetTypeServicio;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {

            List<VMAssetType> vmAssetTypeLista = _mapper.Map<List<VMAssetType>>(await _assetTypeServicio.Lista());
            return StatusCode(StatusCodes.Status200OK, new { data = vmAssetTypeLista });

        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] VMAssetType modelo)
        {
            GenericResponse<VMAssetType> gResponse = new GenericResponse<VMAssetType>();

            try
            {
                AssetType assetType_creada = await _assetTypeServicio.Crear(_mapper.Map<AssetType>(modelo));
                modelo = _mapper.Map<VMAssetType>(assetType_creada);

                gResponse.Estado = true;
                gResponse.Objeto = modelo;
            }
            catch (Exception ex)
            {
                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, gResponse);
        }

        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] VMAssetType modelo)
        {
            GenericResponse<VMAssetType> gResponse = new GenericResponse<VMAssetType>();

            try
            {
                AssetType assetType_editada = await _assetTypeServicio.Editar(_mapper.Map<AssetType>(modelo));
                modelo = _mapper.Map<VMAssetType>(assetType_editada);

                gResponse.Estado = true;
                gResponse.Objeto = modelo;

            }


            catch (Exception ex)
            {
                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, gResponse);
        }

        [HttpDelete]
        public async Task<IActionResult> Eliminar(int idAssetType)
        {

            GenericResponse<string> gResponse = new GenericResponse<string>();
            try
            {
                gResponse.Estado = await _assetTypeServicio.Eliminar(idAssetType);

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
