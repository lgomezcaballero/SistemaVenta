using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaVenta.Entity;

namespace SistemaVenta.BLL.Interfaces
{
    public interface IAssetTypeService
    {

        Task<List<AssetType>> Lista();
        Task<AssetType> Crear(AssetType entidad);
        Task<AssetType> Editar(AssetType entidad);
        Task<bool> Eliminar(int idAssetType);

    }
}

