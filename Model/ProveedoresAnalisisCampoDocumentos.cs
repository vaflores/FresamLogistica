namespace FresamLogistica.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProveedoresAnalisisCampoDocumentos
    {
        [Key]
        public int proveedor_analisis_campo_documento_id { get; set; }

        public int proveedor_analisis_campo_id { get; set; }

        [Required]
        [StringLength(255)]
        public string nombre_archivo { get; set; }

        [Required]
        public byte[] contenido { get; set; }

        public long? tamano_bytes { get; set; }

        public DateTime fecha_crea { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_crea { get; set; }

        public DateTime? fecha_modifica { get; set; }

        [StringLength(50)]
        public string usuario_modifica { get; set; }

        public virtual ProveedoresAnalisisCampo ProveedoresAnalisisCampo { get; set; }
    }
}
