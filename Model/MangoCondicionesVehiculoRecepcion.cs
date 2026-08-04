namespace FresamLogistica.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MangoCondicionesVehiculoRecepcion")]
    public partial class MangoCondicionesVehiculoRecepcion
    {
        [Key]
        public int condicion_vehiculo_recepcion_id { get; set; }

        public int mango_vehiculo_recepcion_id { get; set; }

        public int condicion_id { get; set; }

        public bool valor { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_crea { get; set; }

        public DateTime fecha_crea { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_modifica { get; set; }

        public DateTime fecha_modifica { get; set; }

        public virtual Condiciones Condiciones { get; set; }

        public virtual MangoVehiculoRecepcion MangoVehiculoRecepcion { get; set; }
    }
}
