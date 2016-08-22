using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _Connections
{
    public static class _Oauth2
    {
        /// <summary>
        /// wrapper to get using an environment string instead of an enumeration
        /// </summary>
        public static Oauth2_PasswordModel GetOauth2PasswordModel(_Enums.SFApps sfApp, string environment)
        {
            return GetOauth2PasswordModel(sfApp, _Enums.GetEnvEnum(environment));
        }

        /// <summary>
        /// Get Oauth2 configuration
        /// </summary>
        /// <param name="sfApp"></param>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static Oauth2_PasswordModel GetOauth2PasswordModel(_Enums.SFApps sfApp, _Enums.Environments environment)
        {
            try
            {
                return new Oauth2_PasswordModel(
                    ClientIDs[sfApp][environment],
                    ClientSecrets[sfApp][environment],
                    RequestBaseUris[sfApp][environment],
                    RedirectUris[sfApp][environment],
                    Usernames[sfApp][environment],
                    Passwords[sfApp][environment]
                );
            }
            catch
            {
                throw new Exception(
                    _Enums.GetInvalidKeyError(new Dictionary<string, string>(){
                        {"SalesforceApp", sfApp.ToString()},
                        {"Environment", environment.ToString()}
                    }, "Oauth2")
                );
            }
        }

        /// <summary>
        /// Oauth2 client ID configuration
        /// </summary>
        private static Dictionary<_Enums.SFApps, Dictionary<_Enums.Environments, string>> ClientIDs = new Dictionary<_Enums.SFApps, Dictionary<_Enums.Environments, string>>()
        {
            {_Enums.SFApps.Redacted, new Dictionary<_Enums.Environments, string>()
                {
                    {_Enums.Environments.DEV, ""},
                    {_Enums.Environments.QA, ""},
                    {_Enums.Environments.PROD, ""}
                }
            }
        };

        /// <summary>
        /// Oauth2 client secret configuration
        /// </summary>
        private static Dictionary<_Enums.SFApps, Dictionary<_Enums.Environments, string>> ClientSecrets = new Dictionary<_Enums.SFApps, Dictionary<_Enums.Environments, string>>()
        {
            {_Enums.SFApps.Redacted, new Dictionary<_Enums.Environments, string>()
                {
                    {_Enums.Environments.DEV, ""},
                    {_Enums.Environments.QA, ""},
                    {_Enums.Environments.PROD, ""}
                }
            }
        };

        /// <summary>
        /// Oauth2 request uri configuration
        /// </summary>
        private static Dictionary<_Enums.SFApps, Dictionary<_Enums.Environments, string>> RequestBaseUris = new Dictionary<_Enums.SFApps, Dictionary<_Enums.Environments, string>>()
        {
            {_Enums.SFApps.Redacted, new Dictionary<_Enums.Environments, string>()
                {
                    {_Enums.Environments.DEV, ""},
                    {_Enums.Environments.QA, ""},
                    {_Enums.Environments.PROD, ""}
                }
            }
        };

        /// <summary>
        /// Oauth2 redirect uri configuration
        /// </summary>
        private static Dictionary<_Enums.SFApps, Dictionary<_Enums.Environments, string>> RedirectUris = new Dictionary<_Enums.SFApps, Dictionary<_Enums.Environments, string>>()
        {
            {_Enums.SFApps.Redacted, new Dictionary<_Enums.Environments, string>()
                {
                    {_Enums.Environments.DEV, ""},
                    {_Enums.Environments.QA, ""},
                    {_Enums.Environments.PROD, ""}
                }
            }
        };
        
        /// <summary>
        /// Oauth2 username configuration
        /// </summary>
        private static Dictionary<_Enums.SFApps, Dictionary<_Enums.Environments, string>> Usernames = new Dictionary<_Enums.SFApps, Dictionary<_Enums.Environments, string>>()
        {
            {_Enums.SFApps.Redacted, new Dictionary<_Enums.Environments, string>()
                {
                    {_Enums.Environments.DEV, ""},
                    {_Enums.Environments.QA, ""},
                    {_Enums.Environments.PROD, ""}
                }
            }
        };

        /// <summary>
        /// Oauth2 password configuration
        /// </summary>
        private static Dictionary<_Enums.SFApps, Dictionary<_Enums.Environments, string>> Passwords = new Dictionary<_Enums.SFApps, Dictionary<_Enums.Environments, string>>()
        {
            {_Enums.SFApps.Redacted, new Dictionary<_Enums.Environments, string>()
                {
                    {_Enums.Environments.DEV, ""},
                    {_Enums.Environments.QA, ""},
                    {_Enums.Environments.PROD, ""}
                }
            }
        };

        /// <summary>
        /// Model for containing Oauth2 configuration
        /// </summary>
        public class Oauth2_PasswordModel
        {
            public string ClientID;
            public string ClientSecret;
            public string RequestBaseUri;
            public string RedirectUri;
            public string Username;
            public string Password;

            public Oauth2_PasswordModel()
            {

            }

            public Oauth2_PasswordModel(string clientID, string clientSecret, string requestBaseUri, string redirectUri, string username, string password)
            {
                ClientID = clientID;
                ClientSecret = clientSecret;
                RequestBaseUri = requestBaseUri;
                RedirectUri = redirectUri;
                Username = username;
                Password = password;
            }
        }
    }
}
