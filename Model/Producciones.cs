namespace FresamLogistica.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Producciones
    {
        [Key]
        public int produccion_id { get; set; }

        [Column(TypeName = "date")]
        public DateTime fecha { get; set; }

        public int fruta_id { get; set; }

        public decimal cantidad { get; set; }

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
