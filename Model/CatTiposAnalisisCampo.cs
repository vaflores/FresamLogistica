namespace FresamLogistica.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CatTiposAnalisisCampo")]
    public partial class CatTiposAnalisisCampo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CatTiposAnalisisCampo()
        {
            AgricultoresAnalisisCampoDetalle = new HashSet<AgricultoresAnalisisCampoDetalle>();
            ProveedoresAnalisisCampoDetalle = new HashSet<ProveedoresAnalisisCampoDetalle>();
        }

        [Key]
        public int tipo_analisis_campo_id { get; set; }

        [Required]
        [StringLength(30)]
        public string codigo { get; set; }

        [Required]
        [StringLength(150)]
        public string nombre { get; set; }

        [StringLength(500)]
        public string descripcion { get; set; }

        public decimal ponderacion { get; set; }

        public bool activo { get; set; }

        public DateTime fecha_crea { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_crea { get; set; }

        public DateTime? fecha_modifica { get; set; }

        [StringLength(50)]
        public string usuario_modifica { get; set; }

        public bool? esCampo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AgricultoresAnalisisCampoDetalle> AgricultoresAnalisisCampoDetalle { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProveedoresAnalisisCampoDetalle> ProveedoresAnalisisCampoDetalle { get; set; }
    }
}
