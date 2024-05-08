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
    public class DailyReportController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IDailyReportService _dailyReportServicio;
        public DailyReportController(IMapper mapper, IDailyReportService dailyReportServicio)
        {
            _mapper = mapper;
            _dailyReportServicio = dailyReportServicio;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {

            List<VMDailyReport> vmDailyReportLista = _mapper.Map<List<VMDailyReport>>(await _dailyReportServicio.Lista());
            return StatusCode(StatusCodes.Status200OK, new { data = vmDailyReportLista });

        }

        [HttpGet]
        public async Task<IActionResult> ObtenerAsset(string busqueda)
        {
            List<VMAsset> vmListaAsset = _mapper.Map<List<VMAsset>>(await _dailyReportServicio.ObtenerAsset(busqueda));

            return StatusCode(StatusCodes.Status200OK, vmListaAsset);
        }


        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] VMDailyReport modelo)
        {
            GenericResponse<VMDailyReport> gResponse = new GenericResponse<VMDailyReport>();

            try
            {
                DailyReport dailyReport_creada = await _dailyReportServicio.Crear(_mapper.Map<DailyReport>(modelo));
                modelo = _mapper.Map<VMDailyReport>(dailyReport_creada);

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
        public async Task<IActionResult> Editar([FromBody] VMDailyReport modelo)
        {
            GenericResponse<VMDailyReport> gResponse = new GenericResponse<VMDailyReport>();

            try
            {
                DailyReport dailyReport_editada = await _dailyReportServicio.Editar(_mapper.Map<DailyReport>(modelo));
                modelo = _mapper.Map<VMDailyReport>(dailyReport_editada);

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
        public async Task<IActionResult> Eliminar(int idDailyReport)
        {

            GenericResponse<string> gResponse = new GenericResponse<string>();
            try
            {
                gResponse.Estado = await _dailyReportServicio.Eliminar(idDailyReport);

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
