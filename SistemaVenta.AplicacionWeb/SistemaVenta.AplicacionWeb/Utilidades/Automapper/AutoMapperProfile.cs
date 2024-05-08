
using SistemaVenta.AplicacionWeb.Models.ViewModels;
using SistemaVenta.Entity;
using System.Globalization;
using AutoMapper;

namespace SistemaVenta.AplicacionWeb.Utilidades.Automapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Rol
            CreateMap<Rol, VMRol>().ReverseMap();
            #endregion Rol

            #region Usuario
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            CreateMap<Usuario, VMUsuario>()
                .ForMember(destino =>
                    destino.EsActivo,
                    opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
                )
                .ForMember(destino =>
                    destino.NombreRol,
                    opt => opt.MapFrom(origen => origen.IdRolNavigation.Descripcion)
                );
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            CreateMap<VMUsuario, Usuario>()
                .ForMember(destino =>
                    destino.EsActivo,
                    opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)
                )
                .ForMember(destino =>
                    destino.IdRolNavigation,
                    opt => opt.Ignore()
                    );

            #endregion

            #region Negocio
#pragma warning disable CS8629 // Un tipo que acepta valores NULL puede ser nulo.
            CreateMap<Negocio, VMNegocio>()
                .ForMember(destino =>
                    destino.PorcentajeImpuesto,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.PorcentajeImpuesto.Value, new CultureInfo("es-PE")))
                );
#pragma warning restore CS8629 // Un tipo que acepta valores NULL puede ser nulo.

            CreateMap<VMNegocio, Negocio>()
               .ForMember(destino =>
                   destino.PorcentajeImpuesto,
                   opt => opt.MapFrom(origen => Convert.ToDecimal(origen.PorcentajeImpuesto, new CultureInfo("es-PE")))
               );


            #endregion

            #region AssetRequestType
            CreateMap<AssetRequestType, VMAssetRequestType>().ReverseMap();
            #endregion
            
            #region Request
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning disable CS8629 // Un tipo que acepta valores NULL puede ser nulo.
#pragma warning disable CS8629 // Un tipo que acepta valores NULL puede ser nulo.
#pragma warning disable CS8629 // Un tipo que acepta valores NULL puede ser nulo.
#pragma warning disable CS8629 // Un tipo que acepta valores NULL puede ser nulo.
            CreateMap<Request, VMRequest>()
                 .ForMember(destino =>
                    destino.AssetRequestType,
                    opt => opt.MapFrom(origen => origen.IdAssetRequestTypeNavigation.description)
                )
                 .ForMember(destino =>
                    destino.Usuario,
                    opt => opt.MapFrom(origen => origen.IdUsuarioNavigation.Nombre)
                )
                 .ForMember(destino =>
                    destino.Priority,
                    opt => opt.MapFrom(origen => origen.IdPriorityNavigation.description)
                )


                 .ForMember(destino =>
                    destino.registerDate,
                    opt => opt.MapFrom(origen => origen.registerDate.Value.ToString("dd/MM/yyyy"))
                );


            CreateMap<VMRequest, Request>();
            //.ForMember(destino =>
            //     destino.SubTotal,
            //     opt => opt.MapFrom(origen => Convert.ToDecimal(origen.SubTotal, new CultureInfo("es-PE")))
            // )
            // .ForMember(destino =>
            //     destino.ImpuestoTotal,
            //     opt => opt.MapFrom(origen => Convert.ToDecimal(origen.ImpuestoTotal, new CultureInfo("es-PE")))
            // )
            // .ForMember(destino =>
            //    destino.Total,
            //    opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Total, new CultureInfo("es-PE")))
            //);
            #endregion

            #region RequestDetail
#pragma warning disable CS8629 // Un tipo que acepta valores NULL puede ser nulo.
#pragma warning disable CS8629 // Un tipo que acepta valores NULL puede ser nulo.
            CreateMap<RequestDetail, VMRequestDetail>()
                ;
#pragma warning restore CS8629 // Un tipo que acepta valores NULL puede ser nulo.
#pragma warning restore CS8629 // Un tipo que acepta valores NULL puede ser nulo.

            CreateMap<VMRequestDetail, RequestDetail>()
                ;


#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning disable CS8629 // Un tipo que acepta valores NULL puede ser nulo.
            CreateMap<RequestDetail, VMRequestReport>()
            .ForMember(destino =>
                  destino.registerDate,
                  opt => opt.MapFrom(origen => origen.IdRequestNavigation.registerDate.Value.ToString("dd/MM/yyyy"))
              )
            .ForMember(destino =>
                  destino.numberRequest,
                  opt => opt.MapFrom(origen => origen.IdRequestNavigation.numberRequest)
              )
            .ForMember(destino =>
                  destino.requestType,
                  opt => opt.MapFrom(origen => origen.IdRequestNavigation.IdAssetRequestTypeNavigation.description)
              )
             .ForMember(destino =>
                  destino.description,
                  opt => opt.MapFrom(origen => origen.IdRequestNavigation.description)
              )
             .ForMember(destino =>
                  destino.odometer,
                  opt => opt.MapFrom(origen => origen.IdRequestNavigation.odometer)
              )
             .ForMember(destino =>
                  destino.asset,
                  opt => opt.MapFrom(origen => origen.modelAsset)
              )
             .ForMember(destino =>
                  destino.inter,
                  opt => opt.MapFrom(origen => origen.assetInternal)
              )
             .ForMember(destino =>
                  destino.assetType,
                  opt => opt.MapFrom(origen => origen.assetType)
              )
             .ForMember(destino =>
                  destino.modelAsset,
                  opt => opt.MapFrom(origen => origen.modelAsset)
              )


              ;
