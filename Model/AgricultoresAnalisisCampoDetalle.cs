namespace FresamLogistica.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AgricultoresAnalisisCampoDetalle")]
    public partial class AgricultoresAnalisisCampoDetalle
    {
        [Key]
        public int agricultor_analisis_campo_detalle_id { get; set; }

        public int agricultor_analisis_campo_id { get; set; }

        public int tipo_analisis_campo_id { get; set; }

        public bool cumple { get; set; }

        public DateTime fecha_crea { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_crea { get; set; }

        public DateTime? fecha_modifica { get; set; }

        [StringLength(50)]
        public string usuario_modifica { get; set; }

        public virtual AgricultoresAnalisisCampo AgricultoresAnalisisCampo { get; set; }

        public virtual CatTiposAnalisisCampo CatTiposAnalisisCampo { get; set; }
    }
}
