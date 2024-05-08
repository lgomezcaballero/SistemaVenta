using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaVenta.BLL.Interfaces;
using SistemaVenta.DAL.Interfaces;
using SistemaVenta.Entity;

namespace SistemaVenta.BLL.Implementacion
{
    public class AssetTypeService : IAssetTypeService
    {
        private readonly IGenericRepository<AssetType> _repositorio;
#pragma warning disable CS0169 // El campo 'AssetTypeService.idAssetType' nunca se usa
        private int idAssetType;
#pragma warning restore CS0169 // El campo 'AssetTypeService.idAssetType' nunca se usa

        public AssetTypeService(IGenericRepository<AssetType> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<AssetType>> Lista()
        {
            IQueryable<AssetType> query = await _repositorio.Consultar();
            return query.ToList();
        }
        public async Task<AssetType> Crear(AssetType entidad)
        {
            try
            {
                AssetType assetType_creada = await _repositorio.Crear(entidad);
                if (assetType_creada.idAssetType == 0)
                    throw new TaskCanceledException("No se pudo crear el Asset Type");

                return assetType_creada;
            }
            catch
            {
                throw;
            }
        }

        public async Task<AssetType> Editar(AssetType entidad)
        {
            try
            {
                AssetType assetType_encontrada = await _repositorio.Obtener(c => c.idAssetType == entidad.idAssetType);
                assetType_encontrada.description = entidad.description;
                assetType_encontrada.active = entidad.active;

                bool respuesta = await _repositorio.Editar(assetType_encontrada);

                if (!respuesta)
                    throw new TaskCanceledException("No se pudo modificar el Asset Type");

                return assetType_encontrada;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int idAssetType)
        {
            try
            {
                AssetType assetType_encontrada = await _repositorio.Obtener(c => c.idAssetType == idAssetType);

                if (assetType_encontrada == null)
                    throw new TaskCanceledException("El Asset Type no existe");

                bool respuesta = await _repositorio.Eliminar(assetType_encontrada);

                return respuesta;
            }
            catch
            {
                throw;
            }
        }


    }
}

