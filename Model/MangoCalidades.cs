namespace FresamLogistica.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MangoCalidades
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MangoCalidades()
        {
            MangoCalidadesRecepcion = new HashSet<MangoCalidadesRecepcion>();
            MangoMuestreosCalidades = new HashSet<MangoMuestreosCalidades>();
        }

        [Key]
        public int calidad_id { get; set; }

        [Required]
        [StringLength(100)]
        public string nombre { get; set; }

        public int orden_captura { get; set; }

        public bool afecta_calificacion { get; set; }

        public bool visible_reporte { get; set; }

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
        public virtual ICollection<MangoCalidadesRecepcion> MangoCalidadesRecepcion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MangoMuestreosCalidades> MangoMuestreosCalidades { get; set; }
    }
}
