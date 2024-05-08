using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using SistemaVenta.AplicacionWeb.Models.ViewModels;
using SistemaVenta.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace SistemaVenta.AplicacionWeb.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {

        private readonly IMapper _mapper;
        private readonly IRequestService _requestServicio;

        public ReportController(IMapper mapper, IRequestService requestServicio)
        {
            _mapper = mapper;
            _requestServicio = requestServicio;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> RequestReport(string FechaInicio, string FechaFin)
        {
            List<VMRequestReport> vmLista = _mapper.Map<List<VMRequestReport>>(await _requestServicio.Reporte(FechaInicio, FechaFin));
            return StatusCode(StatusCodes.Status200OK, new { data = vmLista });
        }

    }
}
