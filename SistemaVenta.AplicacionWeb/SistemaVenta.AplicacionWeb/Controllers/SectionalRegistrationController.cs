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
    public class SectionalRegistrationController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ISectionalRegistrationService _sectionalRegistrationServicio;
        public SectionalRegistrationController(IMapper mapper, ISectionalRegistrationService sectionalRegistrationServicio)
        {
            _mapper = mapper;
            _sectionalRegistrationServicio = sectionalRegistrationServicio;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {

            List<VMSectionalRegistration> vmSectionalRegistrationServiceLista = _mapper.Map<List<VMSectionalRegistration>>(await _sectionalRegistrationServicio.Lista());
            return StatusCode(StatusCodes.Status200OK, new { data = vmSectionalRegistrationServiceLista });

        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] VMSectionalRegistration modelo)
        {
            GenericResponse<VMSectionalRegistration> gResponse = new GenericResponse<VMSectionalRegistration>();

            try
            {
                SectionalRegistration sectionalRegistration_creada = await _sectionalRegistrationServicio.Crear(_mapper.Map<SectionalRegistration>(modelo));
                modelo = _mapper.Map<VMSectionalRegistration>(sectionalRegistration_creada);

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
        public async Task<IActionResult> Editar([FromBody] VMSectionalRegistration modelo)
        {
            GenericResponse<VMSectionalRegistration> gResponse = new GenericResponse<VMSectionalRegistration>();

            try
            {
                SectionalRegistration sectionalRegistration_editada = await _sectionalRegistrationServicio.Editar(_mapper.Map<SectionalRegistration>(modelo));
                modelo = _mapper.Map<VMSectionalRegistration>(sectionalRegistration_editada);

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
        public async Task<IActionResult> Eliminar(int idSectionalRegistration)
        {

            GenericResponse<string> gResponse = new GenericResponse<string>();
            try
            {
                gResponse.Estado = await _sectionalRegistrationServicio.Eliminar(idSectionalRegistration);

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
