namespace FresamLogistica.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AgricultorCajasSaldo")]
    public partial class AgricultorCajasSaldo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int agricultor_id { get; set; }

        public decimal total_cajas { get; set; }

        public DateTime fecha_actualizacion { get; set; }

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
