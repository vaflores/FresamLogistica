namespace FresamLogistica.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MangoRecepcionesSalidasTarimas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MangoRecepcionesSalidasTarimas()
        {
            MangoDestajoDetalleTarimas = new HashSet<MangoDestajoDetalleTarimas>();
        }

        [Key]
        public int mango_recepcion_salida_tarima_id { get; set; }

        public int mango_recepcion_salida_id { get; set; }

        public int numero_tarima { get; set; }

        public decimal cantidad_cajas { get; set; }

        public decimal peso_neto { get; set; }

        public DateTime fecha_crea { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_crea { get; set; }

        public DateTime? fecha_modifica { get; set; }

        [StringLength(50)]
        public string usuario_modifica { get; set; }

        public bool? aprobada { get; set; }

        public DateTime? fecha_aprobacion { get; set; }

        [StringLength(50)]
        public string usuario_aprobacion { get; set; }

        public bool? cancelada { get; set; }

        public DateTime? fecha_cancela { get; set; }

        [StringLength(50)]
        public string usuario_cancela { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MangoDestajoDetalleTarimas> MangoDestajoDetalleTarimas { get; set; }

        public virtual MangoRecepcionesSalidas MangoRecepcionesSalidas { get; set; }
    }
}
