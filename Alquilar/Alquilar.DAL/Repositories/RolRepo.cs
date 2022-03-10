using Alquilar.Helpers.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alquilar.DAL
{
    public class RolRepo
    {
        #region Members
        private readonly DB _db;
        #endregion

        #region Constructor
        public RolRepo(DB db)
        {
            _db = db;
        }
        #endregion

        public List<Rol> GetRoles()
        {
            var roles = _db.Rol.ToList();

            return roles;
        }

        public Rol GetRolById(int idRol)
        {
            var rol = _db.Rol.FirstOrDefault(x => x.IdRol == idRol);

            return rol;
        }

        public void CreateRol(Rol rol)
        {
            if (rol == null)
                throw new ArgumentNullException(nameof(rol));

            _db.Rol.Add(rol);
        }

        public void UpdateRol(int idRol, Rol newRol)
        {
            if (newRol == null)
                throw new ArgumentNullException(nameof(newRol));

            var rol = _db.Rol.FirstOrDefault(l => l.IdRol == idRol);

            if (rol is null)
                throw new NotFoundException("No existe el Rol especificado");

            rol.Descripcion = newRol.Descripcion;
        }

        public void DeleteRol(int idRol)
        {
            var rol = _db.Rol.FirstOrDefault(l => l.IdRol == idRol);

            if (rol is null)
                throw new NotFoundException("No existe el Rol especificado");

            _db.Rol.Remove(rol);
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
