using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SistemaVenta.AplicacionWeb.Models.ViewModels;
using SistemaVenta.AplicacionWeb.Utilidades.Response;
using SistemaVenta.BLL.Interfaces;

namespace SistemaVenta.AplicacionWeb.Controllers
{
    [Authorize]
    public class DashBoardController : Controller
    {

        private readonly IDashBoardService _dashboardServicio;

        public DashBoardController(IDashBoardService dashboardServicio)
        {
            _dashboardServicio = dashboardServicio;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult>ObtenerResumen()
        {
            GenericResponse<VMDashBoard> gResponse = new GenericResponse<VMDashBoard>();
            try
            {
                VMDashBoard vmDashBoard = new VMDashBoard();

                vmDashBoard.TotalRequest = await _dashboardServicio.TotalRequestUltimaSemana();
                vmDashBoard.TotalAssets = await _dashboardServicio.TotalAssets();
                vmDashBoard.TotalLocations = await _dashboardServicio.TotalLocations();
                vmDashBoard.TotalManagers = await _dashboardServicio.TotalManagers();
                vmDashBoard.TotalUserAssets = await _dashboardServicio.TotalUserAssets();                
                vmDashBoard.TotalBusiness = await _dashboardServicio.TotalBusiness();

                // Obtener el diccionario AssetVsStatus
                Dictionary<string, int> assetStatusDictionary = await _dashboardServicio.AssetVsStatus();

                // Convertir el diccionario a una lista de VMAssetVsStatus
                vmDashBoard.AssetVsStatus = assetStatusDictionary
                    .Select(item => new VMAssetVsStatus
                    {
                        Status = item.Key,
                        Cantidad = item.Value
                    })
                    .ToList();

                List<VMRequestSemana> listaRequestSemana = new List<VMRequestSemana>();
                List<VMAssetsSemana> listaAssetsSemana = new List<VMAssetsSemana>();
                

                foreach (KeyValuePair<string, int> item in await _dashboardServicio.RequestUltimaSemana()) {
                    listaRequestSemana.Add(new VMRequestSemana()
                    {
                        date = item.Key,
                        total = item.Value
                    });
                }

               

                foreach (KeyValuePair<string, int> item in await _dashboardServicio.AssetTopUltimaSemana())
                {
                    listaAssetsSemana.Add(new VMAssetsSemana()
                    {
                        Asset = item.Key,
                        amount = item.Value
                    });
                }

                vmDashBoard.RequestUltimaSemana = listaRequestSemana;
                vmDashBoard.AssetsTopUltimaSemana = listaAssetsSemana;
               

                gResponse.Estado = true;
                gResponse.Objeto = vmDashBoard;

            }
            catch(Exception ex) {
                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message;
            }


            return StatusCode(StatusCodes.Status200OK, gResponse);
        }
    }
}
