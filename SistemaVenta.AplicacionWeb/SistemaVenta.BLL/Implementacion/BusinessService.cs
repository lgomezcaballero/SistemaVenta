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
    public class BusinessService : IBusinessService
    {
        private readonly IGenericRepository<Business> _repositorio;
#pragma warning disable CS0169 // El campo 'BusinessService.idBusiness' nunca se usa
        private int idBusiness;
#pragma warning restore CS0169 // El campo 'BusinessService.idBusiness' nunca se usa

        public BusinessService(IGenericRepository<Business> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<Business>> Lista()
        {
            IQueryable<Business> query = await _repositorio.Consultar();
            return query.ToList();
        }
        public async Task<Business> Crear(Business entidad)
        {
            try
            {
                Business business_creada = await _repositorio.Crear(entidad);
                if (business_creada.idBusiness == 0)
                    throw new TaskCanceledException("No se pudo crear el Business");

                return business_creada;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Business> Editar(Business entidad)
        {
            try
            {
                Business business_encontrada = await _repositorio.Obtener(c => c.idBusiness == entidad.idBusiness);
                business_encontrada.description = entidad.description;
                business_encontrada.ein = entidad.ein;
                business_encontrada.type = entidad.type;
                business_encontrada.country = entidad.country;
                business_encontrada.state = entidad.state;
                business_encontrada.address = entidad.address;
                business_encontrada.active = entidad.active;

                bool respuesta = await _repositorio.Editar(business_encontrada);

                if (!respuesta)
                    throw new TaskCanceledException("No se pudo modificar el Fuel");

                return business_encontrada;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int idBusiness)
        {
            try
            {
                Business business_encontrada = await _repositorio.Obtener(c => c.idBusiness == idBusiness);

                if (business_encontrada == null)
                    throw new TaskCanceledException("El Business no existe");

                bool respuesta = await _repositorio.Eliminar(business_encontrada);

                return respuesta;
            }
            catch
            {
                throw;
            }
        }


    }
}

