using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaVenta.Entity;

namespace SistemaVenta.BLL.Interfaces
{
    public interface IBusinessService
    {

        Task<List<Business>> Lista();
        Task<Business> Crear(Business entidad);
        Task<Business> Editar(Business entidad);
        Task<bool> Eliminar(int idBusiness);

    }
}