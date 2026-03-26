namespace FresamLogistica.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TiposDestinoDesperdicio")]
    public partial class TiposDestinoDesperdicio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TiposDestinoDesperdicio()
        {
            DestinosDesperdicio = new HashSet<DestinosDesperdicio>();
        }

        [Key]
        public int tipo_destino_desperdicio_id { get; set; }

        [Required]
        [StringLength(250)]
        public string nombre { get; set; }

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
        public virtual ICollection<DestinosDesperdicio> DestinosDesperdicio { get; set; }
    }
}
