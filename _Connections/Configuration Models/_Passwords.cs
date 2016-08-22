using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _Connections
{
    public static class _Passwords
    {
        /// <summary>
        /// wrapper to get using an environment string instead of an enumeration
        /// </summary>
        public static string Get(_Enums.Users user, string environment)
        {
            return Get(user, _Enums.GetEnvEnum(environment));
        }

        /// <summary>
        /// Get password configuration
        /// </summary>
        /// <param name="user"></param>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static string Get(_Enums.Users user, _Enums.Environments environment)
        {
            try
            {
                return Passwords[user][environment];
            }
            catch
            {
                throw new Exception(
                    _Enums.GetInvalidKeyError(new Dictionary<string, string>(){
                        {"User", user.ToString()},
                        {"Environment", environment.ToString()}
                    }, "Password")
                );
            }
        }

        /// <summary>
        /// password configuration
        /// </summary>
        private static Dictionary<_Enums.Users, Dictionary<_Enums.Environments, string>> Passwords = new Dictionary<_Enums.Users, Dictionary<_Enums.Environments, string>>()
        {
            {_Enums.Users.Redacted, new Dictionary<_Enums.Environments, string>()
                {
                    {_Enums.Environments.DEV, ""},
                    {_Enums.Environments.QA, ""},
                    {_Enums.Environments.PROD, ""}
                }
            }
        };
    }
}
