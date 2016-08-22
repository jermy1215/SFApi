using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Connections
{
    /// <summary>
    /// Class to hold a Sql statement and corresponding parameters
    /// </summary>
    public class _SqlCommand
    {
        public string Sql { get; set; }
        public List<object> Parameters = new List<object>();

        public _SqlCommand()
        {

        }

        public _SqlCommand(string sql)
        {
            Sql = sql;
        }

        public _SqlCommand(string sql, List<object> parameters)
        {
            Sql = sql;
            Parameters = parameters;
        }
    }
}
