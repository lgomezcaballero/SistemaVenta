using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using SistemaVenta.BLL.Interfaces;
using SistemaVenta.DAL.Interfaces;
using SistemaVenta.Entity;
using System.Globalization;

namespace SistemaVenta.BLL.Implementacion
{
    public class DashBoardService : IDashBoardService
    {
        private readonly IRequestRepository _repositorioRequest;
        private readonly IGenericRepository<RequestDetail> _repositorioRequestDetail;
        private readonly IGenericRepository<Asset> _repositorioAsset;
        private readonly IGenericRepository<Location> _repositorioLocation;
        private readonly IGenericRepository<Manager> _repositorioManager;
        private readonly IGenericRepository<UserAsset> _repositorioUserAsset;       
        private readonly IGenericRepository<Business> _repositorioBusiness;
        

        private DateTime FechaInicio = DateTime.Now;


        public DashBoardService(
            IRequestRepository repositorioRequest,
            IGenericRepository<RequestDetail> repositorioRequestDetail,
            IGenericRepository<Asset> repositorioAsset,
            IGenericRepository<Location> repositorioLocation,
            IGenericRepository<Manager> repositorioManager,
            IGenericRepository<UserAsset> repositorioUserAsset,          
            IGenericRepository<Business> repositorioBusiness
           
            )
        {
            _repositorioRequest = repositorioRequest;
            _repositorioRequestDetail = repositorioRequestDetail;
            _repositorioAsset = repositorioAsset;
            _repositorioLocation = repositorioLocation;
            _repositorioManager = repositorioManager;
            _repositorioUserAsset = repositorioUserAsset;         
            _repositorioBusiness = repositorioBusiness;
            


            FechaInicio = FechaInicio.AddDays(-7);
        }

        public async Task<int> TotalRequestUltimaSemana()
        {
            try
            {

#pragma warning disable CS8629 // Un tipo que acepta valores NULL puede ser nulo.
                IQueryable<Request> query = await _repositorioRequest.Consultar(v => v.registerDate.Value.Date >= FechaInicio.Date);
#pragma warning restore CS8629 // Un tipo que acepta valores NULL puede ser nulo.

                int total = query.Count();
                return total;
            }
            catch {
                throw;
            }
        }


        public async Task<int> TotalAssets()
        {
            try
            {
                IQueryable<Asset> query = await _repositorioAsset.Consultar();
                int total = query.Count();
                return total;
            }
            catch
            {
                throw;
            }
        }


