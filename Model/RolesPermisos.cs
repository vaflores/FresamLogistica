namespace FresamLogistica.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RolesPermisos
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string id_rol { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_permiso { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string usuario_crea { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime fecha_crea { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(50)]
        public string usuario_modifica { get; set; }

        [Key]
        [Column(Order = 5)]
        public DateTime fecha_modifica { get; set; }

        public virtual Permisos Permisos { get; set; }

        public virtual Roles Roles { get; set; }
    }
}
