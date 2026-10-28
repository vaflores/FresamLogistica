namespace FresamLogistica.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CicloCalificaciones
    {
        [Key]
        public int ciclo_calificacion_id { get; set; }

        [StringLength(250)]
        public string titulo1 { get; set; }

        [StringLength(250)]
        public string titulo2 { get; set; }

        [StringLength(250)]
        public string titulo3 { get; set; }

        [Required]
        [StringLength(1)]
        public string estado { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_crea { get; set; }

        public DateTime fecha_crea { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_modifica { get; set; }

        public DateTime fecha_modifica { get; set; }

        public int ciclo_id { get; set; }

        public virtual Ciclos Ciclos { get; set; }
    }
}
