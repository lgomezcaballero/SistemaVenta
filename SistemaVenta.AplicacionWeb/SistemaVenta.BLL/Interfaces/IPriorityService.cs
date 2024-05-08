using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaVenta.Entity;

namespace SistemaVenta.BLL.Interfaces
{
    public interface IPriorityService
    {

        Task<List<Priority>> Lista();
        Task<Priority> Crear(Priority entidad);
        Task<Priority> Editar(Priority entidad);
        Task<bool> Eliminar(int idPriority);

    }
}
