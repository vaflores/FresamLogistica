namespace FresamLogistica.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tarimas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int tarima_id { get; set; }

        public int no_tarima { get; set; }

        public int recepcion_id { get; set; }

        public int cajas { get; set; }

        public decimal peso_bruto { get; set; }

        public decimal tara1 { get; set; }

        public decimal tara2 { get; set; }

        public decimal peso_neto { get; set; }

        [StringLength(50)]
        public string grado_id { get; set; }

        public int calificacion { get; set; }

        [Column(TypeName = "money")]
        public decimal importe { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_crea { get; set; }

        public DateTime fecha_crea { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_modifica { get; set; }

        public DateTime fecha_modifica { get; set; }

        public int? tarima_destajo_id { get; set; }

        public DateTime? fecha_hora_destajo { get; set; }

        public bool? es_venta { get; set; }

        [StringLength(50)]
        public string usuario_aprueba_venta { get; set; }

        public virtual Recepciones Recepciones { get; set; }

        public virtual TarimasDestajos TarimasDestajos { get; set; }
    }
}