#pragma warning restore CS8629 // Un tipo que acepta valores NULL puede ser nulo.
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

#pragma warning restore CS8629 // Un tipo que acepta valores NULL puede ser nulo.
#pragma warning restore CS8629 // Un tipo que acepta valores NULL puede ser nulo.
#pragma warning restore CS8629 // Un tipo que acepta valores NULL puede ser nulo.
#pragma warning restore CS8629 // Un tipo que acepta valores NULL puede ser nulo.
#pragma warning restore CS8629 // Un tipo que acepta valores NULL puede ser nulo.
#pragma warning restore CS8629 // Un tipo que acepta valores NULL puede ser nulo.
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            #endregion

            #region Menu
            CreateMap<Menu, VMMenu>()
                 .ForMember(destino =>
                  destino.SubMenus,
                  opt => opt.MapFrom(origen => origen.InverseIdMenuPadreNavigation)
              );
            #endregion

            #region Files
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            CreateMap<Files, VMFiles>()
           //.ForMember(destino =>
           //    destino.active,
           //    opt => opt.MapFrom(origen => origen.active == true ? 1 : 0)
           //)
           .ForMember(destino =>
                destino.nombreAsset,
                opt => opt.MapFrom(origen => origen.IdAssetNavigation.inter)
            )
           .ForMember(destino =>
                destino.nombreFileTypes,
                opt => opt.MapFrom(origen => origen.IdFileTypesNavigation.description)
            )
            ;
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            CreateMap<VMFiles, Files>()
            //.ForMember(destino =>
            //    destino.active,
            //    opt => opt.MapFrom(origen => origen.active == 1 ? true : false)
            //)
            .ForMember(destino =>
                destino.IdAssetNavigation,
                opt => opt.Ignore()
            )
            .ForMember(destino =>
                destino.IdFileTypesNavigation,
                opt => opt.Ignore()
            )
            ;
            #endregion

            #region FileTypes
            CreateMap<FileTypes, VMFileTypes>()
           .ForMember(destino =>
               destino.active,
               opt => opt.MapFrom(origen => origen.active == true ? 1 : 0)
           );

            CreateMap<VMFileTypes, FileTypes>()
            .ForMember(destino =>
                destino.active,
                opt => opt.MapFrom(origen => origen.active == 1 ? true : false)
            );
            #endregion

            #region Fuel
            CreateMap<Fuel, VMFuel>()
           .ForMember(destino =>
               destino.active,
               opt => opt.MapFrom(origen => origen.active == true ? 1 : 0)
           );

            CreateMap<VMFuel, Fuel>()
            .ForMember(destino =>
                destino.active,
                opt => opt.MapFrom(origen => origen.active == 1 ? true : false)
            );
            #endregion

            #region Asset


#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            CreateMap<Asset, VMAsset>()
            //.ForMember(destino =>
            //    destino.Active,
            //    opt => opt.MapFrom(origen => origen.Active == true ? 1 : 0)
            //);
            .ForMember(destino =>
                destino.nombreAssetType,
                opt => opt.MapFrom(origen => origen.IdAssetTypeNavigation.description)
            )
            .ForMember(destino =>
                destino.nombreFuel,
                opt => opt.MapFrom(origen => origen.IdFuelNavigation.description)
            )
            .ForMember(destino =>
                destino.nombreInsurance,
                opt => opt.MapFrom(origen => origen.IdInsuranceNavigation.description)
            )
            .ForMember(destino =>
                destino.nombreInsurancePT,
                opt => opt.MapFrom(origen => origen.IdInsurancePTNavigation.description)
            )
            .ForMember(destino =>
                destino.nombreSectionalRegistration,
                opt => opt.MapFrom(origen => origen.IdSectionalRegistrationNavigation.description)
            )
            .ForMember(destino =>
                destino.nombreLocation,
                opt => opt.MapFrom(origen => origen.IdLocationNavigation.description)
            )
            .ForMember(destino =>
                destino.nombreUserAsset,
                opt => opt.MapFrom(origen => origen.IdUserAssetNavigation.name)
            )
            .ForMember(destino =>
                destino.nombreBusiness,
                opt => opt.MapFrom(origen => origen.IdBusinessNavigation.description)
            )
            .ForMember(destino =>
                destino.nombreManager,
                opt => opt.MapFrom(origen => origen.IdManagerNavigation.name)
            )
            .ForMember(destino =>
                destino.nombreStatus,
                opt => opt.MapFrom(origen => origen.IdStatusNavigation.description)
            )

            ;
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            //.ForMember(destino =>
            //    destino.Precio,
            //    opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-PE")))
            //);

            CreateMap<VMAsset, Asset>()
            //    .ForMember(destino =>
            //        destino.Active,
            //        opt => opt.MapFrom(origen => origen.Active == 1 ? true : false)
            //    );
            .ForMember(destino =>
                destino.IdAssetTypeNavigation,
                opt => opt.Ignore()
            );
            //.ForMember(destino =>
            //    destino.Precio,
            //    opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Precio, new CultureInfo("")))
            

            #endregion

            #region AssetType
            CreateMap<AssetType, VMAssetType>()
           .ForMember(destino =>
               destino.active,
               opt => opt.MapFrom(origen => origen.active == true ? 1 : 0)
           );

            CreateMap<VMAssetType, AssetType>()
            .ForMember(destino =>
                destino.active,
                opt => opt.MapFrom(origen => origen.active == 1 ? true : false)
            );
            #endregion

            #region Business
            CreateMap<Business, VMBusiness>()
           .ForMember(destino =>
               destino.active,
               opt => opt.MapFrom(origen => origen.active == true ? 1 : 0)
           );

            CreateMap<VMBusiness, Business>()
            .ForMember(destino =>
                destino.active,
                opt => opt.MapFrom(origen => origen.active == 1 ? true : false)
            );
            #endregion

            #region DailyReport

