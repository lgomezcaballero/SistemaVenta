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
    public class PriorityController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPriorityService _priorityServicio;
        public PriorityController(IMapper mapper, IPriorityService priorityServicio)
        {
            _mapper = mapper;
            _priorityServicio = priorityServicio;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {

            List<VMPriority> vmPriorityLista = _mapper.Map<List<VMPriority>>(await _priorityServicio.Lista());
            return StatusCode(StatusCodes.Status200OK, new { data = vmPriorityLista });

        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] VMPriority modelo)
        {
            GenericResponse<VMPriority> gResponse = new GenericResponse<VMPriority>();

            try
            {
                Priority priority_creada = await _priorityServicio.Crear(_mapper.Map<Priority>(modelo));
                modelo = _mapper.Map<VMPriority>(priority_creada);

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
        public async Task<IActionResult> Editar([FromBody] VMPriority modelo)
        {
            GenericResponse<VMPriority> gResponse = new GenericResponse<VMPriority>();

            try
            {
                Priority priority_editada = await _priorityServicio.Editar(_mapper.Map<Priority>(modelo));
                modelo = _mapper.Map<VMPriority>(priority_editada);

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
        public async Task<IActionResult> Eliminar(int idPriority)
        {

            GenericResponse<string> gResponse = new GenericResponse<string>();
            try
            {
                gResponse.Estado = await _priorityServicio.Eliminar(idPriority);

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
