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
    public class BusinessController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IBusinessService _businessServicio;
        public BusinessController(IMapper mapper, IBusinessService businessServicio)
        {
            _mapper = mapper;
            _businessServicio = businessServicio;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {

            List<VMBusiness> vmBusinessServiceLista = _mapper.Map<List<VMBusiness>>(await _businessServicio.Lista());
            return StatusCode(StatusCodes.Status200OK, new { data = vmBusinessServiceLista });

        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] VMBusiness modelo)
        {
            GenericResponse<VMBusiness> gResponse = new GenericResponse<VMBusiness>();

            try
            {
                Business business_creada = await _businessServicio.Crear(_mapper.Map<Business>(modelo));
                modelo = _mapper.Map<VMBusiness>(business_creada);

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
        public async Task<IActionResult> Editar([FromBody] VMBusiness modelo)
        {
            GenericResponse<VMBusiness> gResponse = new GenericResponse<VMBusiness>();

            try
            {
                Business business_editada = await _businessServicio.Editar(_mapper.Map<Business>(modelo));
                modelo = _mapper.Map<VMBusiness>(business_editada);

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
        public async Task<IActionResult> Eliminar(int idBusiness)
        {

            GenericResponse<string> gResponse = new GenericResponse<string>();
            try
            {
                gResponse.Estado = await _businessServicio.Eliminar(idBusiness);

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
