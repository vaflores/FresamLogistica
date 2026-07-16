namespace FresamLogistica.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MangoDescuentos
    {
        [Key]
        public int descuento_id { get; set; }

        public int mango_recepcion_id { get; set; }

        public decimal peso_caja { get; set; }

        public decimal desecho { get; set; }

        public decimal porcentaje { get; set; }

        public bool aceptado { get; set; }

        public bool materia { get; set; }

        [StringLength(1024)]
        public string observaciones { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_crea { get; set; }

        public DateTime fecha_crea { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_modifica { get; set; }

        public DateTime fecha_modifica { get; set; }

        public virtual MangoRecepciones MangoRecepciones { get; set; }
    }
}
