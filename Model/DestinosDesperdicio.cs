namespace FresamLogistica.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DestinosDesperdicio")]
    public partial class DestinosDesperdicio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DestinosDesperdicio()
        {
            RegistrosDesperdicioRecolectado = new HashSet<RegistrosDesperdicioRecolectado>();
        }

        [Key]
        public int destino_desperdicio_id { get; set; }

        [Required]
        [StringLength(250)]
        public string nombre { get; set; }

        public int tipo_destino_desperdicio { get; set; }

        public int ciudad_id { get; set; }

        public bool requiere_folio { get; set; }

        public bool estado { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_crea { get; set; }

        public DateTime fecha_crea { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_modifica { get; set; }

        public DateTime fecha_modifica { get; set; }

        public virtual Ciudades Ciudades { get; set; }

        public virtual TiposDestinoDesperdicio TiposDestinoDesperdicio { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RegistrosDesperdicioRecolectado> RegistrosDesperdicioRecolectado { get; set; }
    }
}
