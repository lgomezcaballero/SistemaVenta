using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SistemaVenta.Entity;

namespace SistemaVenta.DAL.DBContext
{
    public partial class DBVENTAContext : DbContext
    {
        public DBVENTAContext()
        {
        }

        public DBVENTAContext(DbContextOptions<DBVENTAContext> options)
            : base(options)
        {
        }

        
        public virtual DbSet<Configuracion> Configuracions { get; set; } = null!;
        public virtual DbSet<Menu> Menus { get; set; } = null!;
        public virtual DbSet<Negocio> Negocios { get; set; } = null!;
        public virtual DbSet<NumeroCorrelativo> NumeroCorrelativos { get; set; } = null!;      
        public virtual DbSet<Rol> Rols { get; set; } = null!;
        public virtual DbSet<RolMenu> RolMenus { get; set; } = null!;
        
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;
       


        public virtual DbSet<Asset> Assets { get; set; } = null!;
        public virtual DbSet<AssetType> AssetTypes { get; set; } = null!;
        public virtual DbSet<Business> Businesses { get; set; } = null!;
        public virtual DbSet<DailyReport> DailyReports { get; set; } = null!;
        public virtual DbSet<FileTypes> FileTypes { get; set; } = null!;
        public virtual DbSet<Files> Files { get; set; } = null!;
        public virtual DbSet<Fuel> Fuels { get; set; } = null!;
        public virtual DbSet<FuelLoad> FuelLoads { get; set; } = null!;
        public virtual DbSet<Insurance> Insurances { get; set; } = null!;
        public virtual DbSet<InsurancePT> InsurancesPT { get; set; } = null!;
        public virtual DbSet<Location> Locations { get; set; } = null!;
        public virtual DbSet<Manager> Managers { get; set; } = null!;
        public virtual DbSet<Priority> Priorities { get; set; } = null!;
        public virtual DbSet<RequestAsset> RequestAssets { get; set; } = null!;
        public virtual DbSet<SectionalRegistration> SectionalRegistrations { get; set; } = null!;
        public virtual DbSet<Status> Status { get; set; } = null!;
        
        public virtual DbSet<UserAsset> UserAssets { get; set; } = null!;
        public virtual DbSet<Priority> Priority { get; set; } = null!;


