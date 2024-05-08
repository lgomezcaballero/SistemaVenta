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
    public class InsurancePTController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IInsurancePTService _insurancePTServicio;
        public InsurancePTController(IMapper mapper, IInsurancePTService insurancePTServicio)
        {
            _mapper = mapper;
            _insurancePTServicio = insurancePTServicio;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {

            List<VMInsurancePT> vmInsurancePTLista = _mapper.Map<List<VMInsurancePT>>(await _insurancePTServicio.Lista());
            return StatusCode(StatusCodes.Status200OK, new { data = vmInsurancePTLista });

        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] VMInsurancePT modelo)
        {
            GenericResponse<VMInsurancePT> gResponse = new GenericResponse<VMInsurancePT>();

            try
            {
                InsurancePT insurancePT_creada = await _insurancePTServicio.Crear(_mapper.Map<InsurancePT>(modelo));
                modelo = _mapper.Map<VMInsurancePT>(insurancePT_creada);

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
        public async Task<IActionResult> Editar([FromBody] VMInsurancePT modelo)
        {
            GenericResponse<VMInsurancePT> gResponse = new GenericResponse<VMInsurancePT>();

            try
            {
                InsurancePT insurancePT_editada = await _insurancePTServicio.Editar(_mapper.Map<InsurancePT>(modelo));
                modelo = _mapper.Map<VMInsurancePT>(insurancePT_editada);

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
        public async Task<IActionResult> Eliminar(int idInsurancePT)
        {

            GenericResponse<string> gResponse = new GenericResponse<string>();
            try
            {
                gResponse.Estado = await _insurancePTServicio.Eliminar(idInsurancePT);

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