        public async Task<int> TotalLocations()
        {
            try
            {
                IQueryable<Location> query = await _repositorioLocation.Consultar();
                int total = query.Count();
                return total;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> TotalManagers()
        {
            try
            {
                IQueryable<Manager> query = await _repositorioManager.Consultar();
                int total = query.Count();
                return total;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> TotalUserAssets()
        {
            try
            {
                IQueryable<UserAsset> query = await _repositorioUserAsset.Consultar();
                int total = query.Count();
                return total;
            }
            catch
            {
                throw;
            }
        }

       

        public async Task<int> TotalBusiness()
        {
            try
            {
                IQueryable<Business> query = await _repositorioBusiness.Consultar();
                int total = query.Count();
                return total;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> TotalRequest()
        {
            try
            {
                IQueryable<Request> query = await _repositorioRequest.Consultar();
                int total = query.Count();
                return total;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Dictionary<string, int>> AssetVsStatus()
        {
            try
            {
                IQueryable<Asset> query = await _repositorioAsset.Consultar();

#pragma warning disable CS8619 // La nulabilidad de los tipos de referencia del valor no coincide con el tipo de destino
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning disable CS8714 // El tipo no se puede usar como parámetro de tipo en el método o tipo genérico. La nulabilidad del argumento de tipo no coincide con la restricción "notnull"
#pragma warning disable CS8621 // La nulabilidad de los tipos de referencia del tipo de valor devuelto no coincide con el delegado de destino (posiblemente debido a los atributos de nulabilidad).
                Dictionary<string, int> resultado = query
                    .GroupBy(a => a.IdStatusNavigation.description)
                    .OrderByDescending(g => g.Count())
                    .Select(g => new { status = g.Key, total = g.Count() })
                    .ToDictionary(r => r.status, r => r.total);
#pragma warning restore CS8621 // La nulabilidad de los tipos de referencia del tipo de valor devuelto no coincide con el delegado de destino (posiblemente debido a los atributos de nulabilidad).
#pragma warning restore CS8714 // El tipo no se puede usar como parámetro de tipo en el método o tipo genérico. La nulabilidad del argumento de tipo no coincide con la restricción "notnull"
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning restore CS8619 // La nulabilidad de los tipos de referencia del valor no coincide con el tipo de destino

                return resultado;
            }
            catch
            {
                throw;
            }
        }



        public async Task<Dictionary<string, int>> RequestUltimaSemana()
        {
            try
            {

#pragma warning disable CS8629 // Un tipo que acepta valores NULL puede ser nulo.
                IQueryable<Request> query = await _repositorioRequest
                    .Consultar(v => v.registerDate.Value.Date >= FechaInicio.Date);
#pragma warning restore CS8629 // Un tipo que acepta valores NULL puede ser nulo.


#pragma warning disable CS8629 // Un tipo que acepta valores NULL puede ser nulo.
                Dictionary<string, int> resultado = query
                    .GroupBy(v => v.registerDate.Value.Date).OrderBy(g => g.Key)
                    .Select(dv => new { fecha = dv.Key.ToString("dd/MM/yyyy"), total = dv.Count() })
                    .ToDictionary(keySelector: r => r.fecha, elementSelector: r => r.total);
#pragma warning restore CS8629 // Un tipo que acepta valores NULL puede ser nulo.

                return resultado;
            }
            catch
            {
                throw;
            }
        }

        public  async Task<Dictionary<string, int>> AssetTopUltimaSemana()
        {
            try
            {

                IQueryable<RequestDetail> query = await _repositorioRequestDetail.Consultar();

#pragma warning disable CS8621 // La nulabilidad de los tipos de referencia del tipo de valor devuelto no coincide con el delegado de destino (posiblemente debido a los atributos de nulabilidad).
#pragma warning disable CS8619 // La nulabilidad de los tipos de referencia del valor no coincide con el tipo de destino
#pragma warning disable CS8714 // El tipo no se puede usar como parámetro de tipo en el método o tipo genérico. La nulabilidad del argumento de tipo no coincide con la restricción "notnull"
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning disable CS8629 // Un tipo que acepta valores NULL puede ser nulo.
                Dictionary<string, int> resultado = query
                    .Include(v => v.IdRequestNavigation)
                    .Where(dv => dv.IdRequestNavigation.registerDate.Value.Date >= FechaInicio.Date)
                    .GroupBy(dv => dv.modelAsset).OrderByDescending(g => g.Count())
                    .Select(dv => new { asset = dv.Key, total = dv.Count() }).Take(4)
                    .ToDictionary(keySelector: r => r.asset, elementSelector: r => r.total);
#pragma warning restore CS8629 // Un tipo que acepta valores NULL puede ser nulo.
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning restore CS8714 // El tipo no se puede usar como parámetro de tipo en el método o tipo genérico. La nulabilidad del argumento de tipo no coincide con la restricción "notnull"
#pragma warning restore CS8619 // La nulabilidad de los tipos de referencia del valor no coincide con el tipo de destino
#pragma warning restore CS8621 // La nulabilidad de los tipos de referencia del tipo de valor devuelto no coincide con el delegado de destino (posiblemente debido a los atributos de nulabilidad).

                return resultado;
            }
            catch
            {
                throw;
            }
        }


        




    }
}
