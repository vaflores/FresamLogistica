namespace FresamLogistica.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MangoTiposSalidas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MangoTiposSalidas()
        {
            MangoRecepcionesSalidasDetalle = new HashSet<MangoRecepcionesSalidasDetalle>();
        }

        [Key]
        public int mango_tipo_salida_id { get; set; }

        [Required]
        [StringLength(100)]
        public string nombre { get; set; }

        [StringLength(300)]
        public string descripcion { get; set; }

        public bool afecta_existencia { get; set; }

        public int orden { get; set; }

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MangoRecepcionesSalidasDetalle> MangoRecepcionesSalidasDetalle { get; set; }
    }
}
