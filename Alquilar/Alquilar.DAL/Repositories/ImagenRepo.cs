using Alquilar.Helpers.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alquilar.DAL
{
    public class ImagenRepo : BaseRepo
    {
        #region Constructor
        public ImagenRepo(DB db) : base(db) { }
        #endregion

        public List<Imagen> GetImagenes()
        {
            var imagenes = _db.Imagen.ToList();

            return imagenes;
        }

        public Imagen GetImagenById(int idImagen)
        {
            var imagen = _db.Imagen.FirstOrDefault(x => x.IdImagen == idImagen);

            return imagen;
        }

        public void CreateImagen(Imagen imagen)
        {
            if (imagen == null)
                throw new ArgumentNullException(nameof(imagen));

            _db.Imagen.Add(imagen);
        }

        public void UpdateImagen(int idImagen, Imagen newImagen)
        {
            if (newImagen == null)
                throw new ArgumentNullException(nameof(newImagen));

            var imagen = _db.Imagen.FirstOrDefault(l => l.IdImagen == idImagen);

            if (imagen is null)
                throw new NotFoundException("No existe la Imagen especificada");

            imagen.Url = newImagen.Url;
            imagen.IdUsuario = newImagen.IdUsuario;
            imagen.IdInmueble = newImagen.IdInmueble;
        }

        public void DeleteImagen(int idImagen)
        {
            var imagen = _db.Imagen.FirstOrDefault(l => l.IdImagen == idImagen);

            if (imagen is null)
                throw new NotFoundException("No existe la Imagen especificada");

            _db.Imagen.Remove(imagen);
        }
    }
}
