using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaVenta.Entity;

namespace SistemaVenta.BLL.Interfaces
{
    public interface ILocationService
    {
        Task<List<Location>> Lista();
        Task<Location> Crear(Location entidad);
        Task<Location> Editar(Location entidad);
        Task<bool> Eliminar(int idLocation);
    }
}
