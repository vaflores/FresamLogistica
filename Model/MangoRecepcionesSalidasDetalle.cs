namespace FresamLogistica.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MangoRecepcionesSalidasDetalle")]
    public partial class MangoRecepcionesSalidasDetalle
    {
        [Key]
        public int mango_recepcion_salida_detalle_id { get; set; }

        public int mango_recepcion_salida_id { get; set; }

        public int mango_tipo_salida_id { get; set; }

        public decimal peso { get; set; }

        [StringLength(300)]
        public string observaciones { get; set; }

        public DateTime fecha_crea { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_crea { get; set; }

        public DateTime? fecha_modifica { get; set; }

        [StringLength(50)]
        public string usuario_modifica { get; set; }

        public virtual MangoRecepcionesSalidas MangoRecepcionesSalidas { get; set; }

        public virtual MangoTiposSalidas MangoTiposSalidas { get; set; }
    }
}
