namespace FresamLogistica.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TipoDesperdicio")]
    public partial class TipoDesperdicio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TipoDesperdicio()
        {
            DetalleRegistroDesperdicioRecolectado = new HashSet<DetalleRegistroDesperdicioRecolectado>();
        }

        [Key]
        public int tipo_desperdicio_id { get; set; }

        [Required]
        [StringLength(120)]
        public string nombre { get; set; }

        public int fruta_id { get; set; }

        public bool estado { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_crea { get; set; }

        public DateTime fecha_crea { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_modifica { get; set; }

        public DateTime fecha_modifica { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetalleRegistroDesperdicioRecolectado> DetalleRegistroDesperdicioRecolectado { get; set; }

        public virtual Frutas Frutas { get; set; }
    }
}
