namespace FresamLogistica.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DetalleRegistroDesperdicioRecolectado")]
    public partial class DetalleRegistroDesperdicioRecolectado
    {
        [Key]
        public int detalle_registro_desperdicio_id { get; set; }

        public int registro_desperdicio_id { get; set; }

        public int tipo_desperdicio_id { get; set; }

        public decimal cantidad { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_crea { get; set; }

        public DateTime fecha_crea { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_modifica { get; set; }

        public DateTime fecha_modifica { get; set; }

        public virtual RegistrosDesperdicioRecolectado RegistrosDesperdicioRecolectado { get; set; }

        public virtual TipoDesperdicio TipoDesperdicio { get; set; }
    }
}
