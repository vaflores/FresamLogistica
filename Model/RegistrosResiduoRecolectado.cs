namespace FresamLogistica.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RegistrosResiduoRecolectado")]
    public partial class RegistrosResiduoRecolectado
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RegistrosResiduoRecolectado()
        {
            DetalleRegistroResiduoRecolectado = new HashSet<DetalleRegistroResiduoRecolectado>();
            FoliosRegistroResiduoRecolectado = new HashSet<FoliosRegistroResiduoRecolectado>();
        }

        [Key]
        public int registro_residuo_id { get; set; }

        public DateTime fecha { get; set; }

        public int operador_residuo_id { get; set; }

        public int destino_residuo_id { get; set; }

        public int vehiculo_id { get; set; }

        public decimal cantidad_total { get; set; }

        [StringLength(500)]
        public string observaciones { get; set; }

        [Required]
        [StringLength(1)]
        public string estado { get; set; }

        [StringLength(50)]
        public string usuario_crea { get; set; }

        public DateTime? fecha_crea { get; set; }

        [StringLength(50)]
        public string usuario_modifica { get; set; }

        public DateTime? fecha_modifica { get; set; }

        public virtual DestinosResiduo DestinosResiduo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetalleRegistroResiduoRecolectado> DetalleRegistroResiduoRecolectado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FoliosRegistroResiduoRecolectado> FoliosRegistroResiduoRecolectado { get; set; }

        public virtual OperadoresResiduos OperadoresResiduos { get; set; }

        public virtual Vehiculos Vehiculos { get; set; }
    }
}
