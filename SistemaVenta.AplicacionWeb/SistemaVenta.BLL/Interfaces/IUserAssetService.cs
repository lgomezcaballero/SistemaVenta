using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaVenta.Entity;

namespace SistemaVenta.BLL.Interfaces
{
    public interface IUserAssetService
    {
        Task<List<UserAsset>> Lista();
#pragma warning disable CS8625 // No se puede convertir un literal NULL en un tipo de referencia que no acepta valores NULL.
        Task<UserAsset> Crear(UserAsset entidad, Stream imagen1 = null, Stream imagen2 = null, string NombreImagen1 = "", string nombreImagen2 = "");
#pragma warning restore CS8625 // No se puede convertir un literal NULL en un tipo de referencia que no acepta valores NULL.
#pragma warning disable CS8625 // No se puede convertir un literal NULL en un tipo de referencia que no acepta valores NULL.
        Task<UserAsset> Editar(UserAsset entidad, Stream imagen1 = null, string NombreImagen1 = "", Stream imagen2 = null, string NombreImagen2 = null);
#pragma warning restore CS8625 // No se puede convertir un literal NULL en un tipo de referencia que no acepta valores NULL.
        Task<bool> Eliminar(int idUserAsset);

    }
}

