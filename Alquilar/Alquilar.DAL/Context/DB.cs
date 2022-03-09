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

        #region Db Sets
        public DbSet<Localidad> Localidad { get; set; }
        public DbSet<Provincia> Provincia { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<TipoInmueble> TipoInmueble { get; set; }
        #endregion
    }
}
