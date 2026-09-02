namespace FresamLogistica.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MangoDestajoDetalleTarimas
    {
        [Key]
        public int mango_destajo_detalle_tarima_id { get; set; }

        public int mango_destajo_id { get; set; }

        public int mango_recepcion_salida_tarima_id { get; set; }

        public DateTime fecha_hora_registro { get; set; }

        public DateTime fecha_crea { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_crea { get; set; }

        public DateTime? fecha_modifica { get; set; }

        [StringLength(50)]
        public string usuario_modifica { get; set; }

        public virtual MangoDestajo MangoDestajo { get; set; }

        public virtual MangoRecepcionesSalidasTarimas MangoRecepcionesSalidasTarimas { get; set; }
    }
}
