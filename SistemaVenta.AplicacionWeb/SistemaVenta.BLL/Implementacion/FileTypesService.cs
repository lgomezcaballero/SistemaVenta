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
    public class FileTypesService : IFileTypesService
    {
        private readonly IGenericRepository<FileTypes> _repositorio;

#pragma warning disable CS0169 // El campo 'FileTypesService.idFileTypes' nunca se usa
        private int idFileTypes;
#pragma warning restore CS0169 // El campo 'FileTypesService.idFileTypes' nunca se usa

        public FileTypesService(IGenericRepository<FileTypes> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<FileTypes>> Lista()
        {
            IQueryable<FileTypes> query = await _repositorio.Consultar();
            return query.ToList();
        }
        public async Task<FileTypes> Crear(FileTypes entidad)
        {
            try
            {
                FileTypes fileTypes_creada = await _repositorio.Crear(entidad);
                if (fileTypes_creada.idFileTypes == 0)
                    throw new TaskCanceledException("No se pudo crear el FileTypes");

                return fileTypes_creada;
            }
            catch
            {
                throw;
            }
        }

        public async Task<FileTypes> Editar(FileTypes entidad)
        {
            try
            {
                FileTypes fileTypes_encontrada = await _repositorio.Obtener(c => c.idFileTypes == entidad.idFileTypes);
                fileTypes_encontrada.description = entidad.description;
                fileTypes_encontrada.active = entidad.active;

                bool respuesta = await _repositorio.Editar(fileTypes_encontrada);

                if (!respuesta)
                    throw new TaskCanceledException("No se pudo modificar el FileTypes");

                return fileTypes_encontrada;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int idFileTypes)
        {
            try
            {
                FileTypes fileTypes_encontrada = await _repositorio.Obtener(c => c.idFileTypes == idFileTypes);

                if (fileTypes_encontrada == null)
                    throw new TaskCanceledException("El File Type no existe");

                bool respuesta = await _repositorio.Eliminar(fileTypes_encontrada);

                return respuesta;
            }
            catch
            {
                throw;
            }
        }


    }
}

