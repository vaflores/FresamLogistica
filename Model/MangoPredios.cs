namespace FresamLogistica.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MangoPredios
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MangoPredios()
        {
            MangoRecepciones = new HashSet<MangoRecepciones>();
        }

        [Key]
        public int predio_id { get; set; }

        public int propietario_id { get; set; }

        public int localidad_id { get; set; }

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

        public virtual MangoLocalidades MangoLocalidades { get; set; }

        public virtual MangoPropietarios MangoPropietarios { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MangoRecepciones> MangoRecepciones { get; set; }
    }
}
