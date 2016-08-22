using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesforceData
{
    public static class DynamicConfig
    {
        public static string Environment { get; set; }
        public static OauthToken PasswordToken;

        public static void Get(string environment)
        {
            Environment = environment;
            PasswordToken = new PasswordToken(Environment);
        }

        public static void GetConfig(NameValueCollection configuration)
        {
            Environment = configuration["Environment"];
            string enviro = Environment;
            PasswordToken = new PasswordToken(Environment);
        }
    }
}
