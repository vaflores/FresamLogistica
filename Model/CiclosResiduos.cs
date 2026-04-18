namespace FresamLogistica.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CiclosResiduos
    {
        [Key]
        public int ciclo_residuos_id { get; set; }

        [Column(TypeName = "date")]
        public DateTime fecha_inicio { get; set; }

        [Column(TypeName = "date")]
        public DateTime? fecha_fin { get; set; }

        [StringLength(50)]
        public string proxima_revision { get; set; }

        [StringLength(50)]
        public string edicion { get; set; }

        [StringLength(50)]
        public string fecha_emision { get; set; }

        [Required]
        [StringLength(1)]
        public string estado { get; set; }

        [StringLength(50)]
        public string usuario_crea { get; set; }

        public DateTime? fecha_crea { get; set; }

        [StringLength(50)]
        public string usuario_modifica { get; set; }

        public DateTime? fecha_modifica { get; set; }
    }
}
