using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace FresamLogistica.Model
{
    public partial class Model : DbContext
    {
        public Model()
            : base("name=Modelo")
        {
        }

        public virtual DbSet<Agricultores> Agricultores { get; set; }
        public virtual DbSet<Calidades> Calidades { get; set; }
        public virtual DbSet<CalidadesRecepciones> CalidadesRecepciones { get; set; }
        public virtual DbSet<CicloCalidades> CicloCalidades { get; set; }
        public virtual DbSet<CicloEtiquetas> CicloEtiquetas { get; set; }
        public virtual DbSet<CicloGrados> CicloGrados { get; set; }
        public virtual DbSet<CicloInspecciones> CicloInspecciones { get; set; }
        public virtual DbSet<Ciclos> Ciclos { get; set; }
        public virtual DbSet<Ciudades> Ciudades { get; set; }
        public virtual DbSet<Condiciones> Condiciones { get; set; }
        public virtual DbSet<CondicionesVehiculosRecepciones> CondicionesVehiculosRecepciones { get; set; }
        public virtual DbSet<DescuentosRecepciones> DescuentosRecepciones { get; set; }
        public virtual DbSet<Empresa> Empresa { get; set; }
        public virtual DbSet<Estados> Estados { get; set; }
        public virtual DbSet<Frutas> Frutas { get; set; }
        public virtual DbSet<GradosFruta> GradosFruta { get; set; }
        public virtual DbSet<Pantallas> Pantallas { get; set; }
        public virtual DbSet<Permisos> Permisos { get; set; }
        public virtual DbSet<Precios> Precios { get; set; }
        public virtual DbSet<Ranchos> Ranchos { get; set; }
        public virtual DbSet<Recepciones> Recepciones { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Sectores> Sectores { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Tarimas> Tarimas { get; set; }
        public virtual DbSet<UnidadesMedida> UnidadesMedida { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
        public virtual DbSet<Variedades> Variedades { get; set; }
        public virtual DbSet<Vehiculos> Vehiculos { get; set; }
        public virtual DbSet<VehiculosRecepciones> VehiculosRecepciones { get; set; }
        public virtual DbSet<RolesPermisos> RolesPermisos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
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
                .HasMany(e => e.Recepciones)
                .WithRequired(e => e.Ciclos)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ciclos>()
                .HasMany(e => e.Sectores)
                .WithRequired(e => e.Ciclos)
                .HasForeignKey(e => e.id_ciclo)
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

            modelBuilder.Entity<CondicionesVehiculosRecepciones>()
                .Property(e => e.usuario_crea)
                .IsUnicode(false);

            modelBuilder.Entity<CondicionesVehiculosRecepciones>()
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
                .HasMany(e => e.Tarimas)
                .WithRequired(e => e.Recepciones)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Recepciones>()
                .HasMany(e => e.VehiculosRecepciones)
                .WithRequired(e => e.Recepciones)
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
