namespace FresamLogistica.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class KardexCondicionesCajas
    {
        [Key]
        public int kardex_condicion_caja_id { get; set; }

        public int kardex_id { get; set; }

        public int condicion_caja_id { get; set; }

        public int valor { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_crea { get; set; }

        public DateTime fecha_crea { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_modifica { get; set; }

        public DateTime fecha_modifica { get; set; }

        public virtual CondicionesCajas CondicionesCajas { get; set; }

        public virtual KardexCajas KardexCajas { get; set; }
    }
}
