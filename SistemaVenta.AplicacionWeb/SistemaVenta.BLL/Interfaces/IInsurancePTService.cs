using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaVenta.Entity;

namespace SistemaVenta.BLL.Interfaces
{
    public interface IInsurancePTService
    {

        Task<List<InsurancePT>> Lista();
        Task<InsurancePT> Crear(InsurancePT entidad);
        Task<InsurancePT> Editar(InsurancePT entidad);
        Task<bool> Eliminar(int idInsurancePT);

    }
}
