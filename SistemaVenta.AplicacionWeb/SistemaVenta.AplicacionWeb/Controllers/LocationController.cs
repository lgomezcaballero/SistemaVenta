using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using SistemaVenta.AplicacionWeb.Models.ViewModels;
using SistemaVenta.AplicacionWeb.Utilidades.Response;
using SistemaVenta.BLL.Interfaces;
using SistemaVenta.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis;
using Location = SistemaVenta.Entity.Location;

namespace SistemaVenta.AplicacionWeb.Controllers
{
    [Authorize]
    public class LocationController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILocationService _locationServicio;
        public LocationController(IMapper mapper, ILocationService locationServicio)
        {
            _mapper = mapper;
            _locationServicio = locationServicio;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {

            List<VMLocation> vmLocationLista = _mapper.Map<List<VMLocation>>(await _locationServicio.Lista());
            return StatusCode(StatusCodes.Status200OK, new { data = vmLocationLista });

        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] VMLocation modelo)
        {
            GenericResponse<VMLocation> gResponse = new GenericResponse<VMLocation>();

            try
            {
                Location location_creada = await _locationServicio.Crear(_mapper.Map<Location>(modelo));
                modelo = _mapper.Map<VMLocation>(location_creada);

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
        public async Task<IActionResult> Editar([FromBody] VMLocation modelo)
        {
            GenericResponse<VMLocation> gResponse = new GenericResponse<VMLocation>();

            try
            {
                Location location_editada = await _locationServicio.Editar(_mapper.Map<Location>(modelo));
                modelo = _mapper.Map<VMLocation>(location_editada);

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
        public async Task<IActionResult> Eliminar(int idLocation)
        {

            GenericResponse<string> gResponse = new GenericResponse<string>();
            try
            {
                gResponse.Estado = await _locationServicio.Eliminar(idLocation);

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
