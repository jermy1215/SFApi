using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _Connections
{
    public static class _Ports
    {
        /// <summary>
        /// wrapper to get using an environment string instead of an enumeration
        /// </summary>
        public static int Get(_Enums.Databases database, string environment)
        {
            return Get(database, _Enums.GetEnvEnum(environment));
        }

        /// <summary>
        /// Get port configuration
        /// </summary>
        /// <param name="database"></param>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static int Get(_Enums.Databases database, _Enums.Environments environment)
        {
            try
            {
                return Ports[database][environment];
            }
            catch
            {
                throw new Exception(
                    _Enums.GetInvalidKeyError(new Dictionary<string, string>(){
                        {"Database", database.ToString()},
                        {"Environment", environment.ToString()}
                    }, "Port")
                );
            }
        }

        /// <summary>
        /// port configuration
        /// </summary>
        private static Dictionary<_Enums.Databases, Dictionary<_Enums.Environments, int>> Ports = new Dictionary<_Enums.Databases, Dictionary<_Enums.Environments, int>>()
        {
            {_Enums.Databases.Redacted, new Dictionary<_Enums.Environments, int>()
                {
                    {_Enums.Environments.DEV, 3307},
                    {_Enums.Environments.QA, 3307},
                    {_Enums.Environments.PROD, 3307}
                }
            }         
        };
    }
}
