using System.Runtime.Serialization;

namespace ToDo_api.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Usuario")]
    [DataContract]
    public partial class Usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuario()
        {
            ToDoList = new HashSet<ToDoList>();
        }
        [DataMember]
        public long Id { get; set; }

        [Required]
        [StringLength(30)]
        [DataMember]
        [EmailAddress]
        public string Mail { get; set; }

        [Required]
        [StringLength(20)]
        [DataMember]
        public string Username { get; set; }

        [Required]
        [StringLength(16)]
        [DataMember]
        public string Clave { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ToDoList> ToDoList { get; set; }
    }
}
