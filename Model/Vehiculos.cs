namespace FresamLogistica.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Vehiculos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vehiculos()
        {
            RegistrosResiduoRecolectado = new HashSet<RegistrosResiduoRecolectado>();
            VehiculosRecepciones = new HashSet<VehiculosRecepciones>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int vehiculo_id { get; set; }

        [Required]
        [StringLength(50)]
        public string placas { get; set; }

        [Required]
        [StringLength(50)]
        public string color { get; set; }

        [Required]
        [StringLength(50)]
        public string marca_modelo { get; set; }

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
        public virtual ICollection<RegistrosResiduoRecolectado> RegistrosResiduoRecolectado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VehiculosRecepciones> VehiculosRecepciones { get; set; }
    }
}
