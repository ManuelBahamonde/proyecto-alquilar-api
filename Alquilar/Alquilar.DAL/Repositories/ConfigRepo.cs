
using System;
using System.Linq;

namespace Alquilar.DAL
{
    public class ConfigRepo : BaseRepo
    {
        #region Constructor
        public ConfigRepo(DB db) : base(db) { }
        #endregion

        public Config GetConfig()
        {
            return _db.Config.FirstOrDefault();
        }
    }
}
