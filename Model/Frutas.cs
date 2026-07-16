namespace FresamLogistica.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Frutas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Frutas()
        {
            GradosFruta = new HashSet<GradosFruta>();
            MangoConfiguracion = new HashSet<MangoConfiguracion>();
            Precios = new HashSet<Precios>();
            Producciones = new HashSet<Producciones>();
            TipoResiduo = new HashSet<TipoResiduo>();
            Variedades = new HashSet<Variedades>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fruta_id { get; set; }

        [Required]
        [StringLength(120)]
        public string nombre { get; set; }

        public int unidad_medida_id { get; set; }

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

        public virtual UnidadesMedida UnidadesMedida { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GradosFruta> GradosFruta { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MangoConfiguracion> MangoConfiguracion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Precios> Precios { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Producciones> Producciones { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TipoResiduo> TipoResiduo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Variedades> Variedades { get; set; }
    }
}
