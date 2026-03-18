namespace FresamLogistica.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Agricultores
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Agricultores()
        {
            Ranchos = new HashSet<Ranchos>();
            Recepciones = new HashSet<Recepciones>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int agricultor_id { get; set; }

        [Required]
        [StringLength(250)]
        public string nombre { get; set; }

        [StringLength(20)]
        public string rfc { get; set; }

        [Required]
        [StringLength(120)]
        public string calle { get; set; }

        [Required]
        [StringLength(120)]
        public string colonia { get; set; }

        public int estado_id { get; set; }

        public int ciudad_id { get; set; }

        public int zip_code { get; set; }

        [StringLength(50)]
        public string telefono1 { get; set; }

        [StringLength(50)]
        public string telefono2 { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_crea { get; set; }

        public DateTime fecha_crea { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_modifica { get; set; }

        public DateTime fecha_modifica { get; set; }

        [Required]
        [StringLength(1)]
        public string estado { get; set; }

        public virtual Ciudades Ciudades { get; set; }

        public virtual Estados Estados { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ranchos> Ranchos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Recepciones> Recepciones { get; set; }
    }
}