        public virtual DbSet<AssetRequestType> AssetRequestType { get; set; } = null!;
        public virtual DbSet<Request> Request { get; set; } = null!;
        public virtual DbSet<RequestDetail> RequestDetail { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region ASSET
            modelBuilder.Entity<Asset>(entity =>
            {
                entity.HasKey(e => e.idAsset)
                    .HasName("PK__Asset__F59A58360172856B");

                entity.ToTable("Asset");

                entity.Property(e => e.idAsset).HasColumnName("idAsset");

                entity.Property(e => e.inter)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("inter");

                entity.Property(e => e.make)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("make");

                entity.Property(e => e.model)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("model");

                entity.Property(e => e.idAssetType).HasColumnName("idAssetType");

                entity.Property(e => e.year)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("year");

                entity.Property(e => e.licencePlate)
                   .HasMaxLength(100)
                   .IsUnicode(false)
                   .HasColumnName("licencePlate");

                entity.Property(e => e.vin)
                   .HasMaxLength(100)
                   .IsUnicode(false)
                   .HasColumnName("vin");

                entity.Property(e => e.engine)
                   .HasMaxLength(100)
                   .IsUnicode(false)
                   .HasColumnName("engine");

                entity.Property(e => e.idFuel).HasColumnName("idFuel");

                entity.Property(e => e.purchasedPrice)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("purchasedPrice");

                entity.Property(e => e.purchasedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("purchasedDate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.idInsurance).HasColumnName("idInsurance");

                entity.Property(e => e.idInsurancePT).HasColumnName("idInsurancePT");

                entity.Property(e => e.insuranceExpiration)
                    .HasColumnType("datetime")
                    .HasColumnName("insuranceExpiration")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.idSectionalRegistration).HasColumnName("idSectionalRegistration");

                entity.Property(e => e.idLocation).HasColumnName("idLocation");

                entity.Property(e => e.idUserAsset).HasColumnName("idUserAsset");

                entity.Property(e => e.idBusiness).HasColumnName("idBusiness");

                entity.Property(e => e.businessType)
                   .HasMaxLength(100)
                   .IsUnicode(false)
                   .HasColumnName("businessType");

                entity.Property(e => e.idManager).HasColumnName("idManager");

                entity.Property(e => e.idStatus).HasColumnName("idStatus");

                entity.Property(e => e.board)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("board");

                entity.Property(e => e.pictureUrl)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("pictureUrl");

                entity.Property(e => e.pictureName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("pictureName");              

                entity.Property(e => e.registerDate)
                    .HasColumnType("datetime")
                    .HasColumnName("registerDate")
                    .HasDefaultValueSql("(getdate())");




                entity.HasOne(d => d.IdAssetTypeNavigation)
                    .WithMany(p => p.Assets)
                    .HasForeignKey(d => d.idAssetType)
                    .HasConstraintName("FK__Asset__IdAssetTy__0880433F");

                entity.HasOne(d => d.IdBusinessNavigation)
                    .WithMany(p => p.Assets)
                    .HasForeignKey(d => d.idBusiness)
                    .HasConstraintName("FK__Asset__IdBusines__10216507");

                entity.HasOne(d => d.IdFuelNavigation)
                    .WithMany(p => p.Assets)
                    .HasForeignKey(d => d.idFuel)
                    .HasConstraintName("FK__Asset__IdFuel__09746778");

                entity.HasOne(d => d.IdInsuranceNavigation)
                    .WithMany(p => p.Assets)
                    .HasForeignKey(d => d.idInsurance)
                    .HasConstraintName("FK__Asset__IdInsuran__0B5CAFEA");

                entity.HasOne(d => d.IdInsurancePTNavigation)
                    .WithMany(p => p.Assets)
                    .HasForeignKey(d => d.idInsurancePT)
                    .HasConstraintName("FK__Asset__IdInsuran__0C50D423");

                entity.HasOne(d => d.IdLocationNavigation)
                    .WithMany(p => p.Assets)
                    .HasForeignKey(d => d.idLocation)
                    .HasConstraintName("FK__Asset__IdLocatio__0E391C95");

                entity.HasOne(d => d.IdManagerNavigation)
                    .WithMany(p => p.Assets)
                    .HasForeignKey(d => d.idManager)
                    .HasConstraintName("FK__Asset__IdManager__11158940");

                entity.HasOne(d => d.IdSectionalRegistrationNavigation)
                   .WithMany(p => p.Assets)
                   .HasForeignKey(d => d.idSectionalRegistration)
                   .HasConstraintName("FK__Asset__IdSection__0D44F85C");

                entity.HasOne(d => d.IdUserAssetNavigation)
                   .WithMany(p => p.Assets)
                   .HasForeignKey(d => d.idUserAsset)
                   .HasConstraintName("FK__Asset__IdUserAss__0F2D40CE");

                entity.HasOne(d => d.IdStatusNavigation)
                  .WithMany(p => p.Assets)
                  .HasForeignKey(d => d.idStatus)
                  .HasConstraintName("FK__Asset__IdStatus__1209AD79");

                //entity.HasOne(d => d.IdAssetNavigation)
                //                    .WithMany(p => p.DailyReport)
                //                    .HasForeignKey(d => d.IdAsset)
                //                    .HasConstraintName("FK__Venta__idUsuario__2C3393D0");

                //entity.HasOne(d => d.IdRequestAssetNavigation)
                //    .WithMany(p => p.Requests)
                //    .HasForeignKey(d => d.IdRequestAsset)
                //    .HasConstraintName("FK__RequestAs__IdAss__1D7B6025");

                //entity.HasOne(d => d.IdFuelLoadNavigation)
                //    .WithMany(p => p.FuelLoads)
                //    .HasForeignKey(d => d.IdFuelLoad)
                //    .HasConstraintName("FK__FuelLoad__IdAsse__22401542");


            });
            #endregion

            #region ASSETTYPE
            modelBuilder.Entity<AssetType>(entity =>
            {
                entity.HasKey(e => e.idAssetType)
                    .HasName("PK__AssetTyp__8136B4CE394D8B68");

                entity.ToTable("AssetType");

                entity.Property(e => e.idAssetType).HasColumnName("idAssetType");

                entity.Property(e => e.description)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.active).HasColumnName("active");

                entity.Property(e => e.registerDate)
                    .HasColumnType("datetime")
                    .HasColumnName("registerDate")
                    .HasDefaultValueSql("(getdate())");
            });
            #endregion

            #region ASSETREQUESTTYPE
            modelBuilder.Entity<AssetRequestType>(entity =>
            {
                entity.HasKey(e => e.idAssetRequestType)
                    .HasName("PK__AssetReq__77FA77DB27192093");

                entity.Property(e => e.idAssetRequestType).HasColumnName("idAssetRequestType");

                entity.Property(e => e.description)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.active).HasColumnName("active");

                entity.Property(e => e.registerDate)
                    .HasColumnType("datetime")
                    .HasColumnName("registerDate")
                    .HasDefaultValueSql("(getdate())");
            });
            #endregion

            #region BUSINESS
            modelBuilder.Entity<Business>(entity =>
            {
                entity.HasKey(e => e.idBusiness)
                    .HasName("PK__Business__9C92AEE5695DA492");

                entity.ToTable("Business");

                entity.Property(e => e.idBusiness).HasColumnName("idBusiness");

                entity.Property(e => e.description)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Description");

                entity.Property(e => e.ein)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ein");

                entity.Property(e => e.type)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("type");

                entity.Property(e => e.country)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("country");

                entity.Property(e => e.state)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("state");

                entity.Property(e => e.address)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.active).HasColumnName("active");

                entity.Property(e => e.registerDate)
                    .HasColumnType("datetime")
                    .HasColumnName("registerDate")
                    .HasDefaultValueSql("(getdate())");
            });
            #endregion

