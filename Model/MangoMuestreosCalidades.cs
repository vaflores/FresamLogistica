namespace FresamLogistica.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MangoMuestreosCalidades
    {
        [Key]
        public int detalle_id { get; set; }

        public int muestreo_id { get; set; }

        public int calidad_id { get; set; }

        public int valor { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_crea { get; set; }

        public DateTime fecha_crea { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_modifica { get; set; }

        public DateTime fecha_modifica { get; set; }

        public virtual MangoCalidades MangoCalidades { get; set; }

        public virtual MangoMuestreos MangoMuestreos { get; set; }
    }
}
