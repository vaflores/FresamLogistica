namespace FresamLogistica.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class KardexCajas
    {
        [Key]
        public int kardex_id { get; set; }

        public int agricultor_id { get; set; }

        public DateTime fecha { get; set; }

        [Required]
        [StringLength(20)]
        public string tipo_movimiento { get; set; }

        public decimal entrada { get; set; }

        public decimal salida { get; set; }

        public decimal saldo_anterior { get; set; }

        public decimal saldo_actual { get; set; }

        [StringLength(200)]
        public string observaciones { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_crea { get; set; }

        public DateTime fecha_crea { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_modifica { get; set; }

        public DateTime fecha_modifica { get; set; }

        public virtual Agricultores Agricultores { get; set; }
    }
}
