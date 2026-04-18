namespace FresamLogistica.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EmpresaGestoraDeResiduos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EmpresaGestoraDeResiduos()
        {
            OperadoresResiduos = new HashSet<OperadoresResiduos>();
        }

        [Key]
        public int empresa_gestora_id { get; set; }

        [Required]
        [StringLength(250)]
        public string nombre { get; set; }

        public bool es_persona_moral { get; set; }

        [StringLength(20)]
        public string RFC { get; set; }

        [StringLength(20)]
        public string telefono { get; set; }

        [StringLength(100)]
        public string licenciaSCT { get; set; }

        [StringLength(100)]
        public string licenciaEcologica { get; set; }

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OperadoresResiduos> OperadoresResiduos { get; set; }
    }
}
