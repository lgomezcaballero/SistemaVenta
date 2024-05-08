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
    public class StatusService : IStatusService
    {
        private readonly IGenericRepository<Status> _repositorio;
#pragma warning disable CS0169 // El campo 'StatusService.idStatus' nunca se usa
        private int idStatus;
#pragma warning restore CS0169 // El campo 'StatusService.idStatus' nunca se usa

        public StatusService(IGenericRepository<Status> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<Status>> Lista()
        {
            IQueryable<Status> query = await _repositorio.Consultar();
            return query.ToList();
        }
        public async Task<Status> Crear(Status entidad)
        {
            try
            {
                Status status_creada = await _repositorio.Crear(entidad);
                if (status_creada.idStatus == 0)
                    throw new TaskCanceledException("No se pudo crear el Status");

                return status_creada;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Status> Editar(Status entidad)
        {
            try
            {
                Status status_encontrada = await _repositorio.Obtener(c => c.idStatus == entidad.idStatus);
                status_encontrada.description = entidad.description;
                status_encontrada.active = entidad.active;

                bool respuesta = await _repositorio.Editar(status_encontrada);

                if (!respuesta)
                    throw new TaskCanceledException("No se pudo modificar el Status");

                return status_encontrada;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int idStatus)
        {
            try
            {
                Status status_encontrada = await _repositorio.Obtener(c => c.idStatus == idStatus);

                if (status_encontrada == null)
                    throw new TaskCanceledException("This Status not exist");

                bool respuesta = await _repositorio.Eliminar(status_encontrada);

                return respuesta;
            }
            catch
            {
                throw;
            }
        }


    }
}

