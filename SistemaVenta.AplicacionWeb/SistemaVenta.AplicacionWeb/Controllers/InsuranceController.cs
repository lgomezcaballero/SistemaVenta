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
    public class InsuranceController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IInsuranceService _insuranceServicio;
        public InsuranceController(IMapper mapper, IInsuranceService insuranceServicio)
        {
            _mapper = mapper;
            _insuranceServicio = insuranceServicio;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {

            List<VMInsurance> vmInsuranceLista = _mapper.Map<List<VMInsurance>>(await _insuranceServicio.Lista());
            return StatusCode(StatusCodes.Status200OK, new { data = vmInsuranceLista });

        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] VMInsurance modelo)
        {
            GenericResponse<VMInsurance> gResponse = new GenericResponse<VMInsurance>();

            try
            {
                Insurance insurance_creada = await _insuranceServicio.Crear(_mapper.Map<Insurance>(modelo));
                modelo = _mapper.Map<VMInsurance>(insurance_creada);

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
        public async Task<IActionResult> Editar([FromBody] VMInsurance modelo)
        {
            GenericResponse<VMInsurance> gResponse = new GenericResponse<VMInsurance>();

            try
            {
                Insurance insurance_editada = await _insuranceServicio.Editar(_mapper.Map<Insurance>(modelo));
                modelo = _mapper.Map<VMInsurance>(insurance_editada);

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
        public async Task<IActionResult> Eliminar(int idInsurance)
        {

            GenericResponse<string> gResponse = new GenericResponse<string>();
            try
            {
                gResponse.Estado = await _insuranceServicio.Eliminar(idInsurance);

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
