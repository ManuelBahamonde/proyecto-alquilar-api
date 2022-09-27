using Alquilar.DAL;
using Alquilar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alquilar.Services
{
     public class NoticiaService
    {
        #region Members
        private readonly NoticiaRepo _noticiaRepo;
        #endregion

        #region Constructor
        public NoticiaService(NoticiaRepo noticiaRepo)
        {
            _noticiaRepo = noticiaRepo;
        }
        #endregion

        #region CRUD
        // Create
        public Noticia CreateNoticia(NoticiaDTO noticia)
        {
            if (noticia is null)
                throw new ArgumentException("Noticia no valida");

            if (string.IsNullOrEmpty(noticia.Titulo))
                throw new ArgumentException("El titulo especificado no es valido.");

            if (string.IsNullOrEmpty(noticia.Descripcion))
                throw new ArgumentException("La descripcion especificada no es valida.");

            var noticiaModel = new Noticia
            {
                Titulo = noticia.Titulo,
                Descripcion = noticia.Descripcion,
            };

            _noticiaRepo.CreateNoticia(noticiaModel);
            _noticiaRepo.SaveChanges();

            return noticiaModel;
        }

        // Get
        public List<Noticia> GetNoticias()
        {
            return _noticiaRepo.GetNoticias();
        }

        public Noticia GetNoticiaById(int idNoticia)
        {
            return _noticiaRepo.GetNoticiaById(idNoticia);
        }

        // Update
        public void UpdateNoticia(int idNoticia, NoticiaDTO noticia)
        {
            var noticiaModel = new Noticia
            {
                Titulo = noticia.Titulo,
                Descripcion = noticia.Descripcion,
            };

            _noticiaRepo.UpdateNoticia(idNoticia, noticiaModel);
            _noticiaRepo.SaveChanges();
        }

        // Delete
        public void DeleteNoticia(int idNoticia)
        {
            _noticiaRepo.DeleteNoticia(idNoticia);
            _noticiaRepo.SaveChanges();
        }
        #endregion
    }
}
