using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace WebApi92.Context
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }

        public DbSet<Autor>Autores { get; set; }
        public DbSet<Libro>Libros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //INSERTAR EN LA TABLA USUARIOS
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    PkUsuario = 1,
                    Nombre = "Misael Cruz",
                    User = "MisaelCC",
                    Password = "123",
                    FkRol = 1,
                });

            //Insertr em la tabla rol
            modelBuilder.Entity<Rol>().HasData(
                new Rol
                {
                    PkRol = 1,
                    Nombre = "Director de ASC"
                });

        }
    }
}