namespace FresamLogistica.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Recepciones
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Recepciones()
        {
            CalidadesRecepciones = new HashSet<CalidadesRecepciones>();
            DescuentosRecepciones = new HashSet<DescuentosRecepciones>();
            MuestreosRecepciones = new HashSet<MuestreosRecepciones>();
            Tarimas = new HashSet<Tarimas>();
            VehiculosRecepciones = new HashSet<VehiculosRecepciones>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int recepcion_id { get; set; }

        public int ciclo_id { get; set; }

        public DateTime fecha { get; set; }

        public int agricultor_id { get; set; }

        public int sector_id { get; set; }

        public int no_tarimas { get; set; }

        [StringLength(50)]
        public string grado_id { get; set; }

        [Required]
        [StringLength(120)]
        public string folio_reception { get; set; }

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

        [Column(TypeName = "money")]
        public decimal? importe { get; set; }

        [Column(TypeName = "money")]
        public decimal? precio_unitario { get; set; }

        public int? id_folio_diario { get; set; }

        public virtual Agricultores Agricultores { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CalidadesRecepciones> CalidadesRecepciones { get; set; }

        public virtual Ciclos Ciclos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DescuentosRecepciones> DescuentosRecepciones { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MuestreosRecepciones> MuestreosRecepciones { get; set; }

        public virtual Sectores Sectores { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tarimas> Tarimas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VehiculosRecepciones> VehiculosRecepciones { get; set; }
    }
}