#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            CreateMap<DailyReport, VMDailyReport>()
            
            .ForMember(destino =>
                destino.nombreAsset,
                opt => opt.MapFrom(origen => origen.IdAssetNavigation.inter)
            )

            ;
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            CreateMap<VMDailyReport, DailyReport>()
           
            .ForMember(destino =>
                destino.IdAssetNavigation,
                opt => opt.Ignore()
            );
            
            #endregion

            #region Insurance
            CreateMap<Insurance, VMInsurance>()
           .ForMember(destino =>
               destino.active,
               opt => opt.MapFrom(origen => origen.active == true ? 1 : 0)
           );

            CreateMap<VMInsurance, Insurance>()
            .ForMember(destino =>
                destino.active,
                opt => opt.MapFrom(origen => origen.active == 1 ? true : false)
            );
            #endregion

            #region InsurancePT
            CreateMap<InsurancePT, VMInsurancePT>()
           .ForMember(destino =>
               destino.active,
               opt => opt.MapFrom(origen => origen.active == true ? 1 : 0)
           );

            CreateMap<VMInsurancePT, InsurancePT>()
            .ForMember(destino =>
                destino.active,
                opt => opt.MapFrom(origen => origen.active == 1 ? true : false)
            );
            #endregion

            #region Location
            CreateMap<Location, VMLocation>()
           .ForMember(destino =>
               destino.active,
               opt => opt.MapFrom(origen => origen.active == true ? 1 : 0)
           );

            CreateMap<VMLocation, Location>()
            .ForMember(destino =>
                destino.active,
                opt => opt.MapFrom(origen => origen.active == 1 ? true : false)
            );
            #endregion

            #region Manager


            CreateMap<Manager, VMManager>()
                .ForMember(destino =>
                    destino.active,
                    opt => opt.MapFrom(origen => origen.active == true ? 1 : 0)
                );

            CreateMap<VMManager, Manager>()
                .ForMember(destino =>
                    destino.active,
                    opt => opt.MapFrom(origen => origen.active == 1 ? true : false)
                );


            #endregion

            #region Priority
            CreateMap<Priority, VMPriority>()
           .ForMember(destino =>
               destino.active,
               opt => opt.MapFrom(origen => origen.active == true ? 1 : 0)
           );

            CreateMap<VMPriority, Priority>()
            .ForMember(destino =>
                destino.active,
                opt => opt.MapFrom(origen => origen.active == 1 ? true : false)
            );
            #endregion

            #region Sectional Registration
            CreateMap<SectionalRegistration, VMSectionalRegistration>()
           .ForMember(destino =>
               destino.active,
               opt => opt.MapFrom(origen => origen.active == true ? 1 : 0)
           );

            CreateMap<VMSectionalRegistration, SectionalRegistration>()
            .ForMember(destino =>
                destino.active,
                opt => opt.MapFrom(origen => origen.active == 1 ? true : false)
            );
            #endregion

            #region Status
            CreateMap<Status, VMStatus>()
           .ForMember(destino =>
               destino.active,
               opt => opt.MapFrom(origen => origen.active == true ? 1 : 0)
           );

            CreateMap<VMStatus, Status>()
            .ForMember(destino =>
                destino.active,
                opt => opt.MapFrom(origen => origen.active == 1 ? true : false)
            );
            #endregion     

            #region UserAsset


            CreateMap<UserAsset, VMUserAsset>()
                .ForMember(destino =>
                    destino.active,
                    opt => opt.MapFrom(origen => origen.active == true ? 1 : 0)
                );

            CreateMap<VMUserAsset, UserAsset>()
                .ForMember(destino =>
                    destino.active,
                    opt => opt.MapFrom(origen => origen.active == 1 ? true : false)
                );
          

            #endregion

        }

    }
}
