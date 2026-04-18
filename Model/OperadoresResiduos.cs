namespace FresamLogistica.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OperadoresResiduos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OperadoresResiduos()
        {
            RegistrosResiduoRecolectado = new HashSet<RegistrosResiduoRecolectado>();
        }

        [Key]
        public int operador_residuo_id { get; set; }

        public int empresa_gestora_id { get; set; }

        [Required]
        [StringLength(250)]
        public string nombre { get; set; }

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

        public virtual EmpresaGestoraDeResiduos EmpresaGestoraDeResiduos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RegistrosResiduoRecolectado> RegistrosResiduoRecolectado { get; set; }
    }
}
