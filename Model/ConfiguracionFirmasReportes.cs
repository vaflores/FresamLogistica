namespace FresamLogistica.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ConfiguracionFirmasReportes
    {
        [Key]
        public int configuracion_firma_id { get; set; }

        [Required]
        [StringLength(100)]
        public string reporte_clave { get; set; }

        public int empleado_id { get; set; }

        [Required]
        [StringLength(100)]
        public string etiqueta_firma { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_crea { get; set; }

        public DateTime fecha_crea { get; set; }

        [StringLength(50)]
        public string usuario_modifica { get; set; }

        public DateTime? fecha_modifica { get; set; }

        public virtual Empleados Empleados { get; set; }
    }
}
