using Microsoft.EntityFrameworkCore;

namespace Alquilar.DAL
{
    public class DB : DbContext
    {
        public DB(DbContextOptions<DB> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) { 
            modelBuilder.Entity<Inmueble>().HasMany(i => i.Imagenes);

            modelBuilder.Entity<TurnoAsignado>().HasKey(x => new { x.IdUsuario, x.IdInmueble, x.Fecha });

            modelBuilder.Entity<Usuario>().HasMany(u => u.Horarios);
        }

        #region Db Sets
        public DbSet<Localidad> Localidad { get; set; }
        public DbSet<Provincia> Provincia { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<TipoInmueble> TipoInmueble { get; set; }
        public DbSet<Imagen> Imagen { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Inmueble> Inmueble { get; set; }
        public DbSet<Horario> Horario { get; set; }
        public DbSet<TurnoAsignado> TurnoAsignado { get; set; }
        public DbSet<Config> Config { get; set; }
        public DbSet<Noticia> Noticia { get; set; }
        #endregion
    }
}
