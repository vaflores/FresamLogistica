namespace FresamLogistica.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MangoMuestreos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MangoMuestreos()
        {
            MangoMuestreosCalidades = new HashSet<MangoMuestreosCalidades>();
        }

        [Key]
        public int muestreo_id { get; set; }

        public int mango_recepcion_id { get; set; }

        [StringLength(500)]
        public string observaciones { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_crea { get; set; }

        public DateTime fecha_crea { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_modifica { get; set; }

        public DateTime fecha_modifica { get; set; }

        public DateTime fecha { get; set; }

        public decimal peso_caja { get; set; }

        public decimal desecho { get; set; }

        public decimal porcentaje { get; set; }

        public bool aceptado { get; set; }

        public bool materia { get; set; }

        public decimal? porcentaje_maduro { get; set; }

        public int? responsable_empleado_id { get; set; }

        public int? verifica_empleado_id { get; set; }

        public virtual Empleados Empleados { get; set; }

        public virtual Empleados Empleados1 { get; set; }

        public virtual MangoRecepciones MangoRecepciones { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MangoMuestreosCalidades> MangoMuestreosCalidades { get; set; }
    }
}
