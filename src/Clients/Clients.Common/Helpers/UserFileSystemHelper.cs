using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clients.Common.Helpers
{
    public static class UserFileSystemHelper
    {
        public static string PathToUserLocalDb 
            => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "MemeRepo.db");
    }
}
