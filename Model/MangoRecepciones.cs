namespace FresamLogistica.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MangoRecepciones
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MangoRecepciones()
        {
            MangoCalidadesRecepcion = new HashSet<MangoCalidadesRecepcion>();
            MangoDescuentos = new HashSet<MangoDescuentos>();
            MangoMuestreos = new HashSet<MangoMuestreos>();
            MangoVehiculoRecepcion = new HashSet<MangoVehiculoRecepcion>();
        }

        [Key]
        public int mango_recepcion_id { get; set; }

        public int ciclo_id { get; set; }

        public int folio_ciclo { get; set; }

        public int folio_proveedor { get; set; }

        public DateTime fecha { get; set; }

        public int proveedor_id { get; set; }

        public int predio_id { get; set; }

        public int variedad_id { get; set; }

        [Required]
        [StringLength(250)]
        public string guia_sanitaria { get; set; }

        [StringLength(100)]
        public string ticket_bascula { get; set; }

        public decimal peso_bruto { get; set; }

        public decimal tara { get; set; }

        public decimal peso_neto { get; set; }

        public decimal? calidad_promedio { get; set; }

        [StringLength(50)]
        public string grado_id { get; set; }

        [Column(TypeName = "money")]
        public decimal? importe { get; set; }

        [Column(TypeName = "money")]
        public decimal? precio_unitario { get; set; }

        [Required]
        [StringLength(1)]
        public string estado { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_crea { get; set; }

        public DateTime fecha_crea { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_modifica { get; set; }

        public DateTime fecha_modifica { get; set; }

        public decimal? porcentaje_costo { get; set; }

        public bool porcentaje_costo_afecta_peso { get; set; }

        public virtual Ciclos Ciclos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MangoCalidadesRecepcion> MangoCalidadesRecepcion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MangoDescuentos> MangoDescuentos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MangoMuestreos> MangoMuestreos { get; set; }

        public virtual MangoPredios MangoPredios { get; set; }

        public virtual MangoProveedores MangoProveedores { get; set; }

        public virtual Variedades Variedades { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MangoVehiculoRecepcion> MangoVehiculoRecepcion { get; set; }
    }
}
