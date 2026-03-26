namespace FresamLogistica.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RegistrosDesperdicioRecolectado")]
    public partial class RegistrosDesperdicioRecolectado
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RegistrosDesperdicioRecolectado()
        {
            DetalleRegistroDesperdicioRecolectado = new HashSet<DetalleRegistroDesperdicioRecolectado>();
        }

        [Key]
        public int registro_desperdicio_id { get; set; }

        public DateTime fecha { get; set; }

        public int proveedor_desperdicio_id { get; set; }

        public int destino_desperdicio_id { get; set; }

        public decimal cantidad_total { get; set; }

        public int? folio_id { get; set; }

        [StringLength(500)]
        public string Observaciones { get; set; }

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

        public virtual DestinosDesperdicio DestinosDesperdicio { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetalleRegistroDesperdicioRecolectado> DetalleRegistroDesperdicioRecolectado { get; set; }

        public virtual Folios Folios { get; set; }

        public virtual ProveedoresDesperdicio ProveedoresDesperdicio { get; set; }
    }
}