            #region DAILYREPORT
            modelBuilder.Entity<DailyReport>(entity =>
            {
                entity.HasKey(e => e.idDailyReport)
                    .HasName("PK__DailyRep__AE0A51F9D55169A0");

                entity.ToTable("DailyReport");

                entity.Property(e => e.idDailyReport).HasColumnName("idDailyReport");

                entity.Property(e => e.idAsset).HasColumnName("idAsset");

                entity.Property(e => e.numberReport)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("numberReport");             

                entity.Property(e => e.final)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("final");

                entity.Property(e => e.closingOfDay)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("closingOfDay");

                entity.Property(e => e.registerDate)
                    .HasColumnType("datetime")
                    .HasColumnName("registerDate")
                    .HasDefaultValueSql("(getdate())");


                entity.HasOne(d => d.IdAssetNavigation)
                    .WithMany(p => p.DailyReport)
                    .HasForeignKey(d => d.idAsset)
                    .HasConstraintName("FK__DailyRepo__IdAss__15DA3E5D");



            });
            #endregion

            #region FILES
            modelBuilder.Entity<Files>(entity =>
            {
                entity.HasKey(e => e.idFiles)
                    .HasName("PK__Files__9670D81D09F497BB");

                entity.Property(e => e.idFiles).HasColumnName("idFiles");

                entity.Property(e => e.idAsset).HasColumnName("idAsset");

                entity.Property(e => e.idFileTypes).HasColumnName("idFileTypes");

                entity.Property(e => e.filex)
                    .IsUnicode(false)
                    .HasColumnName("filex")
                    .HasColumnType("varbinary(max)");

                entity.Property(e => e.extension)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("extension");

                entity.Property(e => e.registerDate)
                    .HasColumnType("datetime")
                    .HasColumnName("registerDate")
                    .HasDefaultValueSql("(getdate())");


                entity.HasOne(d => d.IdAssetNavigation)
                    .WithMany(p => p.Files)
                    .HasForeignKey(d => d.idAsset)
                    .HasConstraintName("FK__Files__idAsset__436BFEE3");

                entity.HasOne(d => d.IdFileTypesNavigation)
                    .WithMany(p => p.Files)
                    .HasForeignKey(d => d.idFileTypes)
                    .HasConstraintName("FK__Request__idUsuar__32767D0B");


 

            });
            #endregion

