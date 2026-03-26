namespace FresamLogistica.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Folios
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Folios()
        {
            RegistrosDesperdicioRecolectado = new HashSet<RegistrosDesperdicioRecolectado>();
        }

        [Key]
        public int folio_id { get; set; }

        public int numero { get; set; }

        public int lote_folio_id { get; set; }

        public bool utilizado { get; set; }

        public DateTime? fecha_uso { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_crea { get; set; }

        public DateTime fecha_crea { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_modifica { get; set; }

        public DateTime fecha_modifica { get; set; }

        public virtual LoteFolios LoteFolios { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RegistrosDesperdicioRecolectado> RegistrosDesperdicioRecolectado { get; set; }
    }
}
