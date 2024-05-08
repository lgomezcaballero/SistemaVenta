using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using SistemaVenta.AplicacionWeb.Models.ViewModels;
using SistemaVenta.BLL.Interfaces;

namespace SistemaVenta.AplicacionWeb.Controllers
{
    public class PlanSheetController : Controller
    {
        private readonly IMapper _mapper;
        private readonly INegocioService _negocioServicio;
        private readonly IRequestService _requestServicio;



        public PlanSheetController(IMapper mapper,
            INegocioService negocioServicio,
            IRequestService requestServicio)
        {
            _mapper = mapper;
            _negocioServicio = negocioServicio;
            _requestServicio = requestServicio;
        }
        public IActionResult EnviarClave(string correo, string clave)
        {
            ViewData["Correo"] = correo;
            ViewData["Clave"] = clave;
            ViewData["Url"] = $"{this.Request.Scheme}://{this.Request.Host}";

            return View();
        }

        public async Task<IActionResult> PDFRequest(string numberRequest)
        {

            VMRequest vmRequest = _mapper.Map<VMRequest>(await _requestServicio.Detalle(numberRequest));
            VMNegocio vmNegocio = _mapper.Map<VMNegocio>(await _negocioServicio.Obtener());

            VMPDFRequest modelo = new VMPDFRequest();

            modelo.negocio = vmNegocio;
            modelo.request = vmRequest;

            return View(modelo);
        }



        public IActionResult RestablecerClave(string clave)
        {
            ViewData["Clave"] = clave;
            return View();
        }
    }
}
