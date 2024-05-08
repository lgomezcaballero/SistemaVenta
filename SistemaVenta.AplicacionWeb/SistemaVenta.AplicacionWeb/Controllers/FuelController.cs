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
    public class FuelController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IFuelService _fuelServicio;
        public FuelController(IMapper mapper, IFuelService fuelServicio)
        {
            _mapper = mapper;
            _fuelServicio = fuelServicio;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {

            List<VMFuel> vmFuelLista = _mapper.Map<List<VMFuel>>(await _fuelServicio.Lista());
            return StatusCode(StatusCodes.Status200OK, new { data = vmFuelLista });

        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] VMFuel modelo)
        {
            GenericResponse<VMFuel> gResponse = new GenericResponse<VMFuel>();

            try
            {
                Fuel fuel_creada = await _fuelServicio.Crear(_mapper.Map<Fuel>(modelo));
                modelo = _mapper.Map<VMFuel>(fuel_creada);

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
        public async Task<IActionResult> Editar([FromBody] VMFuel modelo)
        {
            GenericResponse<VMFuel> gResponse = new GenericResponse<VMFuel>();

            try
            {
                Fuel fuel_editada = await _fuelServicio.Editar(_mapper.Map<Fuel>(modelo));
                modelo = _mapper.Map<VMFuel>(fuel_editada);

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
        public async Task<IActionResult> Eliminar(int idFuel)
        {

            GenericResponse<string> gResponse = new GenericResponse<string>();
            try
            {
                gResponse.Estado = await _fuelServicio.Eliminar(idFuel);

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
