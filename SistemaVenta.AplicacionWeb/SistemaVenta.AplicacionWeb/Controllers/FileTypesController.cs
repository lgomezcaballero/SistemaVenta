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
    public class FileTypesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IFileTypesService _fileTypesServicio;
        public FileTypesController(IMapper mapper, IFileTypesService fileTypesServicio)
        {
            _mapper = mapper;
            _fileTypesServicio = fileTypesServicio;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {

            List<VMFileTypes> vmFileTypeLista = _mapper.Map<List<VMFileTypes>>(await _fileTypesServicio.Lista());
            return StatusCode(StatusCodes.Status200OK, new { data = vmFileTypeLista });

        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] VMFileTypes modelo)
        {
            GenericResponse<VMFileTypes> gResponse = new GenericResponse<VMFileTypes>();

            try
            {
                FileTypes fileTypes_creada = await _fileTypesServicio.Crear(_mapper.Map<FileTypes>(modelo));
                modelo = _mapper.Map<VMFileTypes>(fileTypes_creada);

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
        public async Task<IActionResult> Editar([FromBody] VMFileTypes modelo)
        {
            GenericResponse<VMFileTypes> gResponse = new GenericResponse<VMFileTypes>();

            try
            {
                FileTypes fileTypes_editada = await _fileTypesServicio.Editar(_mapper.Map<FileTypes>(modelo));
                modelo = _mapper.Map<VMFileTypes>(fileTypes_editada);

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
        public async Task<IActionResult> Eliminar(int idFileTypes)
        {

            GenericResponse<string> gResponse = new GenericResponse<string>();
            try
            {
                gResponse.Estado = await _fileTypesServicio.Eliminar(idFileTypes);

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

