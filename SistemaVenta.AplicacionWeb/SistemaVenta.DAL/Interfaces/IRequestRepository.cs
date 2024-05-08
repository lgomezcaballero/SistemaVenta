using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaVenta.Entity;

namespace SistemaVenta.DAL.Interfaces
{
    public interface IRequestRepository : IGenericRepository<Request>
    {
        Task<Request> Registrar(Request entidad);
        Task<List<RequestDetail>> Reporte(DateTime FechaInicio, DateTime FechaFin);
    }
}