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
    public class FuelService : IFuelService
    {
        private readonly IGenericRepository<Fuel> _repositorio;
#pragma warning disable CS0169 // El campo 'FuelService.idFuel' nunca se usa
        private int idFuel;
#pragma warning restore CS0169 // El campo 'FuelService.idFuel' nunca se usa

        public FuelService(IGenericRepository<Fuel> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<Fuel>> Lista()
        {
            IQueryable<Fuel> query = await _repositorio.Consultar();
            return query.ToList();
        }
        public async Task<Fuel> Crear(Fuel entidad)
        {
            try
            {
                Fuel fuel_creada = await _repositorio.Crear(entidad);
                if (fuel_creada.idFuel == 0)
                    throw new TaskCanceledException("No se pudo crear el Fuel");

                return fuel_creada;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Fuel> Editar(Fuel entidad)
        {
            try
            {
                Fuel fuel_encontrada = await _repositorio.Obtener(c => c.idFuel == entidad.idFuel);
                fuel_encontrada.description = entidad.description;
                fuel_encontrada.active = entidad.active;

                bool respuesta = await _repositorio.Editar(fuel_encontrada);

                if (!respuesta)
                    throw new TaskCanceledException("No se pudo modificar el Fuel");

                return fuel_encontrada;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int idFuel)
        {
            try
            {
                Fuel fuel_encontrada = await _repositorio.Obtener(c => c.idFuel == idFuel);

                if (fuel_encontrada == null)
                    throw new TaskCanceledException("El Fuel no existe");

                bool respuesta = await _repositorio.Eliminar(fuel_encontrada);

                return respuesta;
            }
            catch
            {
                throw;
            }
        }


    }
}
