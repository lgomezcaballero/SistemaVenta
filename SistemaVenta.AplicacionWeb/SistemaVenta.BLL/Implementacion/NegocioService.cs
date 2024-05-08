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
    public class NegocioService : INegocioService
    {
        private readonly IGenericRepository<Negocio> _repositorio;
        private readonly IFireBaseService _firebaseService;

        public NegocioService(IGenericRepository<Negocio> repositorio, IFireBaseService firebaseService)
        {
            _repositorio = repositorio;
            _firebaseService = firebaseService; 
        }

        public async Task<Negocio> Obtener()
        {
            try
            {
                Negocio negocio_encontrado = await _repositorio.Obtener(n => n.IdNegocio == 1);
                return negocio_encontrado;
            }
            catch {
                throw;
            }
        }
#pragma warning disable CS8625 // No se puede convertir un literal NULL en un tipo de referencia que no acepta valores NULL.
        public async Task<Negocio> GuardarCambios(Negocio entidad, Stream Logo = null, string NombreLogo = "")
#pragma warning restore CS8625 // No se puede convertir un literal NULL en un tipo de referencia que no acepta valores NULL.
        {
            try
            {
                Negocio negocio_encontrado = await _repositorio.Obtener(n => n.IdNegocio == 1);


                negocio_encontrado.NumeroDocumento = entidad.NumeroDocumento;
                negocio_encontrado.Nombre = entidad.Nombre;
                negocio_encontrado.Correo = entidad.Correo;
                negocio_encontrado.Direccion = entidad.Direccion;
                negocio_encontrado.Telefono = entidad.Telefono;
                negocio_encontrado.PorcentajeImpuesto = entidad.PorcentajeImpuesto;
                negocio_encontrado.SimboloMoneda = entidad.SimboloMoneda;

                negocio_encontrado.NombreLogo = negocio_encontrado.NombreLogo == "" ? NombreLogo : negocio_encontrado.NombreLogo;

                if (Logo != null) {
#pragma warning disable CS8604 // Posible argumento de referencia nulo
                    string urlLogo = await _firebaseService.SubirStorage(Logo, "carpeta_logo", negocio_encontrado.NombreLogo);
#pragma warning restore CS8604 // Posible argumento de referencia nulo
                    negocio_encontrado.UrlLogo = urlLogo;
                
                }

                await _repositorio.Editar(negocio_encontrado);
                return negocio_encontrado;
            }
            catch {
                throw;
            }

        }

    }
}
