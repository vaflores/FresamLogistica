namespace FresamLogistica.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Empresa")]
    public partial class Empresa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int empresa_id { get; set; }

        [Column(TypeName = "image")]
        [Required]
        public byte[] logo { get; set; }

        [Required]
        [StringLength(250)]
        public string nombre { get; set; }

        [Required]
        [StringLength(250)]
        public string calle { get; set; }

        [Required]
        [StringLength(250)]
        public string colonia { get; set; }

        [Required]
        [StringLength(250)]
        public string ciudad { get; set; }

        [Required]
        [StringLength(250)]
        public string estado { get; set; }

        [StringLength(20)]
        public string rfc { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_crea { get; set; }

        public DateTime fecha_crea { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_modifica { get; set; }

        public DateTime fecha_modifica { get; set; }
    }
}
