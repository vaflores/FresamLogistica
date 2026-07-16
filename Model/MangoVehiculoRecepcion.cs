namespace FresamLogistica.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MangoVehiculoRecepcion")]
    public partial class MangoVehiculoRecepcion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MangoVehiculoRecepcion()
        {
            MangoCondicionesVehiculoRecepcion = new HashSet<MangoCondicionesVehiculoRecepcion>();
        }

        [Key]
        public int mango_vehiculo_recepcion_id { get; set; }

        public int mango_recepcion_id { get; set; }

        public int vehiculo_id { get; set; }

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
        public virtual ICollection<MangoCondicionesVehiculoRecepcion> MangoCondicionesVehiculoRecepcion { get; set; }

        public virtual MangoRecepciones MangoRecepciones { get; set; }

        public virtual Vehiculos Vehiculos { get; set; }
    }
}
