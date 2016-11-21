using System.Configuration;

namespace Dealership.ConfigurationReaders
{
    public class ConfigurationReader
    {
        public bool IsMongo()
        {
            return bool.Parse(ConfigurationManager.AppSettings["IsMongo"]);
        }

        public bool IsSqlServer()
        {
            return bool.Parse(ConfigurationManager.AppSettings["IsSqlServer"]);
        }
    }
}
