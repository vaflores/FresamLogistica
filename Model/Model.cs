using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace FresamLogistica.Model
{
    public partial class Model : DbContext
    {
        public Model()
            : base("name=Model")
        {
        }

        public virtual DbSet<AgricultorCajasSaldo> AgricultorCajasSaldo { get; set; }
        public virtual DbSet<Agricultores> Agricultores { get; set; }
        public virtual DbSet<Calidades> Calidades { get; set; }
        public virtual DbSet<CalidadesRecepciones> CalidadesRecepciones { get; set; }
        public virtual DbSet<CicloCalidades> CicloCalidades { get; set; }
        public virtual DbSet<CicloEtiquetas> CicloEtiquetas { get; set; }
        public virtual DbSet<CicloGrados> CicloGrados { get; set; }
        public virtual DbSet<CicloInspecciones> CicloInspecciones { get; set; }
        public virtual DbSet<CicloMangos> CicloMangos { get; set; }
        public virtual DbSet<Ciclos> Ciclos { get; set; }
        public virtual DbSet<CiclosResiduos> CiclosResiduos { get; set; }
        public virtual DbSet<Ciudades> Ciudades { get; set; }
        public virtual DbSet<Condiciones> Condiciones { get; set; }
        public virtual DbSet<CondicionesCajas> CondicionesCajas { get; set; }
        public virtual DbSet<CondicionesVehiculosRecepciones> CondicionesVehiculosRecepciones { get; set; }
        public virtual DbSet<ConfiguracionFirmasReportes> ConfiguracionFirmasReportes { get; set; }
        public virtual DbSet<ConfiguracionGeneralCajas> ConfiguracionGeneralCajas { get; set; }
        public virtual DbSet<DescuentosRecepciones> DescuentosRecepciones { get; set; }
        public virtual DbSet<DestinosResiduo> DestinosResiduo { get; set; }
        public virtual DbSet<DetalleRegistroResiduoRecolectado> DetalleRegistroResiduoRecolectado { get; set; }
        public virtual DbSet<Empleados> Empleados { get; set; }
        public virtual DbSet<Empresa> Empresa { get; set; }
        public virtual DbSet<EmpresaGestoraDeResiduos> EmpresaGestoraDeResiduos { get; set; }
        public virtual DbSet<Estados> Estados { get; set; }
        public virtual DbSet<Folios> Folios { get; set; }
        public virtual DbSet<FoliosRegistroResiduoRecolectado> FoliosRegistroResiduoRecolectado { get; set; }
        public virtual DbSet<Frutas> Frutas { get; set; }
        public virtual DbSet<GradosFruta> GradosFruta { get; set; }
        public virtual DbSet<KardexCajas> KardexCajas { get; set; }
        public virtual DbSet<KardexCondicionesCajas> KardexCondicionesCajas { get; set; }
        public virtual DbSet<LoteFolios> LoteFolios { get; set; }
        public virtual DbSet<MangoCalidades> MangoCalidades { get; set; }
        public virtual DbSet<MangoCalidadesRecepcion> MangoCalidadesRecepcion { get; set; }
        public virtual DbSet<MangoCondicionesVehiculoRecepcion> MangoCondicionesVehiculoRecepcion { get; set; }
        public virtual DbSet<MangoConfiguracion> MangoConfiguracion { get; set; }
        public virtual DbSet<MangoDescuentos> MangoDescuentos { get; set; }
        public virtual DbSet<MangoLocalidades> MangoLocalidades { get; set; }
        public virtual DbSet<MangoMuestreos> MangoMuestreos { get; set; }
        public virtual DbSet<MangoMuestreosCalidades> MangoMuestreosCalidades { get; set; }
        public virtual DbSet<MangoPredios> MangoPredios { get; set; }
        public virtual DbSet<MangoPropietarios> MangoPropietarios { get; set; }
        public virtual DbSet<MangoProveedores> MangoProveedores { get; set; }
        public virtual DbSet<MangoRecepciones> MangoRecepciones { get; set; }
        public virtual DbSet<MangoRecepcionesSalidas> MangoRecepcionesSalidas { get; set; }
        public virtual DbSet<MangoRecepcionesSalidasDetalle> MangoRecepcionesSalidasDetalle { get; set; }
        public virtual DbSet<MangoTiposSalidas> MangoTiposSalidas { get; set; }
        public virtual DbSet<MangoVehiculoRecepcion> MangoVehiculoRecepcion { get; set; }
        public virtual DbSet<MuestreosCalidadesRecepciones> MuestreosCalidadesRecepciones { get; set; }
        public virtual DbSet<MuestreosRecepciones> MuestreosRecepciones { get; set; }
        public virtual DbSet<OperadoresResiduos> OperadoresResiduos { get; set; }
        public virtual DbSet<Pantallas> Pantallas { get; set; }
        public virtual DbSet<Permisos> Permisos { get; set; }
        public virtual DbSet<Precios> Precios { get; set; }
        public virtual DbSet<Producciones> Producciones { get; set; }
        public virtual DbSet<Puestos> Puestos { get; set; }
        public virtual DbSet<Ranchos> Ranchos { get; set; }
        public virtual DbSet<Recepciones> Recepciones { get; set; }
        public virtual DbSet<RegistrosResiduoRecolectado> RegistrosResiduoRecolectado { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Sectores> Sectores { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Tarimas> Tarimas { get; set; }
        public virtual DbSet<TipoResiduo> TipoResiduo { get; set; }
        public virtual DbSet<TiposDestinoResiduo> TiposDestinoResiduo { get; set; }
        public virtual DbSet<TitulosReporteResiduos> TitulosReporteResiduos { get; set; }
        public virtual DbSet<UnidadesMedida> UnidadesMedida { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
        public virtual DbSet<Variedades> Variedades { get; set; }
        public virtual DbSet<Vehiculos> Vehiculos { get; set; }
        public virtual DbSet<VehiculosRecepciones> VehiculosRecepciones { get; set; }
        public virtual DbSet<RolesPermisos> RolesPermisos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AgricultorCajasSaldo>()
                .Property(e => e.total_cajas)
                .HasPrecision(10, 0);

            modelBuilder.Entity<AgricultorCajasSaldo>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<AgricultorCajasSaldo>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<Agricultores>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Agricultores>()
                .Property(e => e.rfc)
                .IsUnicode(false);

            modelBuilder.Entity<Agricultores>()
                .Property(e => e.calle)
                .IsUnicode(false);

            modelBuilder.Entity<Agricultores>()
                .Property(e => e.colonia)
                .IsUnicode(false);

            modelBuilder.Entity<Agricultores>()
                .Property(e => e.telefono1)
                .IsUnicode(false);

            modelBuilder.Entity<Agricultores>()
                .Property(e => e.telefono2)
                .IsUnicode(false);

            modelBuilder.Entity<Agricultores>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<Agricultores>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<Agricultores>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Agricultores>()
                .HasOptional(e => e.AgricultorCajasSaldo)
                .WithRequired(e => e.Agricultores);

            modelBuilder.Entity<Agricultores>()
                .HasMany(e => e.KardexCajas)
                .WithRequired(e => e.Agricultores)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Agricultores>()
                .HasMany(e => e.Ranchos)
                .WithRequired(e => e.Agricultores)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Agricultores>()
                .HasMany(e => e.Recepciones)
                .WithRequired(e => e.Agricultores)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Calidades>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Calidades>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Calidades>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<Calidades>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<Calidades>()
                .HasMany(e => e.CalidadesRecepciones)
                .WithRequired(e => e.Calidades)
                .HasForeignKey(e => e.calidad_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Calidades>()
                .HasMany(e => e.MuestreosCalidadesRecepciones)
                .WithRequired(e => e.Calidades)
                .HasForeignKey(e => e.calidad_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CalidadesRecepciones>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<CalidadesRecepciones>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<CicloCalidades>()
                .Property(e => e.titulo1)
                .IsUnicode(false);

            modelBuilder.Entity<CicloCalidades>()
                .Property(e => e.titulo2)
                .IsUnicode(false);

            modelBuilder.Entity<CicloCalidades>()
                .Property(e => e.titulo3)
                .IsUnicode(false);

            modelBuilder.Entity<CicloCalidades>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CicloCalidades>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<CicloCalidades>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<CicloEtiquetas>()
                .Property(e => e.titulo1)
                .IsUnicode(false);

            modelBuilder.Entity<CicloEtiquetas>()
                .Property(e => e.titulo2)
                .IsUnicode(false);

            modelBuilder.Entity<CicloEtiquetas>()
                .Property(e => e.titulo3)
                .IsUnicode(false);

            modelBuilder.Entity<CicloEtiquetas>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CicloEtiquetas>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<CicloEtiquetas>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<CicloGrados>()
                .Property(e => e.titulo1)
                .IsUnicode(false);

            modelBuilder.Entity<CicloGrados>()
                .Property(e => e.titulo2)
                .IsUnicode(false);

            modelBuilder.Entity<CicloGrados>()
                .Property(e => e.titulo3)
                .IsUnicode(false);

            modelBuilder.Entity<CicloGrados>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CicloGrados>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<CicloGrados>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<CicloInspecciones>()
                .Property(e => e.titulo1)
                .IsUnicode(false);

            modelBuilder.Entity<CicloInspecciones>()
                .Property(e => e.titulo2)
                .IsUnicode(false);

            modelBuilder.Entity<CicloInspecciones>()
                .Property(e => e.titulo3)
                .IsUnicode(false);

            modelBuilder.Entity<CicloInspecciones>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CicloInspecciones>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<CicloInspecciones>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<CicloMangos>()
                .Property(e => e.titulo1)
                .IsUnicode(false);

            modelBuilder.Entity<CicloMangos>()
                .Property(e => e.titulo2)
                .IsUnicode(false);

            modelBuilder.Entity<CicloMangos>()
                .Property(e => e.titulo3)
                .IsUnicode(false);

            modelBuilder.Entity<CicloMangos>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CicloMangos>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<CicloMangos>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<Ciclos>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Ciclos>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<Ciclos>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<Ciclos>()
                .Property(e => e.proxima_revision)
                .IsUnicode(false);

            modelBuilder.Entity<Ciclos>()
                .Property(e => e.fecha_emision)
                .IsUnicode(false);

            modelBuilder.Entity<Ciclos>()
                .Property(e => e.edicion)
                .IsUnicode(false);

            modelBuilder.Entity<Ciclos>()
                .HasMany(e => e.CicloCalidades)
                .WithRequired(e => e.Ciclos)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ciclos>()
                .HasMany(e => e.CicloEtiquetas)
                .WithRequired(e => e.Ciclos)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ciclos>()
                .HasMany(e => e.CicloGrados)
                .WithRequired(e => e.Ciclos)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ciclos>()
                .HasMany(e => e.CicloInspecciones)
                .WithRequired(e => e.Ciclos)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ciclos>()
                .HasMany(e => e.CicloMangos)
                .WithRequired(e => e.Ciclos)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ciclos>()
                .HasMany(e => e.MangoRecepciones)
                .WithRequired(e => e.Ciclos)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ciclos>()
                .HasMany(e => e.Recepciones)
                .WithRequired(e => e.Ciclos)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ciclos>()
                .HasMany(e => e.Sectores)
                .WithRequired(e => e.Ciclos)
                .HasForeignKey(e => e.id_ciclo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CiclosResiduos>()
                .Property(e => e.proxima_revision)
                .IsUnicode(false);

            modelBuilder.Entity<CiclosResiduos>()
                .Property(e => e.edicion)
                .IsUnicode(false);

            modelBuilder.Entity<CiclosResiduos>()
                .Property(e => e.fecha_emision)
                .IsUnicode(false);

            modelBuilder.Entity<CiclosResiduos>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CiclosResiduos>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<CiclosResiduos>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<CiclosResiduos>()
                .HasMany(e => e.TitulosReporteResiduos)
                .WithRequired(e => e.CiclosResiduos)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ciudades>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Ciudades>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Ciudades>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<Ciudades>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<Ciudades>()
                .HasMany(e => e.Agricultores)
                .WithRequired(e => e.Ciudades)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ciudades>()
                .HasMany(e => e.DestinosResiduo)
                .WithRequired(e => e.Ciudades)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Condiciones>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Condiciones>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Condiciones>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<Condiciones>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<Condiciones>()
                .HasMany(e => e.CondicionesVehiculosRecepciones)
                .WithRequired(e => e.Condiciones)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Condiciones>()
                .HasMany(e => e.MangoCondicionesVehiculoRecepcion)
                .WithRequired(e => e.Condiciones)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CondicionesCajas>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<CondicionesCajas>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<CondicionesCajas>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CondicionesCajas>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<CondicionesCajas>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<CondicionesCajas>()
                .HasMany(e => e.KardexCondicionesCajas)
                .WithRequired(e => e.CondicionesCajas)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CondicionesVehiculosRecepciones>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<CondicionesVehiculosRecepciones>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<ConfiguracionFirmasReportes>()
                .Property(e => e.reporte_clave)
                .IsUnicode(false);

            modelBuilder.Entity<ConfiguracionFirmasReportes>()
                .Property(e => e.etiqueta_firma)
                .IsUnicode(false);

            modelBuilder.Entity<ConfiguracionFirmasReportes>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<ConfiguracionFirmasReportes>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<ConfiguracionGeneralCajas>()
                .Property(e => e.costo_caja)
                .HasPrecision(10, 2);

            modelBuilder.Entity<ConfiguracionGeneralCajas>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<ConfiguracionGeneralCajas>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<DescuentosRecepciones>()
                .Property(e => e.observaciones)
                .IsUnicode(false);

            modelBuilder.Entity<DescuentosRecepciones>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<DescuentosRecepciones>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<DestinosResiduo>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<DestinosResiduo>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DestinosResiduo>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<DestinosResiduo>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<DestinosResiduo>()
                .HasMany(e => e.LoteFolios)
                .WithRequired(e => e.DestinosResiduo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DestinosResiduo>()
                .HasMany(e => e.RegistrosResiduoRecolectado)
                .WithRequired(e => e.DestinosResiduo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DetalleRegistroResiduoRecolectado>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<DetalleRegistroResiduoRecolectado>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<Empleados>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Empleados>()
                .Property(e => e.apellido_paterno)
                .IsUnicode(false);

            modelBuilder.Entity<Empleados>()
                .Property(e => e.apellido_materno)
                .IsUnicode(false);

            modelBuilder.Entity<Empleados>()
                .Property(e => e.estado)
                .IsUnicode(false);

            modelBuilder.Entity<Empleados>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<Empleados>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<Empleados>()
                .HasMany(e => e.ConfiguracionFirmasReportes)
                .WithRequired(e => e.Empleados)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Empleados>()
                .HasMany(e => e.MangoMuestreos)
                .WithOptional(e => e.Empleados)
                .HasForeignKey(e => e.responsable_empleado_id);

            modelBuilder.Entity<Empleados>()
                .HasMany(e => e.MangoMuestreos1)
                .WithOptional(e => e.Empleados1)
                .HasForeignKey(e => e.verifica_empleado_id);

            modelBuilder.Entity<Empresa>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Empresa>()
                .Property(e => e.calle)
                .IsUnicode(false);

            modelBuilder.Entity<Empresa>()
                .Property(e => e.colonia)
                .IsUnicode(false);

            modelBuilder.Entity<Empresa>()
                .Property(e => e.ciudad)
                .IsUnicode(false);

            modelBuilder.Entity<Empresa>()
                .Property(e => e.estado)
                .IsUnicode(false);

            modelBuilder.Entity<Empresa>()
                .Property(e => e.rfc)
                .IsUnicode(false);

            modelBuilder.Entity<Empresa>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<Empresa>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<EmpresaGestoraDeResiduos>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<EmpresaGestoraDeResiduos>()
                .Property(e => e.RFC)
                .IsUnicode(false);

            modelBuilder.Entity<EmpresaGestoraDeResiduos>()
                .Property(e => e.telefono)
                .IsUnicode(false);

            modelBuilder.Entity<EmpresaGestoraDeResiduos>()
                .Property(e => e.licenciaSCT)
                .IsUnicode(false);

            modelBuilder.Entity<EmpresaGestoraDeResiduos>()
                .Property(e => e.licenciaEcologica)
                .IsUnicode(false);

            modelBuilder.Entity<EmpresaGestoraDeResiduos>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<EmpresaGestoraDeResiduos>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<EmpresaGestoraDeResiduos>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<EmpresaGestoraDeResiduos>()
                .HasMany(e => e.OperadoresResiduos)
                .WithRequired(e => e.EmpresaGestoraDeResiduos)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Estados>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Estados>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Estados>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<Estados>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<Estados>()
                .Property(e => e.abreviatura)
                .IsUnicode(false);

            modelBuilder.Entity<Estados>()
                .HasMany(e => e.Agricultores)
                .WithRequired(e => e.Estados)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Estados>()
                .HasMany(e => e.Ciudades)
                .WithRequired(e => e.Estados)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Folios>()
                .Property(e => e.costo)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Folios>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<Folios>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<Folios>()
                .HasMany(e => e.FoliosRegistroResiduoRecolectado)
                .WithRequired(e => e.Folios)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FoliosRegistroResiduoRecolectado>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<FoliosRegistroResiduoRecolectado>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<Frutas>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Frutas>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Frutas>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<Frutas>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<Frutas>()
                .HasMany(e => e.GradosFruta)
                .WithRequired(e => e.Frutas)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Frutas>()
                .HasMany(e => e.Precios)
                .WithRequired(e => e.Frutas)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Frutas>()
                .HasMany(e => e.Producciones)
                .WithRequired(e => e.Frutas)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Frutas>()
                .HasMany(e => e.TipoResiduo)
                .WithRequired(e => e.Frutas)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Frutas>()
                .HasMany(e => e.Variedades)
                .WithRequired(e => e.Frutas)
                .HasForeignKey(e => e.id_fruta)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GradosFruta>()
                .Property(e => e.grado_id)
                .IsUnicode(false);

            modelBuilder.Entity<GradosFruta>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<GradosFruta>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<GradosFruta>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<KardexCajas>()
                .Property(e => e.tipo_movimiento)
                .IsUnicode(false);

            modelBuilder.Entity<KardexCajas>()
                .Property(e => e.entrada)
                .HasPrecision(10, 0);

            modelBuilder.Entity<KardexCajas>()
                .Property(e => e.salida)
                .HasPrecision(10, 0);

            modelBuilder.Entity<KardexCajas>()
                .Property(e => e.saldo_anterior)
                .HasPrecision(10, 0);

            modelBuilder.Entity<KardexCajas>()
                .Property(e => e.saldo_actual)
                .HasPrecision(10, 0);

            modelBuilder.Entity<KardexCajas>()
                .Property(e => e.observaciones)
                .IsUnicode(false);

            modelBuilder.Entity<KardexCajas>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<KardexCajas>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<KardexCajas>()
                .HasMany(e => e.KardexCondicionesCajas)
                .WithRequired(e => e.KardexCajas)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KardexCondicionesCajas>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<KardexCondicionesCajas>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<LoteFolios>()
                .Property(e => e.importe)
                .HasPrecision(19, 4);

            modelBuilder.Entity<LoteFolios>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<LoteFolios>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<LoteFolios>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<LoteFolios>()
                .HasMany(e => e.Folios)
                .WithRequired(e => e.LoteFolios)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MangoCalidades>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<MangoCalidades>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<MangoCalidades>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<MangoCalidades>()
                .HasMany(e => e.MangoCalidadesRecepcion)
                .WithRequired(e => e.MangoCalidades)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MangoCalidades>()
                .HasMany(e => e.MangoMuestreosCalidades)
                .WithRequired(e => e.MangoCalidades)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MangoCalidadesRecepcion>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<MangoCalidadesRecepcion>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<MangoCondicionesVehiculoRecepcion>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<MangoCondicionesVehiculoRecepcion>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<MangoConfiguracion>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<MangoConfiguracion>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<MangoDescuentos>()
                .Property(e => e.observaciones)
                .IsUnicode(false);

            modelBuilder.Entity<MangoDescuentos>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<MangoDescuentos>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<MangoLocalidades>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<MangoLocalidades>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<MangoLocalidades>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<MangoLocalidades>()
                .HasMany(e => e.MangoPredios)
                .WithRequired(e => e.MangoLocalidades)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MangoMuestreos>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<MangoMuestreos>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<MangoMuestreos>()
                .HasMany(e => e.MangoMuestreosCalidades)
                .WithRequired(e => e.MangoMuestreos)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MangoMuestreosCalidades>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<MangoMuestreosCalidades>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<MangoPredios>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<MangoPredios>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<MangoPredios>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<MangoPredios>()
                .HasMany(e => e.MangoRecepciones)
                .WithRequired(e => e.MangoPredios)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MangoPropietarios>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<MangoPropietarios>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<MangoPropietarios>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<MangoPropietarios>()
                .HasMany(e => e.MangoPredios)
                .WithRequired(e => e.MangoPropietarios)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MangoProveedores>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<MangoProveedores>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<MangoProveedores>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<MangoProveedores>()
                .Property(e => e.codigo)
                .IsUnicode(false);

            modelBuilder.Entity<MangoProveedores>()
                .HasMany(e => e.MangoRecepciones)
                .WithRequired(e => e.MangoProveedores)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MangoRecepciones>()
                .Property(e => e.grado_id)
                .IsUnicode(false);

            modelBuilder.Entity<MangoRecepciones>()
                .Property(e => e.importe)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MangoRecepciones>()
                .Property(e => e.precio_unitario)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MangoRecepciones>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<MangoRecepciones>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<MangoRecepciones>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<MangoRecepciones>()
                .HasMany(e => e.MangoCalidadesRecepcion)
                .WithRequired(e => e.MangoRecepciones)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MangoRecepciones>()
                .HasMany(e => e.MangoDescuentos)
                .WithRequired(e => e.MangoRecepciones)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MangoRecepciones>()
                .HasMany(e => e.MangoMuestreos)
                .WithRequired(e => e.MangoRecepciones)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MangoRecepciones>()
                .HasMany(e => e.MangoRecepcionesSalidas)
                .WithRequired(e => e.MangoRecepciones)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MangoRecepciones>()
                .HasMany(e => e.MangoVehiculoRecepcion)
                .WithRequired(e => e.MangoRecepciones)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MangoRecepcionesSalidas>()
                .Property(e => e.observaciones)
                .IsUnicode(false);

            modelBuilder.Entity<MangoRecepcionesSalidas>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<MangoRecepcionesSalidas>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<MangoRecepcionesSalidas>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<MangoRecepcionesSalidas>()
                .HasMany(e => e.MangoRecepcionesSalidasDetalle)
                .WithRequired(e => e.MangoRecepcionesSalidas)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MangoRecepcionesSalidasDetalle>()
                .Property(e => e.observaciones)
                .IsUnicode(false);

            modelBuilder.Entity<MangoRecepcionesSalidasDetalle>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<MangoRecepcionesSalidasDetalle>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<MangoTiposSalidas>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<MangoTiposSalidas>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<MangoTiposSalidas>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<MangoTiposSalidas>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<MangoTiposSalidas>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<MangoTiposSalidas>()
                .HasMany(e => e.MangoRecepcionesSalidasDetalle)
                .WithRequired(e => e.MangoTiposSalidas)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MangoVehiculoRecepcion>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<MangoVehiculoRecepcion>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<MangoVehiculoRecepcion>()
                .HasMany(e => e.MangoCondicionesVehiculoRecepcion)
                .WithRequired(e => e.MangoVehiculoRecepcion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MuestreosCalidadesRecepciones>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<MuestreosCalidadesRecepciones>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<MuestreosRecepciones>()
                .Property(e => e.observaciones)
                .IsUnicode(false);

            modelBuilder.Entity<MuestreosRecepciones>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<MuestreosRecepciones>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<MuestreosRecepciones>()
                .HasMany(e => e.MuestreosCalidadesRecepciones)
                .WithRequired(e => e.MuestreosRecepciones)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OperadoresResiduos>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<OperadoresResiduos>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<OperadoresResiduos>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<OperadoresResiduos>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<OperadoresResiduos>()
                .HasMany(e => e.RegistrosResiduoRecolectado)
                .WithRequired(e => e.OperadoresResiduos)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Pantallas>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Pantallas>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Pantallas>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<Pantallas>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<Pantallas>()
                .HasMany(e => e.Permisos)
                .WithRequired(e => e.Pantallas)
                .HasForeignKey(e => e.id_pantalla)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Permisos>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Permisos>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Permisos>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<Permisos>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<Permisos>()
                .HasMany(e => e.RolesPermisos)
                .WithRequired(e => e.Permisos)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Precios>()
                .Property(e => e.grado_id)
                .IsUnicode(false);

            modelBuilder.Entity<Precios>()
                .Property(e => e.importe)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Precios>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Precios>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<Precios>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<Producciones>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<Producciones>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<Puestos>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Puestos>()
                .Property(e => e.estado)
                .IsUnicode(false);

            modelBuilder.Entity<Puestos>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<Puestos>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<Puestos>()
                .HasMany(e => e.Empleados)
                .WithRequired(e => e.Puestos)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ranchos>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Ranchos>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Ranchos>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<Ranchos>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<Ranchos>()
                .HasMany(e => e.Sectores)
                .WithRequired(e => e.Ranchos)
                .HasForeignKey(e => e.id_rancho)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Recepciones>()
                .Property(e => e.grado_id)
                .IsUnicode(false);

            modelBuilder.Entity<Recepciones>()
                .Property(e => e.folio_reception)
                .IsUnicode(false);

            modelBuilder.Entity<Recepciones>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Recepciones>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<Recepciones>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<Recepciones>()
                .Property(e => e.importe)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Recepciones>()
                .Property(e => e.precio_unitario)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Recepciones>()
                .HasMany(e => e.CalidadesRecepciones)
                .WithRequired(e => e.Recepciones)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Recepciones>()
                .HasMany(e => e.DescuentosRecepciones)
                .WithRequired(e => e.Recepciones)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Recepciones>()
                .HasMany(e => e.MuestreosRecepciones)
                .WithRequired(e => e.Recepciones)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Recepciones>()
                .HasMany(e => e.Tarimas)
                .WithRequired(e => e.Recepciones)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Recepciones>()
                .HasMany(e => e.VehiculosRecepciones)
                .WithRequired(e => e.Recepciones)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RegistrosResiduoRecolectado>()
                .Property(e => e.observaciones)
                .IsUnicode(false);

            modelBuilder.Entity<RegistrosResiduoRecolectado>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<RegistrosResiduoRecolectado>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<RegistrosResiduoRecolectado>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<RegistrosResiduoRecolectado>()
                .HasMany(e => e.DetalleRegistroResiduoRecolectado)
                .WithRequired(e => e.RegistrosResiduoRecolectado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RegistrosResiduoRecolectado>()
                .HasMany(e => e.FoliosRegistroResiduoRecolectado)
                .WithRequired(e => e.RegistrosResiduoRecolectado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Roles>()
                .Property(e => e.rol_id)
                .IsUnicode(false);

            modelBuilder.Entity<Roles>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Roles>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<Roles>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<Roles>()
                .HasMany(e => e.RolesPermisos)
                .WithRequired(e => e.Roles)
                .HasForeignKey(e => e.id_rol)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Roles>()
                .HasMany(e => e.Usuarios)
                .WithRequired(e => e.Roles)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sectores>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Sectores>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<Sectores>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<Sectores>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Sectores>()
                .HasMany(e => e.Recepciones)
                .WithRequired(e => e.Sectores)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tarimas>()
                .Property(e => e.grado_id)
                .IsUnicode(false);

            modelBuilder.Entity<Tarimas>()
                .Property(e => e.importe)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Tarimas>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<Tarimas>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<TipoResiduo>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<TipoResiduo>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<TipoResiduo>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<TipoResiduo>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<TipoResiduo>()
                .HasMany(e => e.DetalleRegistroResiduoRecolectado)
                .WithRequired(e => e.TipoResiduo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TiposDestinoResiduo>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<TiposDestinoResiduo>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<TiposDestinoResiduo>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<TiposDestinoResiduo>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<TiposDestinoResiduo>()
                .HasMany(e => e.DestinosResiduo)
                .WithRequired(e => e.TiposDestinoResiduo)
                .HasForeignKey(e => e.tipo_destino_residuo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TitulosReporteResiduos>()
                .Property(e => e.titulo1)
                .IsUnicode(false);

            modelBuilder.Entity<TitulosReporteResiduos>()
                .Property(e => e.titulo2)
                .IsUnicode(false);

            modelBuilder.Entity<TitulosReporteResiduos>()
                .Property(e => e.titulo3)
                .IsUnicode(false);

            modelBuilder.Entity<TitulosReporteResiduos>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<TitulosReporteResiduos>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<TitulosReporteResiduos>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<UnidadesMedida>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<UnidadesMedida>()
                .Property(e => e.abreviatura)
                .IsUnicode(false);

            modelBuilder.Entity<UnidadesMedida>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<UnidadesMedida>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<UnidadesMedida>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<UnidadesMedida>()
                .HasMany(e => e.Frutas)
                .WithRequired(e => e.UnidadesMedida)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuarios>()
                .Property(e => e.usuario_id)
                .IsUnicode(false);

            modelBuilder.Entity<Usuarios>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<Usuarios>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Usuarios>()
                .Property(e => e.rol_id)
                .IsUnicode(false);

            modelBuilder.Entity<Usuarios>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<Usuarios>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<Variedades>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Variedades>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Variedades>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<Variedades>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<Variedades>()
                .HasMany(e => e.MangoRecepciones)
                .WithRequired(e => e.Variedades)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Variedades>()
                .HasMany(e => e.Sectores)
                .WithRequired(e => e.Variedades)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vehiculos>()
                .Property(e => e.placas)
                .IsUnicode(false);

            modelBuilder.Entity<Vehiculos>()
                .Property(e => e.color)
                .IsUnicode(false);

            modelBuilder.Entity<Vehiculos>()
                .Property(e => e.marca_modelo)
                .IsUnicode(false);

            modelBuilder.Entity<Vehiculos>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Vehiculos>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<Vehiculos>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<Vehiculos>()
                .HasMany(e => e.MangoVehiculoRecepcion)
                .WithRequired(e => e.Vehiculos)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vehiculos>()
                .HasMany(e => e.RegistrosResiduoRecolectado)
                .WithRequired(e => e.Vehiculos)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vehiculos>()
                .HasMany(e => e.VehiculosRecepciones)
                .WithRequired(e => e.Vehiculos)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<VehiculosRecepciones>()
                .Property(e => e.nombre_chofer)
                .IsUnicode(false);

            modelBuilder.Entity<VehiculosRecepciones>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<VehiculosRecepciones>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);

            modelBuilder.Entity<VehiculosRecepciones>()
                .HasMany(e => e.CondicionesVehiculosRecepciones)
                .WithRequired(e => e.VehiculosRecepciones)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RolesPermisos>()
                .Property(e => e.id_rol)
                .IsUnicode(false);

            modelBuilder.Entity<RolesPermisos>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<RolesPermisos>()
                .Property(e => e.usuario_modifica)
                .IsUnicode(false);
        }
    }
}
