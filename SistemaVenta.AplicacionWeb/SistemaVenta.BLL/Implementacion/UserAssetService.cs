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
    public class UserAssetService : IUserAssetService
    {


        private readonly IGenericRepository<UserAsset> _repositorio;
        private readonly IFireBaseService _fireBaseServicio;

        public UserAssetService(IGenericRepository<UserAsset> repositorio,
            IFireBaseService fireBaseServicio)
        {
            _repositorio = repositorio;
            _fireBaseServicio = fireBaseServicio;

        }

        public async Task<List<UserAsset>> Lista()
        {
            IQueryable<UserAsset> query = await _repositorio.Consultar();
            return query.ToList();
        }


#pragma warning disable CS8625 // No se puede convertir un literal NULL en un tipo de referencia que no acepta valores NULL.
        public async Task<UserAsset> Crear(UserAsset entidad, Stream imagen1 = null, Stream imagen2 = null, string NombreImagen1 = "", string nombreImagen2 = "")
#pragma warning restore CS8625 // No se puede convertir un literal NULL en un tipo de referencia que no acepta valores NULL.

        {
            UserAsset userAsset_existe = await _repositorio.Obtener(p => p.workFile == entidad.workFile);

            if (userAsset_existe != null)
                throw new TaskCanceledException("This work file already exist");



#pragma warning disable CS0168 // La variable está declarada pero nunca se usa
            try
            {
                entidad.pictureName = NombreImagen1;
                if (imagen1 != null)
                {
                    string urlImage = await _fireBaseServicio.SubirStorage(imagen1, "carpeta_producto", NombreImagen1);
                    entidad.pictureName = NombreImagen1;
                    entidad.pictureUrl = urlImage;
                }

                if (imagen2 != null)
                {
                    string urlImage = await _fireBaseServicio.SubirStorage(imagen2, "carpeta_producto", nombreImagen2);
                    entidad.fileLicenceName = nombreImagen2;
                    entidad.fileLicenceUrl = urlImage;
                }


                UserAsset userAsset_creado = await _repositorio.Crear(entidad);

                if (userAsset_creado.idUserAsset == 0)
                    throw new TaskCanceledException("Asset user could not be created");


                return userAsset_creado;
            }
            catch (Exception ex)
            {

                throw;
            }
#pragma warning restore CS0168 // La variable está declarada pero nunca se usa
#pragma warning restore CS0168 // La variable está declarada pero nunca se usa


        }


        public async Task<UserAsset> Editar(UserAsset entidad, Stream imagen1 = null, string NombreImagen1 = "", Stream imagen2 = null, string NombreImagen2 = "")

        {
            UserAsset producto_existe = await _repositorio.Obtener(p => p.workFile == entidad.workFile && p.idUserAsset != entidad.idUserAsset);

            if (producto_existe != null)
                throw new TaskCanceledException("This work file already exist");

            try
            {
                IQueryable<UserAsset> queryProducto = await _repositorio.Consultar(p => p.idUserAsset == entidad.idUserAsset);

                UserAsset userAsset_para_editar = queryProducto.First();

                userAsset_para_editar.name = entidad.name;
                userAsset_para_editar.workFile = entidad.workFile;
                userAsset_para_editar.licence = entidad.licence;
                userAsset_para_editar.licenceExpiration = entidad.licenceExpiration;
                userAsset_para_editar.birthday = entidad.birthday;
                userAsset_para_editar.phone = entidad.phone;
                userAsset_para_editar.emergencyContact = entidad.emergencyContact;
                userAsset_para_editar.email = entidad.email;
                userAsset_para_editar.active = entidad.active;

                if (userAsset_para_editar.pictureName == "")
                {
                    userAsset_para_editar.pictureName = NombreImagen1;
                }

                if (userAsset_para_editar.fileLicenceName == "")
                {
                    userAsset_para_editar.fileLicenceName = NombreImagen2;
                }

                if (imagen1 != null && !string.IsNullOrWhiteSpace(NombreImagen1))
                {
                    string urlImagen1 = await _fireBaseServicio.SubirStorage(imagen1, "carpeta_producto", userAsset_para_editar.pictureName ?? string.Empty);

                    userAsset_para_editar.pictureUrl = urlImagen1;
                }

                if (imagen2 != null && !string.IsNullOrWhiteSpace(NombreImagen2))
                {
                    string urlImagen2 = await _fireBaseServicio.SubirStorage(imagen2, "carpeta_producto", userAsset_para_editar.fileLicenceName ?? string.Empty);

                    userAsset_para_editar.fileLicenceUrl = urlImagen2;

                }

                bool respuesta = await _repositorio.Editar(userAsset_para_editar);

                if (!respuesta)
                    throw new TaskCanceledException("Asset user could not be created");


                return userAsset_para_editar;

            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int idUserAsset)
        {
            try
            {
                UserAsset userAsset_encontrado = await _repositorio.Obtener(p => p.idUserAsset == idUserAsset);

                if (userAsset_encontrado == null)
                    throw new TaskCanceledException("The user asset not exist");

#pragma warning disable CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL
                string nombreImagen = userAsset_encontrado.pictureName;
#pragma warning restore CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL

                bool respuesta = await _repositorio.Eliminar(userAsset_encontrado);

                if (respuesta)
#pragma warning disable CS8604 // Posible argumento de referencia nulo
                    await _fireBaseServicio.EliminarStorage("carpeta_producto", nombreImagen);
#pragma warning restore CS8604 // Posible argumento de referencia nulo

                return true;

            }
            catch
            {
                throw;
            }
        }


    }
}
