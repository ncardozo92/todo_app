using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ToDo_api.Models
{
    public class Context : DbContext
    {
        public Context() : base("Context") { }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<ToDoList> ToDoList { get; set; }
        public DbSet<Tarea> Tarea { get; set; }
        //actualizar a medida que se crean nuevos modelos
    }
}