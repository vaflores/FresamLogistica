namespace FresamLogistica.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MuestreosRecepciones
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MuestreosRecepciones()
        {
            MuestreosCalidadesRecepciones = new HashSet<MuestreosCalidadesRecepciones>();
        }

        [Key]
        public int muestreo_id { get; set; }

        public int recepcion_id { get; set; }

        public DateTime fecha { get; set; }

        public decimal peso_caja { get; set; }

        public decimal desecho { get; set; }

        public decimal porcentaje { get; set; }

        public bool aceptado { get; set; }

        public bool materia { get; set; }

        [StringLength(1024)]
        public string observaciones { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_crea { get; set; }

        public DateTime fecha_crea { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_modifica { get; set; }

        public DateTime fecha_modifica { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MuestreosCalidadesRecepciones> MuestreosCalidadesRecepciones { get; set; }

        public virtual Recepciones Recepciones { get; set; }
    }
}
