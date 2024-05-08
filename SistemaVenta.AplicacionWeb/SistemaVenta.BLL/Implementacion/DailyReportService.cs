using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaVenta.BLL.Interfaces;
using SistemaVenta.DAL.Interfaces;
using SistemaVenta.Entity;

namespace SistemaVenta.BLL.Implementacion
{
    public class DailyReportService : IDailyReportService
    {
        private readonly IGenericRepository<DailyReport> _repositorio;
        private readonly IGenericRepository<Asset> _repositorioAsset;

#pragma warning disable CS0169 // El campo 'DailyReportService.idDailyReport' nunca se usa
        private int idDailyReport;
#pragma warning restore CS0169 // El campo 'DailyReportService.idDailyReport' nunca se usa

        public DailyReportService(IGenericRepository<DailyReport> repositorio, IGenericRepository<Asset> repositorioAsset)
        {
            _repositorio = repositorio;
            _repositorioAsset = repositorioAsset;
        }

        public async Task<List<DailyReport>> Lista()
        {
            IQueryable<DailyReport> query = await _repositorio.Consultar();
            return query
                .Include(c => c.IdAssetNavigation)

                .ToList();
        }
        public async Task<DailyReport> Crear(DailyReport entidad)
        {
            try
            {
                DailyReport dailyReport_creado = await _repositorio.Crear(entidad);
                if (dailyReport_creado.idDailyReport == 0)
                    throw new TaskCanceledException("No se pudo crear el DailyReport");

                IQueryable<DailyReport> query = await _repositorio.Consultar(p => p.idDailyReport == dailyReport_creado.idDailyReport);

                dailyReport_creado = query
                    .Include(c => c.IdAssetNavigation)
                   
                    .First();

                return dailyReport_creado;
            }
            catch
            {
                throw;
            }
        }

        public async Task<DailyReport> Editar(DailyReport entidad)
        {
            try
            {
                IQueryable<DailyReport> queryDailyReport = await _repositorio.Consultar(p => p.idDailyReport == entidad.idDailyReport);

                DailyReport dailyReport_encontrada = queryDailyReport.First();

                dailyReport_encontrada.idAsset = entidad.idAsset;
                dailyReport_encontrada.numberReport = entidad.numberReport;
                dailyReport_encontrada.final = entidad.final;
                dailyReport_encontrada.closingOfDay = entidad.closingOfDay;
                dailyReport_encontrada.registerDate = entidad.registerDate;

                bool respuesta = await _repositorio.Editar(dailyReport_encontrada);

                if (!respuesta)
                    throw new TaskCanceledException("No se pudo modificar el Daily Report");

                DailyReport dailyReport_editado = queryDailyReport
                    .Include(c => c.IdAssetNavigation)
                    .First();

                return dailyReport_editado;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int idDailyReport)
        {
            try
            {
                DailyReport dailyReport_encontrada = await _repositorio.Obtener(c => c.idDailyReport == idDailyReport);

                if (dailyReport_encontrada == null)
                    throw new TaskCanceledException("El Daily Report no existe");

                bool respuesta = await _repositorio.Eliminar(dailyReport_encontrada);

                return respuesta;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Asset>> ObtenerAsset(string busqueda)
        {
            IQueryable<Asset> query = await _repositorioAsset.Consultar(
                p =>
                //p.EsActivo == true &&
                //p.Stock > 0 &&
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
    }
}
