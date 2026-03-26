namespace FresamLogistica.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProveedoresDesperdicio")]
    public partial class ProveedoresDesperdicio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProveedoresDesperdicio()
        {
            RegistrosDesperdicioRecolectado = new HashSet<RegistrosDesperdicioRecolectado>();
        }

        [Key]
        public int proveedor_desperdicio_id { get; set; }

        [Required]
        [StringLength(240)]
        public string nombre { get; set; }

        [StringLength(250)]
        public string direccion { get; set; }

        [StringLength(20)]
        public string telefono { get; set; }

        [StringLength(250)]
        public string email { get; set; }

        [StringLength(100)]
        public string licenciaSCT { get; set; }

        [StringLength(100)]
        public string licenciaEcologica { get; set; }

        public bool estado { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_crea { get; set; }

        public DateTime fecha_crea { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_modifica { get; set; }

        public DateTime fecha_modifica { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RegistrosDesperdicioRecolectado> RegistrosDesperdicioRecolectado { get; set; }
    }
}
