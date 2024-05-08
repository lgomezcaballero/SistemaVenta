
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using SistemaVenta.BLL.Interfaces;
using SistemaVenta.DAL.Interfaces;
using SistemaVenta.Entity;

namespace SistemaVenta.BLL.Implementacion
{
    public class AssetService : IAssetService
    { 

        private readonly IGenericRepository<Asset> _repositorio;
        private readonly IFireBaseService _fireBaseServicio;

#pragma warning disable CS0169 // El campo 'AssetService.idAsset' nunca se usa
        private int idAsset;
#pragma warning restore CS0169 // El campo 'AssetService.idAsset' nunca se usa

        public AssetService(IGenericRepository<Asset> repositorio,
            IFireBaseService fireBaseServicio)
        {
            _repositorio = repositorio;
            _fireBaseServicio = fireBaseServicio;

        }

        public async Task<List<Asset>> Lista()
        {
            IQueryable<Asset> query = await _repositorio.Consultar();
            return query
                .Include(c => c.IdAssetTypeNavigation)
                .Include(c => c.IdFuelNavigation)
                .Include(c => c.IdInsuranceNavigation)
                .Include(c => c.IdInsurancePTNavigation)
                .Include(c => c.IdSectionalRegistrationNavigation)
                .Include(c => c.IdLocationNavigation)
                .Include(c => c.IdUserAssetNavigation)
                .Include(c => c.IdBusinessNavigation)
                .Include(c => c.IdManagerNavigation)
                .Include(c => c.IdStatusNavigation)

                .ToList();


        }
#pragma warning disable CS8625 // No se puede convertir un literal NULL en un tipo de referencia que no acepta valores NULL.
        public async Task<Asset> Crear(Asset entidad, Stream imagen = null, string NombreImagen = "")
#pragma warning restore CS8625 // No se puede convertir un literal NULL en un tipo de referencia que no acepta valores NULL.
        {
            Asset asset_existe = await _repositorio.Obtener(p => p.licencePlate == entidad.licencePlate);

            if(asset_existe != null)
                throw new TaskCanceledException("The Licence Plate already exist");

#pragma warning disable CS0168 // La variable está declarada pero nunca se usa
            try
            {
                entidad.pictureName = NombreImagen;
                if (imagen != null) {

                    string pictureUrl = await _fireBaseServicio.SubirStorage(imagen, "folder_Asset", NombreImagen);
                    entidad.pictureUrl = pictureUrl;
                
                }

                Asset asset_creado = await _repositorio.Crear(entidad);

                if (asset_creado.idAsset == 0) 
                    throw new TaskCanceledException("Asset could not be created");
                
                IQueryable<Asset> query = await _repositorio.Consultar(p => p.idAsset == asset_creado.idAsset);

                asset_creado =  query
                    .Include(c => c.IdAssetTypeNavigation)
                    .Include(c => c.IdFuelNavigation)
                    .Include(c => c.IdInsuranceNavigation)
                    .Include(c => c.IdInsurancePTNavigation)
                    .Include(c => c.IdSectionalRegistrationNavigation)
                    .Include(c => c.IdLocationNavigation)
                    .Include(c => c.IdUserAssetNavigation)
                    .Include(c => c.IdBusinessNavigation)
                    .Include(c => c.IdManagerNavigation)
                    .Include(c => c.IdStatusNavigation)

                    .First();

                return asset_creado;
            }
            catch (Exception ex) {

                throw;
            }
#pragma warning restore CS0168 // La variable está declarada pero nunca se usa

        }

#pragma warning disable CS8625 // No se puede convertir un literal NULL en un tipo de referencia que no acepta valores NULL.
        public async Task<Asset> Editar(Asset entidad, Stream imagen = null, string NombreImagen = "")
#pragma warning restore CS8625 // No se puede convertir un literal NULL en un tipo de referencia que no acepta valores NULL.
        {
            Asset asset_existe = await _repositorio.Obtener(p => p.licencePlate == entidad.licencePlate && p.idAsset != entidad.idAsset);

            if(asset_existe != null)
                throw new TaskCanceledException("The Licence Plate already exist in another Asset");

            try
            {
                IQueryable<Asset> queryAsset = await _repositorio.Consultar(p => p.idAsset == entidad.idAsset);

                Asset asset_para_editar = queryAsset.First();

                asset_para_editar.inter = entidad.inter;
                asset_para_editar.make = entidad.make;
                asset_para_editar.model = entidad.model;
                asset_para_editar.idAssetType = entidad.idAssetType;
                asset_para_editar.year = entidad.year;
                asset_para_editar.licencePlate = entidad.licencePlate;
                asset_para_editar.vin = entidad.vin;
                asset_para_editar.engine = entidad.engine;
                asset_para_editar.idFuel = entidad.idFuel;
                asset_para_editar.purchasedPrice = entidad.purchasedPrice;
                asset_para_editar.purchasedDate = entidad.purchasedDate;
                asset_para_editar.idInsurance = entidad.idInsurance;
                asset_para_editar.idInsurancePT = entidad.idInsurancePT;
                asset_para_editar.insuranceExpiration = entidad.insuranceExpiration;
                asset_para_editar.idSectionalRegistration = entidad.idSectionalRegistration;
                asset_para_editar.idLocation = entidad.idLocation;
                asset_para_editar.idUserAsset = entidad.idUserAsset;
                asset_para_editar.idBusiness = entidad.idBusiness;
                asset_para_editar.businessType = entidad.businessType;
                asset_para_editar.idManager = entidad.idManager;
                asset_para_editar.idStatus = entidad.idStatus;
                asset_para_editar.board = entidad.board;
     

                if (asset_para_editar.pictureName == "") {
                    asset_para_editar.pictureName = NombreImagen;
                }


                if (imagen != null) {
#pragma warning disable CS8604 // Posible argumento de referencia nulo
                    string pictureUrl = await _fireBaseServicio.SubirStorage(imagen, "folder_Asset", asset_para_editar.pictureName);
#pragma warning restore CS8604 // Posible argumento de referencia nulo
                    asset_para_editar.pictureUrl = pictureUrl;
                }

                bool respuesta = await _repositorio.Editar(asset_para_editar);

                if(!respuesta)
                    throw new TaskCanceledException("Could not edit Asset");


                Asset asset_editado = queryAsset
                    .Include(c => c.IdAssetTypeNavigation)
                    .Include(c => c.IdFuelNavigation)
                    .Include(c => c.IdInsuranceNavigation)
                    .Include(c => c.IdInsurancePTNavigation)
                    .Include(c => c.IdSectionalRegistrationNavigation)
                    .Include(c => c.IdLocationNavigation)
                    .Include(c => c.IdUserAssetNavigation)
                    .Include(c => c.IdBusinessNavigation)
                    .Include(c => c.IdManagerNavigation)
                    .Include(c => c.IdStatusNavigation)

                    .First();

                return asset_editado;

            }
            catch {
                throw;
            }
        }

        public async Task<bool> Eliminar(int idAsset)
        {
            try
            {
                Asset asset_encontrado = await _repositorio.Obtener(p => p.idAsset == idAsset);

                if(asset_encontrado == null)
                    throw new TaskCanceledException("The Asset not exist");

#pragma warning disable CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL
                string nombreImagen = asset_encontrado.pictureName;
#pragma warning restore CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL

                bool respuesta = await _repositorio.Eliminar(asset_encontrado);

                if (respuesta)
#pragma warning disable CS8604 // Posible argumento de referencia nulo
                    await _fireBaseServicio.EliminarStorage("folder_Asset", nombreImagen);
#pragma warning restore CS8604 // Posible argumento de referencia nulo

                return true;

            }
            catch {
                throw;
            }
        }

     
    }
}
