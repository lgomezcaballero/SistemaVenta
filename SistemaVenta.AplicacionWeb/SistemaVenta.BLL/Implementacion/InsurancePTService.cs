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
    public class InsurancePTService : IInsurancePTService
    {
        private readonly IGenericRepository<InsurancePT> _repositorio;
#pragma warning disable CS0169 // El campo 'InsurancePTService.idInsurancePT' nunca se usa
        private int idInsurancePT;
#pragma warning restore CS0169 // El campo 'InsurancePTService.idInsurancePT' nunca se usa

        public InsurancePTService(IGenericRepository<InsurancePT> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<InsurancePT>> Lista()
        {
            IQueryable<InsurancePT> query = await _repositorio.Consultar();
            return query.ToList();
        }
        public async Task<InsurancePT> Crear(InsurancePT entidad)
        {
            try
            {
                InsurancePT insurancePT_creada = await _repositorio.Crear(entidad);
                if (insurancePT_creada.idInsurancePT == 0)
                    throw new TaskCanceledException("No se pudo crear el Insurance Policy Type");

                return insurancePT_creada;
            }
            catch
            {
                throw;
            }
        }

        public async Task<InsurancePT> Editar(InsurancePT entidad)
        {
            try
            {
                InsurancePT insurancePT_encontrada = await _repositorio.Obtener(c => c.idInsurancePT == entidad.idInsurancePT);
                insurancePT_encontrada.description = entidad.description;
                insurancePT_encontrada.active = entidad.active;

                bool respuesta = await _repositorio.Editar(insurancePT_encontrada);

                if (!respuesta)
                    throw new TaskCanceledException("No se pudo modificar el Insurance Policy Type");

                return insurancePT_encontrada;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int idInsurancePT)
        {
            try
            {
                InsurancePT insurancePT_encontrada = await _repositorio.Obtener(c => c.idInsurancePT == idInsurancePT);

                if (insurancePT_encontrada == null)
                    throw new TaskCanceledException("El Insurance Policy Type no existe");

                bool respuesta = await _repositorio.Eliminar(insurancePT_encontrada);

                return respuesta;
            }
            catch
            {
                throw;
            }
        }


    }
}

