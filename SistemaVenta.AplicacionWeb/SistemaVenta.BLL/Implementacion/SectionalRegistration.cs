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
    public class SectionalRegistrationService : ISectionalRegistrationService
    {
        private readonly IGenericRepository<SectionalRegistration> _repositorio;
#pragma warning disable CS0169 // El campo 'SectionalRegistrationService.idSectionalRegistration' nunca se usa
        private int idSectionalRegistration;
#pragma warning restore CS0169 // El campo 'SectionalRegistrationService.idSectionalRegistration' nunca se usa

        public SectionalRegistrationService(IGenericRepository<SectionalRegistration> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<SectionalRegistration>> Lista()
        {
            IQueryable<SectionalRegistration> query = await _repositorio.Consultar();
            return query.ToList();
        }
        public async Task<SectionalRegistration> Crear(SectionalRegistration entidad)
        {
            try
            {
                SectionalRegistration sectionalRegistration_creada = await _repositorio.Crear(entidad);
                if (sectionalRegistration_creada.idSectionalRegistration == 0)
                    throw new TaskCanceledException("No se pudo crear el Sectional Registration");

                return sectionalRegistration_creada;
            }
            catch
            {
                throw;
            }
        }

        public async Task<SectionalRegistration> Editar(SectionalRegistration entidad)
        {
            try
            {
                SectionalRegistration sectionalRegistration_encontrada = await _repositorio.Obtener(c => c.idSectionalRegistration == entidad.idSectionalRegistration);
                sectionalRegistration_encontrada.description = entidad.description;
                sectionalRegistration_encontrada.state = entidad.state;
                sectionalRegistration_encontrada.location = entidad.location;
                sectionalRegistration_encontrada.address = entidad.address;
                sectionalRegistration_encontrada.active = entidad.active;

                bool respuesta = await _repositorio.Editar(sectionalRegistration_encontrada);

                if (!respuesta)
                    throw new TaskCanceledException("No se pudo modificar el Fuel");

                return sectionalRegistration_encontrada;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int idSectionalRegistration)
        {
            try
            {
                SectionalRegistration sectionalRegistration_encontrada = await _repositorio.Obtener(c => c.idSectionalRegistration == idSectionalRegistration);

                if (sectionalRegistration_encontrada == null)
                    throw new TaskCanceledException("El Sectional no existe");

                bool respuesta = await _repositorio.Eliminar(sectionalRegistration_encontrada);

                return respuesta;
            }
            catch
            {
                throw;
            }
        }


    }
}

