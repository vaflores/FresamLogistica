namespace FresamLogistica.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MangoProveedores
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MangoProveedores()
        {
            MangoRecepciones = new HashSet<MangoRecepciones>();
        }

        [Key]
        public int proveedor_id { get; set; }

        [Required]
        [StringLength(150)]
        public string nombre { get; set; }

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

        [Required]
        [StringLength(20)]
        public string codigo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MangoRecepciones> MangoRecepciones { get; set; }
    }
}
