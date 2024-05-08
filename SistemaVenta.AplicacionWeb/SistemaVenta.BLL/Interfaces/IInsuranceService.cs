using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaVenta.Entity;

namespace SistemaVenta.BLL.Interfaces
{
    public interface IInsuranceService
    {

        Task<List<Insurance>> Lista();
        Task<Insurance> Crear(Insurance entidad);
        Task<Insurance> Editar(Insurance entidad);
        Task<bool> Eliminar(int idInsurance);

    }
}