            #region FILETYPES
            modelBuilder.Entity<FileTypes>(entity =>
            {
                entity.HasKey(e => e.idFileTypes)
                    .HasName("PK__FileType__B25BABD01A485845");

                entity.ToTable("FileTypes");

                entity.Property(e => e.idFileTypes).HasColumnName("idFileTypes");

                entity.Property(e => e.description)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.active).HasColumnName("active");

                entity.Property(e => e.registerDate)
                    .HasColumnType("datetime")
                    .HasColumnName("registerDate")
                    .HasDefaultValueSql("(getdate())");
            });
            #endregion

            #region FUEL
            modelBuilder.Entity<Fuel>(entity =>
            {
                entity.HasKey(e => e.idFuel)
                    .HasName("PK__Fuel__06E9F60CF53B9EA4");

                entity.ToTable("Fuel");

                entity.Property(e => e.idFuel).HasColumnName("idFuel");

                entity.Property(e => e.description)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.active).HasColumnName("Active");

                entity.Property(e => e.registerDate)
                    .HasColumnType("datetime")
                    .HasColumnName("registerDate")
                    .HasDefaultValueSql("(getdate())");
            });
            #endregion

            #region FUELLOAD
            modelBuilder.Entity<FuelLoad>(entity =>
            {
                entity.HasKey(e => e.IdFuelLoad)
                    .HasName("PK__FuelLoad__019070DDDDC33B88");

                entity.ToTable("FuelLoad");

                entity.Property(e => e.IdFuelLoad).HasColumnName("IdFuelLoad");

                entity.Property(e => e.IdAsset).HasColumnName("IdAsset");

                entity.Property(e => e.IdFuel).HasColumnName("IdFuel");

                entity.Property(e => e.Amount)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Amount");

                entity.Property(e => e.CurrentBoard)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("Final");

                entity.Property(e => e.RegisterDate)
                    .HasColumnType("datetime")
                    .HasColumnName("RegisterDate")
                    .HasDefaultValueSql("(getdate())");
            });
            #endregion

            #region INSURANCE
            modelBuilder.Entity<Insurance>(entity =>
            {
                entity.HasKey(e => e.idInsurance)
                    .HasName("PK__Insuranc__ADE0D10F942F5893");

                entity.ToTable("Insurance");

                entity.Property(e => e.idInsurance).HasColumnName("idInsurance");

                entity.Property(e => e.description)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.active).HasColumnName("active");

                entity.Property(e => e.registerDate)
                    .HasColumnType("datetime")
                    .HasColumnName("registerDate")
                    .HasDefaultValueSql("(getdate())");
            });
            #endregion

            #region INSURANCEPT
            modelBuilder.Entity<InsurancePT>(entity =>
            {
                entity.HasKey(e => e.idInsurancePT)
                    .HasName("PK__Insuranc__23064E97BCEC73AA");

                entity.ToTable("InsurancePT");

                entity.Property(e => e.idInsurancePT).HasColumnName("idInsurancePT");

                entity.Property(e => e.description)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.active).HasColumnName("active");

                entity.Property(e => e.registerDate)
                    .HasColumnType("datetime")
                    .HasColumnName("registerDate")
                    .HasDefaultValueSql("(getdate())");
            });
            #endregion

            #region LOCATION
            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasKey(e => e.idLocation)
                    .HasName("PK__Location__FB5FABA968E16F1E");

                entity.ToTable("Location");

                entity.Property(e => e.idLocation).HasColumnName("idLocation");

                entity.Property(e => e.description)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Description");

                entity.Property(e => e.name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.active).HasColumnName("active");

                entity.Property(e => e.registerDate)
                    .HasColumnType("datetime")
                    .HasColumnName("registerDate")
                    .HasDefaultValueSql("(getdate())");
            });
            #endregion

            #region MANAGER
            modelBuilder.Entity<Manager>(entity =>
            {
                entity.HasKey(e => e.idManager)
                    .HasName("PK__Manager__ABC3516E4B5D2F29");

                entity.ToTable("Manager");

                entity.Property(e => e.idManager).HasColumnName("idManager");

                entity.Property(e => e.name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.workFile)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("workFile");

                entity.Property(e => e.description)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.licence)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("licence");

                entity.Property(e => e.licenceExpiration)
                    .HasColumnType("datetime")
                    .HasColumnName("licenceExpiration")
                    .HasDefaultValueSql("(GETDATE())");

                entity.Property(e => e.birthday)
                    .HasColumnType("datetime")
                    .HasColumnName("birthday")
                    .HasDefaultValueSql("(GETDATE())");

                entity.Property(e => e.phone)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.emergencyContact)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("emergencyContact");

                entity.Property(e => e.email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.pictureUrl)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("pictureUrl");

                entity.Property(e => e.pictureName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("pictureName");
       
                entity.Property(e => e.active).HasColumnName("active");

                entity.Property(e => e.registerDate)
                    .HasColumnType("datetime")
                    .HasColumnName("registerDate")
                    .HasDefaultValueSql("(getdate())");
            });
            #endregion

            #region PRIORITY
            modelBuilder.Entity<Priority>(entity =>
            {
                entity.HasKey(e => e.idPriority)
                    .HasName("PK__Priority__20A1DC1ADF95F5E7");

                entity.ToTable("Priority");

                entity.Property(e => e.idPriority).HasColumnName("idPriority");

                entity.Property(e => e.description)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.active).HasColumnName("active");

                entity.Property(e => e.registerDate)
                    .HasColumnType("datetime")
                    .HasColumnName("registerDate")
                    .HasDefaultValueSql("(getdate())");
            });
            #endregion

            #region REQUEST
            modelBuilder.Entity<Request>(entity =>
            {
                entity.HasKey(e => e.idRequest)
                    .HasName("PK__Request__F4A4109E233170A5");

                entity.Property(e => e.idRequest).HasColumnName("idRequest");

                entity.Property(e => e.numberRequest)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("numberRequest");

                entity.Property(e => e.idAssetRequestType).HasColumnName("idAssetRequestType");

                entity.Property(e => e.idUsuario).HasColumnName("idUsuario");

                entity.Property(e => e.idPriority).HasColumnName("idPriority");

                entity.Property(e => e.registerDate)
                    .HasColumnType("datetime")
                    .HasColumnName("registerDate")
                    .HasDefaultValueSql("(getdate())");

               
                

                entity.HasOne(d => d.IdAssetRequestTypeNavigation)
                    .WithMany(p => p.Request)
                    .HasForeignKey(d => d.idAssetRequestType)
                    .HasConstraintName("FK__Request__idAsset__318258D2");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Request)
                    .HasForeignKey(d => d.idUsuario)
                    .HasConstraintName("FK__Request__idUsuar__32767D0B");


                entity.HasOne(d => d.IdPriorityNavigation)
                    .WithMany(p => p.Request)
                    .HasForeignKey(d => d.idPriority)
                    .HasConstraintName("FK__Request__idPrior__336AA144");




            });
            #endregion

            #region REQUESTASSET
            modelBuilder.Entity<RequestAsset>(entity =>
            {
                entity.HasKey(e => e.IdRequestAsset)
                    .HasName("PK__RequestA__02AA205451855708");

                entity.ToTable("RequestAsset");

                entity.Property(e => e.IdRequestAsset).HasColumnName("IdRequestAsset");

                entity.Property(e => e.IdAsset).HasColumnName("IdAsset");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Description");

                entity.Property(e => e.PictureName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("PictureName");

                entity.Property(e => e.PictureUrl)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("PictureUrl");

                entity.Property(e => e.IdPriority).HasColumnName("IdPriority");

                entity.Property(e => e.CurrentBoard)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("Final");

                entity.Property(e => e.RegisterDate)
                    .HasColumnType("datetime")
                    .HasColumnName("RegisterDate")
                    .HasDefaultValueSql("(getdate())");
            });
            #endregion

            #region REQUESTDETAIL
            modelBuilder.Entity<RequestDetail>(entity =>
            {
                entity.HasKey(e => e.idRequestDetail)
                    .HasName("PK__RequestD__98533C81A8C6191D");

                entity.Property(e => e.idRequestDetail).HasColumnName("idRequestDetail");


                entity.Property(e => e.makerAsset)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("makerAsset");

                entity.Property(e => e.modelAsset)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("modelAsset");

                entity.Property(e => e.assetType)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("assetType");

                entity.Property(e => e.assetInternal)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("assetInternal");

                entity.Property(e => e.fuelAsset)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("fuelAsset");

                entity.Property(e => e.locationAsset)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("locationAsset");

                entity.Property(e => e.userAsset)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("userAsset");

                entity.Property(e => e.managerAsset)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("managerAsset");

                entity.Property(e => e.yearAsset)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("yearAsset");

                entity.Property(e => e.licencePlateAsset)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("licencePlateAsset");




                entity.Property(e => e.idRequest).HasColumnName("idRequest");

                entity.Property(e => e.idAsset).HasColumnName("idAsset");

               
                entity.HasOne(d => d.IdRequestNavigation)
                    .WithMany(p => p.RequestDetail)
                    .HasForeignKey(d => d.idRequest)
                    .HasConstraintName("FK_RequestDetail_Request");

                entity.HasOne(d => d.IdAssetNavigation)
                    .WithMany(p => p.RequestDetail)
                    .HasForeignKey(d => d.idAsset)
                    .HasConstraintName("FK__RequestDe__idAss__6ABAD62E");
            });
            #endregion

            #region SECTIONALREGISTRATION
            modelBuilder.Entity<SectionalRegistration>(entity =>
            {
                entity.HasKey(e => e.idSectionalRegistration)
                    .HasName("PK__Sectiona__BE9876AECD25C32C");

                entity.ToTable("SectionalRegistration");

                entity.Property(e => e.idSectionalRegistration).HasColumnName("idSectionalRegistration");

                entity.Property(e => e.description)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.state)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("state");

                entity.Property(e => e.location)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("location");

                entity.Property(e => e.address)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.active).HasColumnName("active");

                entity.Property(e => e.registerDate)
                    .HasColumnType("datetime")
                    .HasColumnName("registerDate")
                    .HasDefaultValueSql("(getdate())");
            });
            #endregion

            #region STATUS
            modelBuilder.Entity<Status>(entity =>
            {
                entity.HasKey(e => e.idStatus)
                    .HasName("PK__Status__B450643AECFDB46C");

                entity.Property(e => e.idStatus).HasColumnName("idStatus");

                entity.Property(e => e.description)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.active).HasColumnName("active");

                entity.Property(e => e.registerDate)
                    .HasColumnType("datetime")
                    .HasColumnName("registerDate")
                    .HasDefaultValueSql("(getdate())");
            });
            #endregion

            #region USERASSET
            modelBuilder.Entity<UserAsset>(entity =>
            {
                entity.HasKey(e => e.idUserAsset)
                    .HasName("PK__UserAsse__B27686FB9513E19D");

                entity.ToTable("UserAsset");

                entity.Property(e => e.idUserAsset).HasColumnName("idUserAsset");

                entity.Property(e => e.name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.workFile)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("workFile");

                entity.Property(e => e.licence)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("licence");

                entity.Property(e => e.licenceExpiration)
                    .HasColumnType("datetime")
                    .HasColumnName("licenceExpiration")
                    .HasDefaultValueSql("(GETDATE())");

                entity.Property(e => e.birthday)
                    .HasColumnType("datetime")
                    .HasColumnName("birthday")
                    .HasDefaultValueSql("(GETDATE())");

                entity.Property(e => e.phone)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.emergencyContact)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("emergencyContact");

                entity.Property(e => e.email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.pictureUrl)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("pictureUrl");

                entity.Property(e => e.pictureName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("pictureName");

                entity.Property(e => e.fileLicenceUrl)
                   .HasMaxLength(500)
                   .IsUnicode(false)
                   .HasColumnName("fileLicenceUrl");

                entity.Property(e => e.fileLicenceName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("fileLicenceName");

                entity.Property(e => e.active).HasColumnName("active");

                entity.Property(e => e.registerDate)
                    .HasColumnType("datetime")
                    .HasColumnName("registerDate")
                    .HasDefaultValueSql("(getdate())");

                

            });
            #endregion

            #region USUARIO-USER
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__Usuario__645723A6E5D3BBB4");

                entity.ToTable("Usuario");

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.Property(e => e.Clave)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("clave");

                entity.Property(e => e.Correo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("correo");

                entity.Property(e => e.EsActivo).HasColumnName("esActivo");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdRol).HasColumnName("idRol");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.NombreFoto)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombreFoto");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("telefono");

                entity.Property(e => e.UrlFoto)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("urlFoto");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("FK__Usuario__idRol__1BFD2C07");
            });
            #endregion

    



            // zona de enlace relacionada al template por defecto

           

            #region CONFIGURACION
            modelBuilder.Entity<Configuracion>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Configuracion");

                entity.Property(e => e.Propiedad)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("propiedad");

                entity.Property(e => e.Recurso)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("recurso");

                entity.Property(e => e.Valor)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("valor");
            });
            #endregion

            

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.HasKey(e => e.IdMenu)
                    .HasName("PK__Menu__C26AF483CA54C183");

                entity.ToTable("Menu");

                entity.Property(e => e.IdMenu).HasColumnName("idMenu");

                entity.Property(e => e.Controlador)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("controlador");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.EsActivo).HasColumnName("esActivo");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Icono)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("icono");

                entity.Property(e => e.IdMenuPadre).HasColumnName("idMenuPadre");

                entity.Property(e => e.PaginaAccion)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("paginaAccion");

                entity.HasOne(d => d.IdMenuPadreNavigation)
                    .WithMany(p => p.InverseIdMenuPadreNavigation)
                    .HasForeignKey(d => d.IdMenuPadre)
                    .HasConstraintName("FK__Menu__idMenuPadr__108B795B");
            });

            modelBuilder.Entity<Negocio>(entity =>
            {
                entity.HasKey(e => e.IdNegocio)
                    .HasName("PK__Negocio__70E1E107C38B04E3");

                entity.ToTable("Negocio");

                entity.Property(e => e.IdNegocio)
                    .ValueGeneratedNever()
                    .HasColumnName("idNegocio");

                entity.Property(e => e.Correo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("correo");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("direccion");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.NombreLogo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombreLogo");

                entity.Property(e => e.NumeroDocumento)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("numeroDocumento");

                entity.Property(e => e.PorcentajeImpuesto)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("porcentajeImpuesto");

                entity.Property(e => e.SimboloMoneda)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("simboloMoneda");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("telefono");

                entity.Property(e => e.UrlLogo)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("urlLogo");
            });

            modelBuilder.Entity<NumeroCorrelativo>(entity =>
            {
                entity.HasKey(e => e.IdNumeroCorrelativo)
                    .HasName("PK__NumeroCo__25FB547EF83526BF");

                entity.ToTable("NumeroCorrelativo");

                entity.Property(e => e.IdNumeroCorrelativo).HasColumnName("idNumeroCorrelativo");

                entity.Property(e => e.CantidadDigitos).HasColumnName("cantidadDigitos");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaActualizacion");

                entity.Property(e => e.Gestion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("gestion");

                entity.Property(e => e.UltimoNumero).HasColumnName("ultimoNumero");
            });

            

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.IdRol)
                    .HasName("PK__Rol__3C872F763C1D1497");

                entity.ToTable("Rol");

                entity.Property(e => e.IdRol).HasColumnName("idRol");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.EsActivo).HasColumnName("esActivo");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<RolMenu>(entity =>
            {
                entity.HasKey(e => e.IdRolMenu)
                    .HasName("PK__RolMenu__CD2045D8F6A2531C");

                entity.ToTable("RolMenu");

                entity.Property(e => e.IdRolMenu).HasColumnName("idRolMenu");

                entity.Property(e => e.EsActivo).HasColumnName("esActivo");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdMenu).HasColumnName("idMenu");

                entity.Property(e => e.IdRol).HasColumnName("idRol");

                entity.HasOne(d => d.IdMenuNavigation)
                    .WithMany(p => p.RolMenus)
                    .HasForeignKey(d => d.IdMenu)
                    .HasConstraintName("FK__RolMenu__idMenu__182C9B23");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.RolMenus)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("FK__RolMenu__idRol__173876EA");
            });

            

            

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
