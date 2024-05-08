using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaVenta.Entity;

namespace SistemaVenta.BLL.Interfaces
{
    public interface IManagerService
    {
        Task<List<Manager>> Lista();
#pragma warning disable CS8625 // No se puede convertir un literal NULL en un tipo de referencia que no acepta valores NULL.
        Task<Manager> Crear(Manager entidad, Stream imagen = null, string NombreImagen = "");
#pragma warning restore CS8625 // No se puede convertir un literal NULL en un tipo de referencia que no acepta valores NULL.
#pragma warning disable CS8625 // No se puede convertir un literal NULL en un tipo de referencia que no acepta valores NULL.
        Task<Manager> Editar(Manager entidad, Stream imagen = null, string NombreImagen = "");
#pragma warning restore CS8625 // No se puede convertir un literal NULL en un tipo de referencia que no acepta valores NULL.
        Task<bool> Eliminar(int idManager);

    }
}

