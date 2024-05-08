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
    public class AssetRequestTypeService : IAssetRequestTypeService
    {
        private readonly IGenericRepository<AssetRequestType> _repositorio;

        public AssetRequestTypeService(IGenericRepository<AssetRequestType> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<AssetRequestType>> Lista()
        {
            IQueryable<AssetRequestType> query = await _repositorio.Consultar();
            return query.ToList();
        }
    }
}
