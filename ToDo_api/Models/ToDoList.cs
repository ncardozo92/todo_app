using System.Runtime.Serialization;

namespace ToDo_api.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ToDoList")]
    [DataContract]
    public partial class ToDoList
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ToDoList()
        {
            Tarea = new HashSet<Tarea>();
        }

        [DataMember]
        public long Id { get; set; }

        [StringLength(20)]
        [DataMember]
        [Required]
        public string Nombre { get; set; }

        [StringLength(200)]
        [DataMember]
        public string Descripcion { get; set; }

        [DataMember]
        [Required]
        public long IdUsuario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tarea> Tarea { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
