using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alquilar.DAL
{
    public class DB : DbContext
    {
        public DB(DbContextOptions<DB> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) { 
            modelBuilder.Entity<Inmueble>().HasMany(i => i.Imagenes); 
        }


        #region Db Sets
        public DbSet<Localidad> Localidad { get; set; }
        public DbSet<Provincia> Provincia { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<TipoInmueble> TipoInmueble { get; set; }
        public DbSet<Imagen> Imagen { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Inmueble> Inmueble { get; set; }
        #endregion
    }
}
