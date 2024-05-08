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
    public class FilesService : IFilesService
    {
        private readonly IGenericRepository<Files> _repositorio;

#pragma warning disable CS0169 // El campo 'FilesService.idFiles' nunca se usa
        private int idFiles;
#pragma warning restore CS0169 // El campo 'FilesService.idFiles' nunca se usa

        public FilesService(IGenericRepository<Files> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<Files>> Lista()
        {
            IQueryable<Files> query = await _repositorio.Consultar();
            return query
                .Include(c => c.IdAssetNavigation)
                .Include(c => c.IdFileTypesNavigation)
                .ToList();
        }
        public async Task<Files> Crear(Files entidad)
        {
            try
            {
                Files files_creada = await _repositorio.Crear(entidad);

                if (files_creada.idFileTypes == 0)
                    throw new TaskCanceledException("No se pudo crear el File");

                IQueryable<Files> query = await _repositorio.Consultar(p => p.idFiles == files_creada.idFiles);

                files_creada = query
                    .Include(c => c.IdAssetNavigation)
                    .Include(c => c.IdFileTypesNavigation)
      
                    

                    .First();




                return files_creada;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Files> Editar(Files entidad)
        {
            try
            {
                Files files_encontrada = await _repositorio.Obtener(c => c.idFiles == entidad.idFiles);
                files_encontrada.idAsset = entidad.idAsset;
                files_encontrada.idFileTypes = entidad.idFileTypes;
                files_encontrada.filex = entidad.filex;
                files_encontrada.extension = entidad.extension;

                bool respuesta = await _repositorio.Editar(files_encontrada);

                if (!respuesta)
                    throw new TaskCanceledException("No se pudo modificar el Files");

                return files_encontrada;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int idFiles)
        {
            try
            {
                Files files_encontrada = await _repositorio.Obtener(c => c.idFiles == idFiles);

                if (files_encontrada == null)
                    throw new TaskCanceledException("El File no existe");

                bool respuesta = await _repositorio.Eliminar(files_encontrada);

                return respuesta;
            }
            catch
            {
                throw;
            }
        }


    }
}


