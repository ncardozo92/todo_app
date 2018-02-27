namespace ToDo_api.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AppModel : DbContext
    {
        public AppModel()
            : base("name=AppModel")
        {
        }

        public virtual DbSet<Tarea> Tarea { get; set; }
        public virtual DbSet<ToDoList> ToDoList { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tarea>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<ToDoList>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<ToDoList>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<ToDoList>()
                .HasMany(e => e.Tarea)
                .WithRequired(e => e.ToDoList)
                .HasForeignKey(e => e.IdTodo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Mail)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Clave)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.ToDoList)
                .WithRequired(e => e.Usuario)
                .HasForeignKey(e => e.IdUsuario)
                .WillCascadeOnDelete(false);
        }
    }
}
