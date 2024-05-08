using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaVenta.Entity;

namespace SistemaVenta.BLL.Interfaces
{
    public interface IFuelService
    {

        Task<List<Fuel>> Lista();
        Task<Fuel> Crear(Fuel entidad);
        Task<Fuel> Editar(Fuel entidad);
        Task<bool> Eliminar(int idFuel);

    }
}
