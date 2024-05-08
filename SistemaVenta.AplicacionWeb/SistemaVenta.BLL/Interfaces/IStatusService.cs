using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaVenta.Entity;

namespace SistemaVenta.BLL.Interfaces
{
    public interface IStatusService
    {

        Task<List<Status>> Lista();
        Task<Status> Crear(Status entidad);
        Task<Status> Editar(Status entidad);
        Task<bool> Eliminar(int idStatus);

    }
}

