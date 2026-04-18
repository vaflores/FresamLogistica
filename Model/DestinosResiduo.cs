namespace FresamLogistica.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DestinosResiduo")]
    public partial class DestinosResiduo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DestinosResiduo()
        {
            LoteFolios = new HashSet<LoteFolios>();
            RegistrosResiduoRecolectado = new HashSet<RegistrosResiduoRecolectado>();
        }

        [Key]
        public int destino_residuo_id { get; set; }

        [Required]
        [StringLength(250)]
        public string nombre { get; set; }

        public int tipo_destino_residuo { get; set; }

        public int ciudad_id { get; set; }

        public bool requiere_folio { get; set; }

        [Required]
        [StringLength(1)]
        public string estado { get; set; }

        [StringLength(50)]
        public string usuario_crea { get; set; }

        public DateTime? fecha_crea { get; set; }

        [StringLength(50)]
        public string usuario_modifica { get; set; }

        public DateTime? fecha_modifica { get; set; }

        public virtual Ciudades Ciudades { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LoteFolios> LoteFolios { get; set; }

        public virtual TiposDestinoResiduo TiposDestinoResiduo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RegistrosResiduoRecolectado> RegistrosResiduoRecolectado { get; set; }
    }
}
