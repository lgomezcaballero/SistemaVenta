using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaVenta.Entity;

namespace SistemaVenta.BLL.Interfaces
{
    public interface ISectionalRegistrationService
    {

        Task<List<SectionalRegistration>> Lista();
        Task<SectionalRegistration> Crear(SectionalRegistration entidad);
        Task<SectionalRegistration> Editar(SectionalRegistration entidad);
        Task<bool> Eliminar(int idSectionalRegistration);

    }
}
