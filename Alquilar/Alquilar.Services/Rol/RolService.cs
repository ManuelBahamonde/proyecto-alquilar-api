using Alquilar.DAL;
using Alquilar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alquilar.Services
{
    public class RolService
    {
        #region Members
        private readonly RolRepo _rolRepo;
        #endregion

        #region Constructor
        public RolService(RolRepo rolRepo)
        {
            _rolRepo = rolRepo;
        }
        #endregion

        #region CRUD
        // Create
        public Rol CreateRol(RolDTO rol)
        {
            if (rol is null)
                throw new ArgumentException("Rol no valido");

            if (string.IsNullOrEmpty(rol.Descripcion))
                throw new ArgumentException("El nombre de Rol espcificado no es valido.");

            var rolModel = new Rol
            {
                Descripcion = rol.Descripcion,
            };

            _rolRepo.CreateRol(rolModel);
            _rolRepo.SaveChanges();

            return rolModel;
        }

        // Read
        public List<Rol> GetRoles()
        {
            return _rolRepo.GetRoles();
        }

        public Rol GetRolById(int idRol)
        {
            return _rolRepo.GetRolById(idRol);
        }

        // Update
        public void UpdateRol(int idRol, RolDTO rol)
        {
            var rolModel = new Rol
            {
                Descripcion = rol.Descripcion,
            };

            _rolRepo.UpdateRol(idRol, rolModel);
            _rolRepo.SaveChanges();
        }

        // Delete
        public void DeleteRol(int idRol)
        {
            _rolRepo.DeleteRol(idRol);
            _rolRepo.SaveChanges();
        }
        #endregion
    }
}
