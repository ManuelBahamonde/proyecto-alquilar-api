using Alquilar.DAL;
using Alquilar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alquilar.Services
{
    public class ImagenService
    {

        #region Members
        private readonly ImagenRepo _imagenRepo;
        #endregion

        #region Constructor
        public ImagenService(ImagenRepo imagenRepo)
        {
            _imagenRepo = imagenRepo;
        }
        #endregion

        #region CRUD
        // Create
        public Imagen CreateImagen(ImagenDTO imagen)
        {
            if (imagen is null)
                throw new ArgumentException("Imagen no valida");

            if (string.IsNullOrEmpty(imagen.Url))
                throw new ArgumentException("El URL de Imagen espcificado no es valido.");

            var imagenModel = new Imagen
            {
                Url = imagen.Url,
            };

            _imagenRepo.CreateImagen(imagenModel);
            _imagenRepo.SaveChanges();

            return imagenModel;
        }

        // Read
        public List<Imagen> GetImagenes()
        {
            return _imagenRepo.GetImagenes();
        }

        public Imagen GetImagenById(int idImagen)
        {
            return _imagenRepo.GetImagenById(idImagen);
        }

        // Update
        public void UpdateImagen(int idImagen, ImagenDTO imagen)
        {
            var imagenModel = new Imagen
            {
                Url = imagen.Url,
            };

            _imagenRepo.UpdateImagen(idImagen, imagenModel);
            _imagenRepo.SaveChanges();
        }

        // Delete
        public void DeleteImagen(int idImagen)
        {
            _imagenRepo.DeleteImagen(idImagen);
            _imagenRepo.SaveChanges();
        }
        #endregion

    }
}
