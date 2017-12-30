using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDo_api.Models
{
    public class ToDoList
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public long IdUsuario { get; set; }

        public virtual ICollection<Tarea> Tarea { get; set; }
    }
}