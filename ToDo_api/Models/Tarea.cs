using System.Runtime.Serialization;

namespace ToDo_api.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Tarea")]
    [DataContract]
    public partial class Tarea
    {
        [DataMember]
        public long Id { get; set; }

        [Required]
        [StringLength(65)]
        [DataMember]
        public string Descripcion { get; set; }

        [DataMember]
        public long IdTodo { get; set; }
        [DataMember]
        public bool Hecho { get; set; }

        public virtual ToDoList ToDoList { get; set; }
    }
}
