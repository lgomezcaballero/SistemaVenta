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
    public class FilesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IFilesService _filesServicio;
        public FilesController(IMapper mapper, IFilesService filesServicio)
        {
            _mapper = mapper;
            _filesServicio = filesServicio;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {

            List<VMFiles> vmFilesLista = _mapper.Map<List<VMFiles>>(await _filesServicio.Lista());
            return StatusCode(StatusCodes.Status200OK, new { data = vmFilesLista });

        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] VMFiles modelo)
        {
            GenericResponse<VMFiles> gResponse = new GenericResponse<VMFiles>();

            try
            {
                Files files_creada = await _filesServicio.Crear(_mapper.Map<Files>(modelo));
                modelo = _mapper.Map<VMFiles>(files_creada);

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
        public async Task<IActionResult> Editar([FromBody] VMFiles modelo)
        {
            GenericResponse<VMFiles> gResponse = new GenericResponse<VMFiles>();

            try
            {
                Files files_editada = await _filesServicio.Editar(_mapper.Map<Files>(modelo));
                modelo = _mapper.Map<VMFiles>(files_editada);

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
        public async Task<IActionResult> Eliminar(int idFiles)
        {

            GenericResponse<string> gResponse = new GenericResponse<string>();
            try
            {
                gResponse.Estado = await _filesServicio.Eliminar(idFiles);

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

