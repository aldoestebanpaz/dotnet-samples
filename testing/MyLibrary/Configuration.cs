using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary
{
    public class Configuration
    {
        private readonly IEnvironmentWrapper _environment;

        public Configuration(IEnvironmentWrapper environment)
        {
            _environment = environment;

            DbDialect = _environment.GetVariable("DB_DIALECT") ?? "";
            DbDriver = _environment.GetVariable("DB_DRIVER") ?? "";
            DbUsername = _environment.GetVariable("DB_USERNAME") ?? "";
            DbPassword = _environment.GetVariable("DB_PASSWORD") ?? "";
            DbHost = _environment.GetVariable("DB_HOST") ?? "";
            DbPort = _environment.GetVariable("DB_PORT") ?? "";
            DbName = _environment.GetVariable("DB_NAME") ?? "";
        }

        public string DbDialect { get; }

        public string DbDriver { get; }

        public string DbUsername { get; }

        public string DbPassword { get; }

        public string DbHost { get; }

        public string DbPort { get; }

        public string DbName { get; }

        public string GetConnectionString()
        {
            // dialect+driver://username:password@host:port/database
            return String.Format("{0}://{1}:{2}@{3}:{4}/{5}",
                (!String.IsNullOrEmpty(DbDialect) ? DbDialect : "mysql") + (!String.IsNullOrEmpty(DbDriver) ? $"+{DbDriver}": ""),
                DbUsername,
                DbPassword,
                (!String.IsNullOrEmpty(DbHost) ? DbHost : "localhost"),
                (!String.IsNullOrEmpty(DbPort) ? DbPort : "3306"),
                DbName
            );
        }
    }
}
