using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaVenta.Entity;

namespace SistemaVenta.BLL.Interfaces
{
    public interface IRequestService
    {
        Task<List<Asset>> ObtenerAsset(string busqueda);

        Task<Request> Registrar(Request entidad);

        Task<List<Request>> Historial(string numberRequest, string fechaInicio, string fechaFin);

        Task<Request> Detalle(string numberRequest);
        Task<List<RequestDetail>> Reporte(string fechaInicio, string fechaFin);

    }
}

