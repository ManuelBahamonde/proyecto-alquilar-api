using Alquilar.Helpers.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alquilar.DAL
{
    public class NoticiaRepo : BaseRepo
    {
        #region Constructor
        public NoticiaRepo(DB db) : base(db) { }
        #endregion

        public List<Noticia> GetNoticias()
        {
            var noticias = _db.Noticia.ToList();

            return noticias;
        }

        public Noticia GetNoticiaById(int idNoticia)
        {
            var noticia = _db.Noticia.FirstOrDefault(x => x.IdNoticia == idNoticia);

            return noticia;
        }
        public void CreateNoticia(Noticia noticia)
        {
            if (noticia == null)
                throw new ArgumentNullException(nameof(noticia));

            _db.Noticia.Add(noticia);
        }

        public void UpdateNoticia(int idNoticia, Noticia newNoticia)
        {
            if (newNoticia == null)
                throw new ArgumentNullException(nameof(newNoticia));

            var noticia = _db.Noticia.FirstOrDefault(l => l.IdNoticia == idNoticia);

            if (noticia is null)
                throw new NotFoundException("No existe la noticia especificada");

            noticia.Titulo = newNoticia.Titulo;
            noticia.Descripcion = newNoticia.Descripcion;
        }

        public void DeleteNoticia(int idNoticia)
        {
            var noticia = _db.Noticia.FirstOrDefault(l => l.IdNoticia == idNoticia);

            if (noticia is null)
                throw new NotFoundException("No existe la noticia especificada");

            _db.Noticia.Remove(noticia);
        }

    }
}
