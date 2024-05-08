using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using SistemaVenta.AplicacionWeb.Models.ViewModels;
using SistemaVenta.AplicacionWeb.Utilidades.Response;
using SistemaVenta.BLL.Interfaces;
using SistemaVenta.Entity;
using Microsoft.AspNetCore.Authorization;
using System.Net.NetworkInformation;

namespace SistemaVenta.AplicacionWeb.Controllers
{
    [Authorize]
    public class StatusController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IStatusService _statusServicio;
        public StatusController(IMapper mapper, IStatusService statusServicio)
        {
            _mapper = mapper;
            _statusServicio = statusServicio;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {

            List<VMStatus> vmStatusLista = _mapper.Map<List<VMStatus>>(await _statusServicio.Lista());
            return StatusCode(StatusCodes.Status200OK, new { data = vmStatusLista });

        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] VMStatus modelo)
        {
            GenericResponse<VMStatus> gResponse = new GenericResponse<VMStatus>();

            try
            {
                Status status_creada = await _statusServicio.Crear(_mapper.Map<Status>(modelo));
                modelo = _mapper.Map<VMStatus>(status_creada);

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
        public async Task<IActionResult> Editar([FromBody] VMStatus modelo)
        {
            GenericResponse<VMStatus> gResponse = new GenericResponse<VMStatus>();

            try
            {
                Status status_editada = await _statusServicio.Editar(_mapper.Map<Status>(modelo));
                modelo = _mapper.Map<VMStatus>(status_editada);

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
        public async Task<IActionResult> Eliminar(int idStatus)
        {

            GenericResponse<string> gResponse = new GenericResponse<string>();
            try
            {
                gResponse.Estado = await _statusServicio.Eliminar(idStatus);

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
