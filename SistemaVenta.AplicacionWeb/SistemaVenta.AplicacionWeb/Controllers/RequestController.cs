using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using SistemaVenta.AplicacionWeb.Models.ViewModels;
using SistemaVenta.AplicacionWeb.Utilidades.Response;
using SistemaVenta.BLL.Interfaces;
using SistemaVenta.Entity;

using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace SistemaVenta.AplicacionWeb.Controllers
{
    [Authorize]
    public class RequestController : Controller
    {
        private readonly IAssetRequestTypeService _assetRequestTypeServicio;
        private readonly IPriorityService _listaPriorityServicio;
        private readonly IRequestService _requestServicio;
        private readonly IMapper _mapper;
        private readonly IConverter _converter;

        public RequestController(
            IAssetRequestTypeService assetRequestTypeServicio,
            IPriorityService listaPriorityServicio,
            IRequestService requestServicio,
            IMapper mapper,
            IConverter converter
            )
        {
            _assetRequestTypeServicio = assetRequestTypeServicio;
            _listaPriorityServicio = listaPriorityServicio;
            _requestServicio = requestServicio;
            _mapper = mapper;
            _converter = converter;
        }

        public IActionResult NewRequest()
        {
            return View();
        }

        public IActionResult HistorialRequest()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListaAssetRequestType()
        {

            List<VMAssetRequestType> vmListaRequestAsset = _mapper.Map<List<VMAssetRequestType>>(await _assetRequestTypeServicio.Lista());

            return StatusCode(StatusCodes.Status200OK, vmListaRequestAsset);
        }

        public async Task<IActionResult> ListaPriority()
        {

            List<VMPriority> vmListaPriority = _mapper.Map<List<VMPriority>>(await _listaPriorityServicio.Lista());

            return StatusCode(StatusCodes.Status200OK, vmListaPriority);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerAsset(string busqueda)
        {
            List<VMAsset> vmListaAsset = _mapper.Map<List<VMAsset>>(await _requestServicio.ObtenerAsset(busqueda));

            return StatusCode(StatusCodes.Status200OK, vmListaAsset);
        }



        [HttpPost]
        public async Task<IActionResult> RegistrarRequest([FromBody] VMRequest modelo)
        {

            GenericResponse<VMRequest> gResponse = new GenericResponse<VMRequest>();

            try
            {
                ClaimsPrincipal claimUser = HttpContext.User;



#pragma warning disable CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL
                string idUsuario = claimUser.Claims
                    .Where(c => c.Type == ClaimTypes.NameIdentifier)
                    .Select(c => c.Value).SingleOrDefault();
#pragma warning restore CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL



#pragma warning disable CS8604 // Posible argumento de referencia nulo
                modelo.idUsuario = int.Parse(idUsuario);
#pragma warning restore CS8604 // Posible argumento de referencia nulo


                Request request_creada = await _requestServicio.Registrar(_mapper.Map<Request>(modelo));
                modelo = _mapper.Map<VMRequest>(request_creada);

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



        [HttpGet]
        public async Task<IActionResult> Historial(string numberRequest, string fechaInicio, string fechaFin)
        {

            List<VMRequest> vmHistorialRequest = _mapper.Map<List<VMRequest>>(await _requestServicio.Historial(numberRequest, fechaInicio, fechaFin));

            return StatusCode(StatusCodes.Status200OK, vmHistorialRequest);
        }


        public IActionResult MostrarPDFRequest(string numberRequest)
        {

            string urlPlansheetView = $"{this.Request.Scheme}://{this.Request.Host}/PlanSheet/PDFRequest?numberRequest={numberRequest}";

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = new GlobalSettings()
                {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait,
                },
                Objects = {
                    new ObjectSettings(){
                        Page = urlPlansheetView
                    }
                }
            };

            var filePDF = _converter.Convert(pdf);

            return File(filePDF, "application/pdf");

        }

    }
}

