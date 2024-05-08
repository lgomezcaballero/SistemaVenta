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
    public class LocationService : ILocationService
    {
        private readonly IGenericRepository<Location> _repositorio;
#pragma warning disable CS0169 // El campo 'LocationService.idLocation' nunca se usa
        private int idLocation;
#pragma warning restore CS0169 // El campo 'LocationService.idLocation' nunca se usa

        public LocationService(IGenericRepository<Location> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<Location>> Lista()
        {
            IQueryable<Location> query = await _repositorio.Consultar();
            return query.ToList();
        }
        public async Task<Location> Crear(Location entidad)
        {
            try
            {
                Location location_creada = await _repositorio.Crear(entidad);
                if (location_creada.idLocation == 0)
                    throw new TaskCanceledException("No se pudo crear Location");

                return location_creada;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Location> Editar(Location entidad)
        {
            try
            {
                Location location_encontrada = await _repositorio.Obtener(c => c.idLocation == entidad.idLocation);
                location_encontrada.description = entidad.description;
                location_encontrada.name = entidad.name;
                location_encontrada.active = entidad.active;

                bool respuesta = await _repositorio.Editar(location_encontrada);

                if (!respuesta)
                    throw new TaskCanceledException("No se pudo modificar Location");

                return location_encontrada;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int idLocation)
        {
            try
            {
                Location location_encontrada = await _repositorio.Obtener(c => c.idLocation == idLocation);

                if (location_encontrada == null)
                    throw new TaskCanceledException("La Location no existe");

                bool respuesta = await _repositorio.Eliminar(location_encontrada);

                return respuesta;
            }
            catch
            {
                throw;
            }
        }


    }
}
