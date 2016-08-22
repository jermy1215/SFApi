using MySql.Data;
using MySql.Data.MySqlClient;
using Oracle.DataAccess.Client;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;

namespace _Connections
{
    public class _DataAdapter : IDisposable
    {
        private SqlConnection _sqlConnection;
        private MySqlConnection _mySqlConnection;
        private OracleConnection _oracleConnection;
        private DataSet _returnDataSet;
        private DataTable _returnDataTable;
        private DataRow _returnDataRow;
        private string _db = "";
        private string _user = "";
        private string _password = "";
        private bool _connected = false;
        private _Enums.ConnTypes _connectionType;
        //ssh
        private bool _useSsh = false;
        private string _sshIPAddress;
        private string _sshUser;
        private string _sshPassword;
        private SshClient _sshClient;
        private ForwardedPortLocal _sshTunnel;
        private string _localIPAddress;
        private uint _localPort;
        private string _remoteIPAddress;
        private uint _remotePort;


        #region public getters and setters
        public SqlConnection sqlConnection
        {
            get { return _sqlConnection; }
            set { _sqlConnection = value; }
        }

        public MySqlConnection mySqlConnection
        {
            get { return _mySqlConnection; }
            set { _mySqlConnection = value; }
        }

        public OracleConnection oracleConnection
        {
            get { return _oracleConnection; }
            set { _oracleConnection = value; }
        }

        public DataSet ReturnDataSet
        {
            get { return _returnDataSet; }
            set { _returnDataSet = value; }
        }

        public DataTable ReturnDataTable
        {
            get { return _returnDataTable; }
            set { _returnDataTable = value; }
        }

        public DataRow ReturnDataRow
        {
            get { return _returnDataRow; }
            set { _returnDataRow = value; }
        }

        public string DB
        {
            get { return _db; }
            set { _db = value; }
        }

