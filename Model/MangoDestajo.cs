namespace FresamLogistica.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MangoDestajo")]
    public partial class MangoDestajo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MangoDestajo()
        {
            MangoDestajoDetalleTarimas = new HashSet<MangoDestajoDetalleTarimas>();
        }

        [Key]
        public int mango_destajo_id { get; set; }

        public DateTime fecha_hora_inicio { get; set; }

        public DateTime? fecha_hora_termino { get; set; }

        public decimal peso_neto { get; set; }

        public decimal total_cajas { get; set; }

        public int empleado_responsable_id { get; set; }

        public int? empleado_verifico_id { get; set; }

        [Required]
        [StringLength(1)]
        public string estado { get; set; }

        public DateTime fecha_crea { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_crea { get; set; }

        public DateTime? fecha_modifica { get; set; }

        [StringLength(50)]
        public string usuario_modifica { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MangoDestajoDetalleTarimas> MangoDestajoDetalleTarimas { get; set; }
    }
}
