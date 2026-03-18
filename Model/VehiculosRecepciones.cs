namespace FresamLogistica.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class VehiculosRecepciones
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VehiculosRecepciones()
        {
            CondicionesVehiculosRecepciones = new HashSet<CondicionesVehiculosRecepciones>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int vehiculo_recepcion_id { get; set; }

        public int recepcion_id { get; set; }

        public int vehiculo_id { get; set; }

        [Required]
        [StringLength(250)]
        public string nombre_chofer { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_crea { get; set; }

        public DateTime fecha_crea { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_modifica { get; set; }

        public DateTime fecha_modifica { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CondicionesVehiculosRecepciones> CondicionesVehiculosRecepciones { get; set; }

        public virtual Recepciones Recepciones { get; set; }

        public virtual Vehiculos Vehiculos { get; set; }
    }
}
