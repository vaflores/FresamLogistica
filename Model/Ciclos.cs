namespace FresamLogistica.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Ciclos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ciclos()
        {
            CicloCalidades = new HashSet<CicloCalidades>();
            CicloEtiquetas = new HashSet<CicloEtiquetas>();
            CicloGrados = new HashSet<CicloGrados>();
            CicloInspecciones = new HashSet<CicloInspecciones>();
            Recepciones = new HashSet<Recepciones>();
            Sectores = new HashSet<Sectores>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ciclo_id { get; set; }

        [Required]
        [StringLength(1)]
        public string estado { get; set; }

        public DateTime fecha_inicio { get; set; }

        public DateTime? fecha_fin { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_crea { get; set; }

        public DateTime fecha_crea { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_modifica { get; set; }

        public DateTime fecha_modifica { get; set; }

        [StringLength(120)]
        public string proxima_revision { get; set; }

        [StringLength(120)]
        public string fecha_emision { get; set; }

        [StringLength(16)]
        public string edicion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CicloCalidades> CicloCalidades { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CicloEtiquetas> CicloEtiquetas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CicloGrados> CicloGrados { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CicloInspecciones> CicloInspecciones { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Recepciones> Recepciones { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sectores> Sectores { get; set; }
    }
}
