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
    public class InsuranceService : IInsuranceService
    {
        private readonly IGenericRepository<Insurance> _repositorio;
#pragma warning disable CS0169 // El campo 'InsuranceService.idInsurance' nunca se usa
        private int idInsurance;
#pragma warning restore CS0169 // El campo 'InsuranceService.idInsurance' nunca se usa

        public InsuranceService(IGenericRepository<Insurance> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<Insurance>> Lista()
        {
            IQueryable<Insurance> query = await _repositorio.Consultar();
            return query.ToList();
        }
        public async Task<Insurance> Crear(Insurance entidad)
        {
            try
            {
                Insurance insurance_creada = await _repositorio.Crear(entidad);
                if (insurance_creada.idInsurance == 0)
                    throw new TaskCanceledException("No se pudo crear el Insurance");

                return insurance_creada;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Insurance> Editar(Insurance entidad)
        {
            try
            {
                Insurance insurance_encontrada = await _repositorio.Obtener(c => c.idInsurance == entidad.idInsurance);
                insurance_encontrada.description = entidad.description;
                insurance_encontrada.active = entidad.active;

                bool respuesta = await _repositorio.Editar(insurance_encontrada);

                if (!respuesta)
                    throw new TaskCanceledException("No se pudo modificar el Insurance");

                return insurance_encontrada;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int idInsurance)
        {
            try
            {
                Insurance insurance_encontrada = await _repositorio.Obtener(c => c.idInsurance == idInsurance);

                if (insurance_encontrada == null)
                    throw new TaskCanceledException("El Insurance no existe");

                bool respuesta = await _repositorio.Eliminar(insurance_encontrada);

                return respuesta;
            }
            catch
            {
                throw;
            }
        }


    }
}

