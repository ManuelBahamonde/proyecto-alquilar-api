using Alquilar.DAL;
using System;

namespace Alquilar.Services
{
    public class ConfigService
    {
        #region Members
        private readonly ConfigRepo _configRepo;
        #endregion

        #region Constructor
        public ConfigService(ConfigRepo configRepo)
        {
            _configRepo = configRepo;
        }
        #endregion

        #region Methods
        public Config GetConfig()
        {
            var config = _configRepo.GetConfig();

            if (config == null)
                throw new InvalidOperationException("La Configuracion no esta definida.");

            return config;
        }
        #endregion
    }
}
