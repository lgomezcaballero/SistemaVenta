using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaVenta.Entity;

namespace SistemaVenta.BLL.Interfaces
{
    public interface IFileTypesService
    {

        Task<List<FileTypes>> Lista();
        Task<FileTypes> Crear(FileTypes entidad);
        Task<FileTypes> Editar(FileTypes entidad);
        Task<bool> Eliminar(int idFileTypes);

    }
}
