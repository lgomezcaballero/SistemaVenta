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
    public class PriorityService : IPriorityService
    {
        private readonly IGenericRepository<Priority> _repositorio;
#pragma warning disable CS0169 
        private int idPriority;
#pragma warning restore CS0169 

        public PriorityService(IGenericRepository<Priority> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<Priority>> Lista()
        {
            IQueryable<Priority> query = await _repositorio.Consultar();
            return query.ToList();
        }
        public async Task<Priority> Crear(Priority entidad)
        {
            try
            {
                Priority priority_creada = await _repositorio.Crear(entidad);
                if (priority_creada.idPriority == 0)
                    throw new TaskCanceledException("No se pudo crear el Priority");

                return priority_creada;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Priority> Editar(Priority entidad)
        {
            try
            {
                Priority priority_encontrada = await _repositorio.Obtener(c => c.idPriority == entidad.idPriority);
                priority_encontrada.description = entidad.description;
                priority_encontrada.active = entidad.active;

                bool respuesta = await _repositorio.Editar(priority_encontrada);

                if (!respuesta)
                    throw new TaskCanceledException("No se pudo modificar el Priority");

                return priority_encontrada;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int idPriority)
        {
            try
            {
                Priority priority_encontrada = await _repositorio.Obtener(c => c.idPriority == idPriority);

                if (priority_encontrada == null)
                    throw new TaskCanceledException("El Priority no existe");

                bool respuesta = await _repositorio.Eliminar(priority_encontrada);

                return respuesta;
            }
            catch
            {
                throw;
            }
        }


    }
}
