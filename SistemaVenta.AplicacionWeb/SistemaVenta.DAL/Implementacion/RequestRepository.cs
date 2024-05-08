using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using SistemaVenta.DAL.DBContext;
using SistemaVenta.DAL.Interfaces;
using SistemaVenta.Entity;

namespace SistemaVenta.DAL.Implementacion
{
    public class RequestRepository : GenericRepository<Request>, IRequestRepository
    {

        private readonly DBVENTAContext _dbContext;

        public RequestRepository(DBVENTAContext dbContex) : base(dbContex)
        {
            _dbContext = dbContex;
        }


        public async Task<Request> Registrar(Request entidad)
        {
            Request requestGenerada = new Request();

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {


                    NumeroCorrelativo correlativo = _dbContext.NumeroCorrelativos.Where(n => n.Gestion == "request").First();

                    correlativo.UltimoNumero = correlativo.UltimoNumero + 1;
                    correlativo.FechaActualizacion = DateTime.Now;

                    _dbContext.NumeroCorrelativos.Update(correlativo);
                    await _dbContext.SaveChangesAsync();


#pragma warning disable CS8629 // Un tipo que acepta valores NULL puede ser nulo.
                    string ceros = string.Concat(Enumerable.Repeat("0", correlativo.CantidadDigitos.Value));
#pragma warning restore CS8629 // Un tipo que acepta valores NULL puede ser nulo.
                    string numberRequest = ceros + correlativo.UltimoNumero.ToString();
                    numberRequest = numberRequest.Substring(numberRequest.Length - correlativo.CantidadDigitos.Value, correlativo.CantidadDigitos.Value);

                    entidad.numberRequest = numberRequest;

                    await _dbContext.Request.AddAsync(entidad);
                    await _dbContext.SaveChangesAsync();

                    requestGenerada = entidad;

                    transaction.Commit();

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }


            }

            return requestGenerada;
        }

        //public Task<Request> Registrar(Request entidad)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<List<RequestDetail>> Reporte(DateTime fechaInicio, DateTime fechaFin)
        {


#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning disable CS8629 // Un tipo que acepta valores NULL puede ser nulo.
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            List<RequestDetail> listaResumen = await _dbContext.RequestDetail
                .Include(a => a.IdAssetNavigation)
                .Include(v => v.IdRequestNavigation)
                .ThenInclude(u => u.IdUsuarioNavigation)
                .Include(v => v.IdRequestNavigation)
                .ThenInclude(tdv => tdv.IdAssetRequestTypeNavigation)
                .Where(dv => dv.IdRequestNavigation.registerDate.Value.Date >= fechaInicio.Date &&
                    dv.IdRequestNavigation.registerDate.Value.Date <= fechaFin.Date).ToListAsync();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning restore CS8629 // Un tipo que acepta valores NULL puede ser nulo.
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.



            return listaResumen;
        }

        
    }
}

