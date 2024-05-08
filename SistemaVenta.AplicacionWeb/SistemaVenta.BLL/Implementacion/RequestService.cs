using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using SistemaVenta.BLL.Interfaces;
using SistemaVenta.DAL.Interfaces;
using SistemaVenta.Entity;

namespace SistemaVenta.BLL.Implementacion
{
    public class RequestService : IRequestService
    {
        private readonly IGenericRepository<Asset> _repositorioAsset;
        private readonly IRequestRepository _repositorioRequest;
        

        public RequestService(IGenericRepository<Asset> repositorioAsset,
            IRequestRepository repositorioRequest
            )
        {
            _repositorioAsset = repositorioAsset;
            _repositorioRequest = repositorioRequest;
        }

        public async Task<List<Asset>> ObtenerAsset(string busqueda)
        {
            IQueryable<Asset> query = await _repositorioAsset.Consultar(
                p =>
                
                string.Concat(p.inter, p.make, p.model, p.licencePlate).Contains(busqueda)
                );

            return query
                .Include(c => c.IdAssetTypeNavigation)
                .Include(c => c.IdFuelNavigation)
                .Include(c => c.IdLocationNavigation)
                .Include(c => c.IdUserAssetNavigation)
                .Include(c => c.IdManagerNavigation)

                .ToList();
        }


        public async Task<Request> Registrar(Request entidad)
        {
            try
            {
                return await _repositorioRequest.Registrar(entidad);
            }
            catch
            {
                throw;
            }
        }


        public async Task<List<Request>> Historial(string numberRequest, string fechaInicio, string fechaFin)
        {
            IQueryable<Request> query = await _repositorioRequest.Consultar();
            fechaInicio = fechaInicio is null ? "" : fechaInicio;
            fechaFin = fechaFin is null ? "" : fechaFin;


            if (fechaInicio != "" && fechaFin != "")
            {

                DateTime fech_inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo(""));
                DateTime fech_fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo(""));

#pragma warning disable CS8629 // Un tipo que acepta valores NULL puede ser nulo.
                return query.Where(v =>
                    v.registerDate.Value.Date >= fech_inicio.Date &&
                    v.registerDate.Value.Date <= fech_fin.Date
                )
                    .Include(tdv => tdv.IdAssetRequestTypeNavigation)
                    .Include(u => u.IdUsuarioNavigation)
                    .Include(p => p.IdPriorityNavigation)
                    .Include(dv => dv.RequestDetail)
                    .ToList();
#pragma warning restore CS8629 // Un tipo que acepta valores NULL puede ser nulo.
            }
            else
            {
                return query.Where(v => v.numberRequest == numberRequest
                   )
                       .Include(tdv => tdv.IdAssetRequestTypeNavigation)
                       .Include(u => u.IdUsuarioNavigation)
                       .Include(p => p.IdPriorityNavigation)
                       .Include(dv => dv.RequestDetail)
                       .ToList();
            }

        }

        public async Task<Request> Detalle(string numberRequest)
        {
            IQueryable<Request> query = await _repositorioRequest.Consultar(v => v.numberRequest == numberRequest);

            return query
                        .Include(tdv => tdv.IdAssetRequestTypeNavigation)
                        .Include(u => u.IdUsuarioNavigation)
                        .Include(dv => dv.RequestDetail)
                        .First();
        }


        public async Task<List<RequestDetail>> Reporte(string fechaInicio, string fechaFin)
        {
            DateTime fech_inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-AR"));
            DateTime fech_fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-AR"));

            List<RequestDetail> lista = await _repositorioRequest.Reporte(fech_inicio, fech_fin);

            return lista;
        }

       
    }
}

