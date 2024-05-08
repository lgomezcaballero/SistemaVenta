using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaVenta.Entity;

namespace SistemaVenta.BLL.Interfaces
{
    public interface IDailyReportService
    {

        Task<List<DailyReport>> Lista();
        Task<DailyReport> Crear(DailyReport entidad);
        Task<DailyReport> Editar(DailyReport entidad);
        Task<bool> Eliminar(int idDailyReport);
        Task<List<Asset>> ObtenerAsset(string busqueda);
    }
}


