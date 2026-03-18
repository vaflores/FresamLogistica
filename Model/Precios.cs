namespace FresamLogistica.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Precios
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int precio_id { get; set; }

        public DateTime fecha_inicio { get; set; }

        public DateTime? fecha_fin { get; set; }

        public int fruta_id { get; set; }

        [Required]
        [StringLength(50)]
        public string grado_id { get; set; }

        [Column(TypeName = "money")]
        public decimal importe { get; set; }

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

        public virtual Frutas Frutas { get; set; }
    }
}