        public string User
        {
            get { return _user; }
            set { _user = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public bool Connected
        {
            get { return _connected; }
            set { _connected = value; }
        }
        #endregion

        #region public initializers
        //public DataAdapter(ConnectionType type = ConnectionType.SQL, string database = null)
        public _DataAdapter(_Enums.Users user, _Enums.Environments environment, _Enums.Databases database = _Enums.Databases.Redacted, _Enums.ConnTypes connType = _Enums.ConnTypes.SQL)
        {
            Initialize(user, environment, database, connType);
        }

        public _DataAdapter(_Enums.Users user, string environment, _Enums.Databases database = _Enums.Databases.Redacted, _Enums.ConnTypes connType = _Enums.ConnTypes.SQL)
        {
            Initialize(user, _Enums.GetEnvEnum(environment), database, connType);
        }

        //ssh support
        public _DataAdapter(_Enums.Users user, string environment, string sshIPAddress, string sshUser, string sshPassword,
            string localIPAddress, uint localPort, string remoteIPAddress, uint remotePort,
            _Enums.Databases database = _Enums.Databases.Redacted, _Enums.ConnTypes connType = _Enums.ConnTypes.SQL)
        {
            _sshIPAddress = sshIPAddress;
            _sshUser = sshUser;
            _sshPassword = sshPassword;
            _localIPAddress = localIPAddress;
            _localPort = localPort;
            _remoteIPAddress = remoteIPAddress;
            _remotePort = remotePort;

            Initialize(user, _Enums.GetEnvEnum(environment), database, connType, true);
        }

        public void Initialize(_Enums.Users user, _Enums.Environments environment, _Enums.Databases database, _Enums.ConnTypes connType, bool ssh = false)
        {
            _connectionType = connType;
            string connectionString = _ConnString.Get(connType, user, database, environment);

            //ssh support
            if (ssh)
            {
                _useSsh = true;
                _sshClient = new SshClient(_sshIPAddress, _sshUser, _sshPassword);
                _sshTunnel = new ForwardedPortLocal(_localIPAddress, _localPort, _remoteIPAddress, _remotePort);
                _sshClient.Connect();
                _sshClient.AddForwardedPort(_sshTunnel);
                _sshTunnel.Start();
            }

            try
            {
                if (_connectionType == _Enums.ConnTypes.SQL)
                {
                    _sqlConnection = new SqlConnection(connectionString);
                    _sqlConnection.Open();
                    _connected = true;
                }
                else if (_connectionType == _Enums.ConnTypes.MYSQL)
                {
                    _mySqlConnection = new MySqlConnection(connectionString);
                    _mySqlConnection.Open();
                    _connected = true;
                }
                else if (_connectionType == _Enums.ConnTypes.ORACLE)
                {
                    _oracleConnection = new OracleConnection(connectionString);
                    _oracleConnection.Open();
                    _connected = true;
                }
                else
                    throw new Exception("Connection of type '" + _connectionType.ToString() + "' is not supported.");
            }
            catch (Exception ex)
            {
                if (_useSsh)
                {
                    _sshTunnel.Stop();
                    _sshClient.RemoveForwardedPort(_sshTunnel);
                    _sshClient.Disconnect();
                }
                throw ex;
            }
        }
        #endregion

        public bool Execute(string sql, CommandType type = CommandType.Text)
        {
            List<object> sqlParams = new List<object>();
            return Execute(sql, sqlParams, type);
        }

        public bool Execute(string sql, object parameter, CommandType type = CommandType.Text)
        {
            List<object> sqlParams = new List<object>();
            sqlParams.Add(parameter);
            return Execute(sql, sqlParams, type);
        }

        public bool Execute(string sql, List<object> parameters, CommandType type = CommandType.Text)
        {
            _returnDataSet = new DataSet();
            _returnDataTable = new DataTable();

            if (_connectionType == _Enums.ConnTypes.SQL)
            {
                SqlCommand cmd = new SqlCommand(sql, _sqlConnection);
                cmd.CommandType = type;

                if (parameters != null && parameters.Count > 0)
                    foreach (SqlParameter param in parameters)
                        cmd.Parameters.Add(param);

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    da.Fill(_returnDataSet);
            }
            else if (_connectionType == _Enums.ConnTypes.MYSQL)
            {
                MySqlCommand cmd = new MySqlCommand(sql, _mySqlConnection);
                cmd.CommandType = type;

                if (parameters != null && parameters.Count > 0)
                    foreach (MySqlParameter param in parameters)
                        cmd.Parameters.Add(param);

                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    da.Fill(_returnDataSet);
            }
            else if (_connectionType == _Enums.ConnTypes.ORACLE)
            {
                OracleCommand cmd = new OracleCommand(sql, _oracleConnection);
                cmd.CommandType = type;
                if (type == CommandType.StoredProcedure)
                    cmd.BindByName = true;

                if (parameters != null && parameters.Count > 0)
                    foreach (OracleParameter param in parameters)
                        cmd.Parameters.Add(param);

                using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                    da.Fill(_returnDataSet);
            }

            if (_returnDataSet.Tables.Count > 0)
                _returnDataTable = _returnDataSet.Tables[0];
            if (_returnDataTable.Rows.Count > 0)
                _returnDataRow = _returnDataTable.Rows[0];
            return true;
        }

        public int ExecuteNonQuery(string sql, object parameter, CommandType type = CommandType.Text)
        {
            List<object> sqlParams = new List<object>();
            sqlParams.Add(parameter);
            return ExecuteNonQuery(sql, sqlParams, type);
        }

        public int ExecuteNonQuery(string sql, CommandType type = CommandType.Text)
        {
            List<object> sqlParams = new List<object>();
            return ExecuteNonQuery(sql, sqlParams, type);
        }

        public int ExecuteNonQuery(string sql, List<object> parameters, CommandType type = CommandType.Text)
        {
            if (_connectionType == _Enums.ConnTypes.SQL)
            {
                SqlCommand cmd = new SqlCommand(sql, _sqlConnection);
                cmd.CommandType = type;

                if (parameters != null && parameters.Count > 0)
                    foreach (SqlParameter param in parameters)
                        cmd.Parameters.Add(param);

                //using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                //    da.Fill(_returnDataSet);

                return cmd.ExecuteNonQuery();
            }
            else if (_connectionType == _Enums.ConnTypes.MYSQL)
            {
                MySqlCommand cmd = new MySqlCommand(sql, _mySqlConnection);
                cmd.CommandType = type;

                if (parameters != null && parameters.Count > 0)
                    foreach (MySqlParameter param in parameters)
                        cmd.Parameters.Add(param);

                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    da.Fill(_returnDataSet);

                return cmd.ExecuteNonQuery();
            }
            else if (_connectionType == _Enums.ConnTypes.ORACLE)
            {
                OracleCommand cmd = new OracleCommand(sql, _oracleConnection);
                cmd.CommandType = type;
                if (type == CommandType.StoredProcedure)
                    cmd.BindByName = true;

                if (parameters != null && parameters.Count > 0)
                    foreach (OracleParameter param in parameters)
                        cmd.Parameters.Add(param);

                using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                    da.Fill(_returnDataSet);

                return cmd.ExecuteNonQuery();
            }
            else
                throw new Exception("Connection of type '" + _connectionType.ToString() + "' is not supported.");
        }

        public object ExecuteScalar(string sql, object parameter, CommandType type = CommandType.Text)
        {
            List<object> sqlParams = new List<object>();
            sqlParams.Add(parameter);
            return ExecuteScalar(sql, type, sqlParams);
        }

        public object ExecuteScalar(string sql, CommandType type = CommandType.Text, List<object> parameters = null)
        {
            if (_connectionType == _Enums.ConnTypes.SQL)
            {
                SqlCommand cmd = new SqlCommand(sql, _sqlConnection);
                cmd.CommandType = type;

                if (parameters != null && parameters.Count > 0)
                    foreach (SqlParameter param in parameters)
                        cmd.Parameters.Add(param);

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    da.Fill(_returnDataSet);

                return cmd.ExecuteScalar();
            }
            else if (_connectionType == _Enums.ConnTypes.MYSQL)
            {
                MySqlCommand cmd = new MySqlCommand(sql, _mySqlConnection);
                cmd.CommandType = type;

                if (parameters != null && parameters.Count > 0)
                    foreach (MySqlParameter param in parameters)
                        cmd.Parameters.Add(param);

                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    da.Fill(_returnDataSet);

                return cmd.ExecuteScalar();
            }
            else if (_connectionType == _Enums.ConnTypes.ORACLE)
            {
                OracleCommand cmd = new OracleCommand(sql, _oracleConnection);
                cmd.CommandType = type;
                if (type == CommandType.StoredProcedure)
                    cmd.BindByName = true;

                if (parameters != null && parameters.Count > 0)
                    foreach (OracleParameter param in parameters)
                        cmd.Parameters.Add(param);

                using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                    da.Fill(_returnDataSet);

                return cmd.ExecuteScalar();
            }
            else
                throw new Exception("Connection of type '" + _connectionType.ToString() + "' is not supported.");
        }

        /// <summary>
        /// Creates and returns a new SQL parameter, setting the given value to DBNull.Value where the given value is null.
        /// </summary>
        /// <param name="name">The parameter name</param>
        /// <param name="value">The parameter value</param>
        /// <returns>A newParam</returns>
        public SqlParameter newSqlParam(string name, object value, SqlDbType type = SqlDbType.VarChar, int size = 0)
        {
            if (value == null)
                value = DBNull.Value;
            if (size == 0)
                return new SqlParameter(name, value) { SqlDbType = type };
            else
                return new SqlParameter(name, value) { SqlDbType = type, Size = size };
        }

        /// <summary>
        /// Creates and returns a new MySql parameter, setting the given value to DBNull.Value where the given value is null.
        /// </summary>
        /// <param name="name">The parameter name</param>
        /// <param name="value">The parameter value</param>
        /// <returns>A newParam</returns>
        public MySqlParameter newMySqlParam(string name, object value, MySqlDbType type = MySqlDbType.VarChar, int size = 0)
        {
            if (value == null)
                value = DBNull.Value;
            if (size == 0)
                return new MySqlParameter(name, value) { MySqlDbType = type };
            else
                return new MySqlParameter(name, value) { MySqlDbType = type, Size = size };
        }

        /// <summary>
        /// Creates and returns a new Oracle parameter, setting the given value to DBNull.Value where the given value is null.
        /// </summary>
        /// <param name="name">The parameter name</param>
        /// <param name="value">The parameter value</param>
        /// <returns>A newParam</returns>
        public OracleParameter newOracleParam(string name, object value, OracleDbType type = OracleDbType.Varchar2, int size = 0)
        {
            if (value == null)
                value = DBNull.Value;
            if (size == 0)
                return new OracleParameter(name, value) { OracleDbType = type };
            else
                return new OracleParameter(name, value) { OracleDbType = type, Size = size };
        }

        /// <summary>
        /// Creates and returns a new ODBC parameter, setting the given value to DBNull.Value where the given value is null.
        /// </summary>
        /// <param name="name">The parameter name</param>
        /// <param name="value">The parameter value</param>
        /// <returns>A newParam</returns>
        public OdbcParameter newParam(string name, object value, OdbcType type = OdbcType.VarChar, int size = 0)
        {
            if (value == null)
                value = DBNull.Value;
            if (size == 0)
                return new OdbcParameter(name, value) { OdbcType = type };
            else
                return new OdbcParameter(name, value) { OdbcType = type, Size = size };
        }

        public string SeqNextVal(string sequence, bool rethrow = false)
        {
            try
            {
                string sqlGetNextSeq = string.Format("SELECT NEXT VALUE FOR dbo.{0} as SEQ", sequence);
                Execute(sqlGetNextSeq);
                return _returnDataTable.Rows[0]["SEQ"].ToString();
            }
            catch (Exception ex)
            {
                if (rethrow)
                    throw new Exception(ex.Message);
                return ex.Message;
            }
        }

        /// <summary>
        /// Function to return a DateTime value from an object conversion.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>DateTime value of object's string value.</returns>
        public DateTime ToDateTime(object value)
        {
            DateTime returnDateTime = new DateTime();
            DateTime.TryParse(value.ToString(), out returnDateTime);
            return returnDateTime;
        }

        /// <summary>
        /// Function to return a Decimal value from an object conversion. If the object's string value is not a Decimal, we return 0.0.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Decimal value of object's string value. 0.0 if the string value of the object is not a valid decimal.</returns>
        public decimal ToDecimal(object value)
        {
            decimal returnDecimal = 0;
            decimal.TryParse(value.ToString(), out returnDecimal);
            return returnDecimal;
        }

        /// <summary>
        /// Function to return a Decimal value from an object conversion. If the object's string value is not a Decimal, we return 0.0.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Decimal value of object's string value. 0.0 if the string value of the object is not a valid decimal.</returns>
        public decimal? ToNullableDecimal(object value)
        {
            if (value == null)
                return null;

            return ToDecimal(value);
        }

        /// <summary>
        /// Function to return an integer value from an object conversion. If the object's string value is not an Integer, we return 0;
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Int value of object's string value. 0 if the string value of the object is not a valid integer.</returns>
        public int ToInt(object value)
        {
            int returnInt = 0;
            Int32.TryParse(value.ToString(), out returnInt);
            return returnInt;
        }

        /// <summary>
        /// Checks if a value is null and if it is, it sets it to DBNull
        /// </summary>
        /// <param name="value">Value to check</param>
        public object IfNull(object value)
        {
            if (value == null)
                value = DBNull.Value;
            return value;
        }

        /// <summary>
        /// See if the DataRow object, returnDataRow, has any rows
        /// </summary>
        /// <returns>True if rows exist and false if they do not</returns>
        public bool HasResults()
        {
            if (_returnDataRow != null)
                if (_returnDataRow.ItemArray.Length > 0)
                    return true;

            return false;
        }

        /// <summary>
        /// Builds an insert string using a field list.
        /// </summary>
        /// <param name="table"></param>
        /// <param name="fieldList"></param>
        /// <param name="conditional"></param>
        /// <returns></returns>
        public _SqlCommand BuildInsert(string table, Dictionary<string, object> fieldList, string conditional = "")
        {
            _SqlCommand command = new _SqlCommand();
            string fields = "";
            string parameters = "";
            for (int i = 0; i < fieldList.Count; i++)
            {
                string postFix = "";
                if (i < (fieldList.Count - 1))
                    postFix = ",";

                fields += string.Format(" {0}{1}", fieldList.ElementAt(i).Key, postFix);
                
                //set up placeholder and param names based on db type
                string paramPlaceholder = "?";
                if (_connectionType == _Enums.ConnTypes.SQL)
                {
                    paramPlaceholder = "@" + fieldList.ElementAt(i).Key;
                    command.Parameters.Add(newSqlParam(paramPlaceholder, fieldList.ElementAt(i).Value));
                }
                else
                    command.Parameters.Add(newParam(fieldList.ElementAt(i).Key, fieldList.ElementAt(i).Value));

                parameters += string.Format(" {0}{1}", paramPlaceholder, postFix);
            }

            command.Sql = string.Format("INSERT INTO {0} ({1}) VALUES ({2})", table, fields, parameters);
            if (!string.IsNullOrWhiteSpace(conditional))
                command.Sql += " " + conditional;

            return command;
        }

        /// <summary>
        /// Builds an update string using a field list.
        /// </summary>
        /// <param name="table"></param>
        /// <param name="fieldList"></param>
        /// <param name="conditional"></param>
        /// <returns></returns>
        public _SqlCommand BuildUpdate(string table, Dictionary<string, object> fieldList, string conditional = "")
        {
            _SqlCommand command = new _SqlCommand();
            string body = "";
            for (int i = 0; i < fieldList.Count; i++)
            {
                if (IfNull(fieldList.ElementAt(i).Value) == DBNull.Value)
                    continue;

                string postFix = "";
                if (i < (fieldList.Count - 1))
                    postFix = ",";

                //set up placeholder and param names based on db type
                string paramPlaceholder = "?";
                if (_connectionType == _Enums.ConnTypes.SQL)
                {
                    paramPlaceholder = "@" + fieldList.ElementAt(i).Key;
                    command.Parameters.Add(newSqlParam(paramPlaceholder, fieldList.ElementAt(i).Value));
                }
                else
                    command.Parameters.Add(newParam(fieldList.ElementAt(i).Key, fieldList.ElementAt(i).Value));
                body += string.Format(" {0} = {1}{2}", fieldList.ElementAt(i).Key, paramPlaceholder, postFix);
            }

            command.Sql = string.Format("UPDATE {0} SET {1}", table, body);
            if (!string.IsNullOrWhiteSpace(conditional))
                command.Sql += " " + conditional;

            return command;
        }

        void IDisposable.Dispose()
        {
            if (_connectionType == _Enums.ConnTypes.SQL)
            {
                _sqlConnection.Close();
                _sqlConnection.Dispose();
            }
            else if (_connectionType == _Enums.ConnTypes.MYSQL)
            {
                _mySqlConnection.Close();
                _mySqlConnection.Dispose();
            }
            else if (_connectionType == _Enums.ConnTypes.ORACLE)
            {
                _oracleConnection.Close();
                _oracleConnection.Dispose();
            }

            //ssh support
            if (_useSsh)
            {
                _sshTunnel.Stop();
                _sshClient.RemoveForwardedPort(_sshTunnel);
                _sshClient.Disconnect();
            }
        }
    }
}
