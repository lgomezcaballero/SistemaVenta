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
    public class ManagerService : IManagerService
    {

        private readonly IGenericRepository<Manager> _repositorio;
        private readonly IFireBaseService _fireBaseServicio;

        public ManagerService(IGenericRepository<Manager> repositorio,
            IFireBaseService fireBaseServicio)
        {
            _repositorio = repositorio;
            _fireBaseServicio = fireBaseServicio;

        }

        public async Task<List<Manager>> Lista()
        {
            IQueryable<Manager> query = await _repositorio.Consultar();
            return query.ToList();
        }

#pragma warning disable CS8625 // No se puede convertir un literal NULL en un tipo de referencia que no acepta valores NULL.
        public async Task<Manager> Crear(Manager entidad, Stream imagen = null, string NombreImagen = "")
#pragma warning restore CS8625 // No se puede convertir un literal NULL en un tipo de referencia que no acepta valores NULL.
        {
            Manager manager_existe = await _repositorio.Obtener(p => p.workFile == entidad.workFile);

            if (manager_existe != null)
                throw new TaskCanceledException("This work file already exist");

#pragma warning disable CS0168 // La variable está declarada pero nunca se usa
            try
            {
                entidad.pictureName = NombreImagen;
                if (imagen != null)
                {
                    string urlImage = await _fireBaseServicio.SubirStorage(imagen, "carpeta_producto", NombreImagen);
                    entidad.pictureUrl = urlImage;

                }

                Manager manager_creado = await _repositorio.Crear(entidad);

                if (manager_creado.idManager == 0)
                    throw new TaskCanceledException("Manager could not be created");


                return manager_creado;
            }
            catch (Exception ex)
            {

                throw;
            }
#pragma warning restore CS0168 // La variable está declarada pero nunca se usa

        }

#pragma warning disable CS8625 // No se puede convertir un literal NULL en un tipo de referencia que no acepta valores NULL.
        public async Task<Manager> Editar(Manager entidad, Stream imagen = null, string NombreImagen = "")
#pragma warning restore CS8625 // No se puede convertir un literal NULL en un tipo de referencia que no acepta valores NULL.
        {
            Manager manager_existe = await _repositorio.Obtener(p => p.workFile == entidad.workFile && p.idManager != entidad.idManager);

            if (manager_existe != null)
                throw new TaskCanceledException("This work file already exist");

            try
            {
                IQueryable<Manager> queryProducto = await _repositorio.Consultar(p => p.idManager == entidad.idManager);

                Manager manager_para_editar = queryProducto.First();

                manager_para_editar.name = entidad.name;
                manager_para_editar.workFile = entidad.workFile;
                manager_para_editar.description = entidad.description;
                manager_para_editar.licence = entidad.licence;
                manager_para_editar.licenceExpiration = entidad.licenceExpiration;
                manager_para_editar.birthday = entidad.birthday;
                manager_para_editar.phone = entidad.phone;
                manager_para_editar.emergencyContact = entidad.emergencyContact;
                manager_para_editar.email = entidad.email;
                manager_para_editar.active = entidad.active;

                if (manager_para_editar.pictureName == "")
                {
                    manager_para_editar.pictureName = NombreImagen;
                }

                if (imagen != null)
                {
#pragma warning disable CS8604 // Posible argumento de referencia nulo
                    string urlImagen = await _fireBaseServicio.SubirStorage(imagen, "carpeta_producto", manager_para_editar.pictureName);
#pragma warning restore CS8604 // Posible argumento de referencia nulo
                    manager_para_editar.pictureUrl = urlImagen;
                }

                bool respuesta = await _repositorio.Editar(manager_para_editar);

                if (!respuesta)
                    throw new TaskCanceledException("Manager could not be created");


                return manager_para_editar;

            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int idManager)
        {
            try
            {
                Manager manager_encontrado = await _repositorio.Obtener(p => p.idManager == idManager);

                if (manager_encontrado == null)
                    throw new TaskCanceledException("The Manager not exist");

#pragma warning disable CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL
                string nombreImagen = manager_encontrado.pictureName;
#pragma warning restore CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL

                bool respuesta = await _repositorio.Eliminar(manager_encontrado);

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

