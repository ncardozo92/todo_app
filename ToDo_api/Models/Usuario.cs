using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDo_api.Models
{
    public class Usuario
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string mail { get; set; }
        public string Clave { get; set; }

        public virtual ICollection<ToDoList> ToDoList { get; set; }
    }
}