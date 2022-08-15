using Domain.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Core.Services
{
    internal class AppInitializerService
    {
        #region Injects

        private readonly IFileStorageProvider _fileStorageProvider;

        #endregion

        #region Ctors

        public AppInitializerService(IFileStorageProvider fileStorageProvider)
        {
            _fileStorageProvider = fileStorageProvider;
        }

        #endregion

        public ValueTask InitApp()
        {
            _fileStorageProvider.DeleteFolder(_fileStorageProvider.TmpPath);

            return ValueTask.CompletedTask;
        }

    }
}
