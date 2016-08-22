using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Collections;

namespace _Connections
{
    public static class _ConnString
    {
        #region drivers
        private static Dictionary<_Enums.ConnTypes, string> OdbcDrivers = new Dictionary<_Enums.ConnTypes, string>()
        {
            {_Enums.ConnTypes.SQL, "SQL Server"},
            {_Enums.ConnTypes.MYSQL, "{MySQL ODBC 5.1 Driver}"},
            {_Enums.ConnTypes.ORACLE, "Oracle in OraClient11g_home1"}
        };
        #endregion

        public static string Get(_Enums.ConnTypes Type, _Enums.Users User, _Enums.Databases Database, _Enums.Environments Environment)
        {
            switch(Type)
            {
                case _Enums.ConnTypes.SQL:
                    return string.Format("Server={0};Database={1};User Id={2};Password={3}",_Servers.Get(Database, Environment), _Databases.Get(Database, Environment), User.ToString(), _Passwords.Get(User, Environment));
                case _Enums.ConnTypes.MYSQL:
                    return string.Format("Server={0};Port={1};Database={2};Uid={3};Pwd={4};Pooling=false;",_Servers.Get(Database, Environment), _Ports.Get(Database, Environment), _Databases.Get(Database, Environment), User.ToString(), _Passwords.Get(User, Environment));
                case _Enums.ConnTypes.ORACLE:
                    return string.Format("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT={1})))(CONNECT_DATA=(SERVER=DEDICATED)(SID={2})));User id={3}; Password={4}; enlist=false; pooling=true;", _Servers.Get(Database, Environment), _Ports.Get(Database, Environment), _Databases.Get(Database, Environment), User.ToString(), _Passwords.Get(User, Environment));
                default:
                    return string.Format("Driver={0};Server={1};Database={2};charset=utf8;UID={3};PWD={4}", OdbcDrivers[Type],_Servers.Get(Database, Environment), _Databases.Get(Database, Environment), User.ToString(), _Passwords.Get(User, Environment));
            }
        }

        public static string GetNativeSql(_Enums.Users User, _Enums.Databases Database, _Enums.Environments Environment)
        {
            return string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3}",_Servers.Get(Database, Environment), _Databases.Get(Database, Environment), User.ToString(), _Passwords.Get(User, Environment));
        }

         /// <summary>
         /// wrapper to get using an environment string instead of an enumeration
         /// </summary>
        public static string Get(_Enums.ConnTypes Type, _Enums.Users User, _Enums.Databases Database, string Environment)
        {
            return Get(Type, User, Database, _Enums.GetEnvEnum(Environment));
        }
    }
}
