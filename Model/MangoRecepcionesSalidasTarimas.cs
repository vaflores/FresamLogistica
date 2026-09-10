namespace FresamLogistica.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MangoRecepcionesSalidasTarimas
    {
        [Key]
        public int mango_recepcion_salida_tarima_id { get; set; }

        public int mango_recepcion_salida_id { get; set; }

        public int numero_tarima { get; set; }

        public decimal cantidad_cajas { get; set; }

        public decimal peso_neto { get; set; }

        public DateTime fecha_crea { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_crea { get; set; }

        public DateTime? fecha_modifica { get; set; }

        [StringLength(50)]
        public string usuario_modifica { get; set; }

        public bool? aprobada { get; set; }

        public DateTime? fecha_aprobacion { get; set; }

        [StringLength(50)]
        public string usuario_aprobacion { get; set; }

        public bool? cancelada { get; set; }

        public DateTime? fecha_cancela { get; set; }

        [StringLength(50)]
        public string usuario_cancela { get; set; }

        public int? mango_destajo_id { get; set; }

        public DateTime? fecha_hora_destajo { get; set; }

        public virtual MangoDestajos MangoDestajos { get; set; }

        public virtual MangoRecepcionesSalidas MangoRecepcionesSalidas { get; set; }
    }
}
