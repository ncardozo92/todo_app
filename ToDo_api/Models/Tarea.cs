using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDo_api.Models
{
    public class Tarea
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public long IdToDo { get; set; }
        public bool Hecho { get; set; }
    }
}