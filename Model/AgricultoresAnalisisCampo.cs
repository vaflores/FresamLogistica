namespace FresamLogistica.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AgricultoresAnalisisCampo")]
    public partial class AgricultoresAnalisisCampo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AgricultoresAnalisisCampo()
        {
            AgricultoresAnalisisCampoDetalle = new HashSet<AgricultoresAnalisisCampoDetalle>();
            AgricultoresAnalisisCampoDocumentos = new HashSet<AgricultoresAnalisisCampoDocumentos>();
        }

        [Key]
        public int agricultor_analisis_campo_id { get; set; }

        public int agricultor_id { get; set; }

        [Column(TypeName = "date")]
        public DateTime fecha_evaluacion { get; set; }

        [Required]
        [StringLength(1)]
        public string estado { get; set; }

        [StringLength(1000)]
        public string observaciones { get; set; }

        public DateTime fecha_crea { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_crea { get; set; }

        public DateTime? fecha_modifica { get; set; }

        [StringLength(50)]
        public string usuario_modifica { get; set; }

        public virtual Agricultores Agricultores { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AgricultoresAnalisisCampoDetalle> AgricultoresAnalisisCampoDetalle { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AgricultoresAnalisisCampoDocumentos> AgricultoresAnalisisCampoDocumentos { get; set; }
    }
}
