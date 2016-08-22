using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _Connections
{
    public static class _Enums
    {
        public enum Environments { DEV, QA, PROD };
        public enum ConnTypes { SQL, MYSQL, ORACLE, API, EntityFramework };
        public enum Users { Redacted };
        public enum Databases { Redacted };
        public enum APIs { Redacted };
        public enum Systems { Redacted };
        public enum CurrencyCodes { USD, CAD };
        public enum SFApps { Redacted };

        private static Dictionary<string, Environments> EnvironmentEnums = new Dictionary<string, Environments>()
        {
            {"DEV", Environments.DEV},
            {"QA", Environments.QA},
            {"PROD", Environments.PROD}
        };

        private static Dictionary<string, CurrencyCodes> CurrencyCodeEnums = new Dictionary<string, CurrencyCodes>()
        {
            {"USD", CurrencyCodes.USD},
            {"CAD", CurrencyCodes.CAD}
        };

        #region helper methods
        public static Environments GetEnvEnum(string environment)
        {
            try
            {
                return EnvironmentEnums[environment];
            }
            catch
            {
                throw new Exception((string.IsNullOrWhiteSpace(environment) ? "NULL" : environment) + " is not a valid environment enumeration");
            }
        }

        public static CurrencyCodes GetCurrencyCodeEnum(string currencyCode)
        {
            try
            {
                return CurrencyCodeEnums[currencyCode];
            }
            catch
            {
                throw new Exception((string.IsNullOrWhiteSpace(currencyCode) ? "NULL" : currencyCode) + " is not a valid currency code enumeration");
            }
        }

        /// <summary>
        /// Gets an invalid key exception message
        /// </summary>
        /// <param name="keys">Dictionary of keys used to get from a dictionary</param>
        /// <param name="dictionary">Name of dictionary that failure occurred on</param>
        public static string GetInvalidKeyError(Dictionary<string, string> keys, string dictionary)
        {
            string error = "No " + dictionary + " configuration found for key values: ";
            foreach (KeyValuePair<string, string> entry in keys)
                if (string.IsNullOrWhiteSpace(entry.Value))
                    error += string.Format("{0} - NULL, ", entry.Key);
                else
                    error += string.Format("{0} - {1}, ", entry.Key, entry.Value);

            throw new Exception((error.LastIndexOf(',') > 0 ? error.Substring(0, error.LastIndexOf(',')) : error));
        }
        #endregion
    }
}
