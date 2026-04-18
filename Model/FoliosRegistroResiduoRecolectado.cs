namespace FresamLogistica.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FoliosRegistroResiduoRecolectado")]
    public partial class FoliosRegistroResiduoRecolectado
    {
        [Key]
        public int folios_registro_residuo_id { get; set; }

        public int registro_residuo_id { get; set; }

        public int folio_id { get; set; }

        [StringLength(50)]
        public string usuario_crea { get; set; }

        public DateTime? fecha_crea { get; set; }

        [StringLength(50)]
        public string usuario_modifica { get; set; }

        public DateTime? fecha_modifica { get; set; }

        public virtual Folios Folios { get; set; }

        public virtual RegistrosResiduoRecolectado RegistrosResiduoRecolectado { get; set; }
    }
}
