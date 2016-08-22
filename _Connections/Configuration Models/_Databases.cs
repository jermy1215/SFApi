using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _Connections
{
    public static class _Databases
    {
        /// <summary>
        /// wrapper to get using an environment string instead of an enumeration
        /// </summary>
        public static string Get(_Enums.Databases database, string environment)
        {
            return Get(database, _Enums.GetEnvEnum(environment));
        }

        /// <summary>
        /// Get database configuration
        /// </summary>
        /// <param name="database"></param>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static string Get(_Enums.Databases database, _Enums.Environments environment)
        {
            try
            {
                return Databases[database][environment];
            }
            catch
            {
                throw new Exception(
                    _Enums.GetInvalidKeyError(new Dictionary<string, string>(){
                        {"Database", database.ToString()},
                        {"Environment", environment.ToString()}
                    }, "Database")
                );
            }
        }

        /// <summary>
        /// database configuration
        /// </summary>
        private static Dictionary<_Enums.Databases, Dictionary<_Enums.Environments, string>> Databases = new Dictionary<_Enums.Databases, Dictionary<_Enums.Environments, string>>()
        {
            {_Enums.Databases.Redacted, new Dictionary<_Enums.Environments, string>()
                {
                    {_Enums.Environments.DEV, ""},
                    {_Enums.Environments.QA, ""},
                    {_Enums.Environments.PROD, ""}
                }
            }
        };
    }
}
