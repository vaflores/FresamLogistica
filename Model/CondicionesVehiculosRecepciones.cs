namespace FresamLogistica.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CondicionesVehiculosRecepciones
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int condicion_vehiculo_recepcion_id { get; set; }

        public int vehiculo_recepcion_id { get; set; }

        public int condicion_id { get; set; }

        public bool condicion_estado { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_crea { get; set; }

        public DateTime fecha_crea { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_modifica { get; set; }

        public DateTime fecha_modifica { get; set; }

        public virtual Condiciones Condiciones { get; set; }

        public virtual VehiculosRecepciones VehiculosRecepciones { get; set; }
    }
}
