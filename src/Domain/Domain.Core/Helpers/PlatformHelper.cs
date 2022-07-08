using System.Runtime.InteropServices;

namespace Domain.Core.Helpers
{
    public static class PlatformHelper
    {
        public static bool IsDesktop
        {
            get
            {
                return RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            }
        }
    }
}
