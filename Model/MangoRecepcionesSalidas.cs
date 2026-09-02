namespace FresamLogistica.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MangoRecepcionesSalidas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MangoRecepcionesSalidas()
        {
            MangoRecepcionesSalidasDetalle = new HashSet<MangoRecepcionesSalidasDetalle>();
            MangoRecepcionesSalidasTarimas = new HashSet<MangoRecepcionesSalidasTarimas>();
        }

        [Key]
        public int mango_recepcion_salida_id { get; set; }

        public int mango_recepcion_id { get; set; }

        public int consecutivo { get; set; }

        public DateTime fecha { get; set; }

        public decimal peso_salida { get; set; }

        public decimal saldo_restante { get; set; }

        [StringLength(500)]
        public string observaciones { get; set; }

        [Required]
        [StringLength(1)]
        public string estado { get; set; }

        public DateTime fecha_crea { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_crea { get; set; }

        public DateTime? fecha_modifica { get; set; }

        [StringLength(50)]
        public string usuario_modifica { get; set; }

        public virtual MangoRecepciones MangoRecepciones { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MangoRecepcionesSalidasDetalle> MangoRecepcionesSalidasDetalle { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MangoRecepcionesSalidasTarimas> MangoRecepcionesSalidasTarimas { get; set; }
    }
}
