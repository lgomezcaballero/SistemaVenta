using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaVenta.DAL.DBContext;
using Microsoft.EntityFrameworkCore;
using SistemaVenta.DAL.Interfaces;
using SistemaVenta.DAL.Implementacion;
using SistemaVenta.BLL.Interfaces;
using SistemaVenta.BLL.Implementacion;

namespace SistemaVenta.IOC
{
    public static class Dependencia
    {

        public static void InyectarDependencia(this IServiceCollection services, IConfiguration Configuration) {

            services.AddDbContext<DBVENTAContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("CadenaSQL"));
            });

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IRequestRepository, RequestRepository>();

            services.AddScoped<ICorreoService, CorreoService>();
            services.AddScoped<IFireBaseService, FireBaseService>();

            services.AddScoped<IUtilidadesService, UtilidadesService>();
            services.AddScoped<IRolService, RolService>();

            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<INegocioService, NegocioService>();
            
            services.AddScoped<IAssetService, AssetService>();
            services.AddScoped<IAssetTypeService, AssetTypeService>();
            services.AddScoped<IBusinessService, BusinessService>();
            services.AddScoped<IFuelService, FuelService>();
            services.AddScoped<IInsuranceService, InsuranceService>();
            services.AddScoped<IInsurancePTService, InsurancePTService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IManagerService, ManagerService>();
            services.AddScoped<ISectionalRegistrationService, SectionalRegistrationService>();
            services.AddScoped<IStatusService, StatusService>();
            services.AddScoped<IUserAssetService, UserAssetService>();

            services.AddScoped<IPriorityService, PriorityService>();
            services.AddScoped<IFileTypesService, FileTypesService>();
            services.AddScoped<IFilesService, FilesService>();

            services.AddScoped<IAssetRequestTypeService, AssetRequestTypeService>();
            
            services.AddScoped<IRequestService, RequestService>();

            services.AddScoped<IDashBoardService, DashBoardService>();
            services.AddScoped<IMenuService, MenuService>();

        }
    }
}
