using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaVenta.Entity;

namespace SistemaVenta.BLL.Interfaces
{
    public interface IFilesService
    {

        Task<List<Files>> Lista();
        Task<Files> Crear(Files entidad);
        Task<Files> Editar(Files entidad);
        Task<bool> Eliminar(int idFiles);

    }
}
