namespace FresamLogistica.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TiposDestinoResiduo")]
    public partial class TiposDestinoResiduo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TiposDestinoResiduo()
        {
            DestinosResiduo = new HashSet<DestinosResiduo>();
        }

        [Key]
        public int tipo_destino_residuo_id { get; set; }

        [Required]
        [StringLength(250)]
        public string nombre { get; set; }

        [Required]
        [StringLength(1)]
        public string estado { get; set; }

        [StringLength(50)]
        public string usuario_crea { get; set; }

        public DateTime? fecha_crea { get; set; }

        [StringLength(50)]
        public string usuario_modifica { get; set; }

        public DateTime? fecha_modifica { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DestinosResiduo> DestinosResiduo { get; set; }
    }
}
