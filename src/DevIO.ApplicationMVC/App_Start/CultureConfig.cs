using System.Globalization;

namespace DevIO.ApplicationMVC
{
    public class CultureConfig
    {
        public static void RegisterCulture ( )
        {
            var culture = new CultureInfo("pt-BR");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
        }
    }
}