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
    public class _DBModel
    {
        public Dictionary<string, string> DBProperties = new Dictionary<string, string>();
        public string Table;
        public string ID;
        public string Environment = "DEV";
        public string Source { get; set; }

        public class DbProperty
        {
            public string FieldName;
            public OdbcType FieldType;
            public int FieldSize;

            public DbProperty() { }

            public DbProperty(string fieldName)
            {
                FieldName = fieldName;
                FieldType = OdbcType.VarChar;
            }

            public DbProperty(string fieldName, OdbcType fieldType)
            {
                FieldName = fieldName;
                FieldType = fieldType;
            }

            public DbProperty(string fieldName, OdbcType fieldType, int fieldSize)
            {
                FieldName = fieldName;
                FieldType = fieldType;
                FieldSize = fieldSize;
            }
        }

        protected void Get(_DataAdapter da, string whereCondition = null, List<object> whereParams = null)
        {
            if (string.IsNullOrWhiteSpace(ID))
                throw new Exception("Given " + this.GetType().Name + " ID is null or missing.");
            string initialID = ID;

            string sql = @"SELECT " + DBProperties["ID"];
            foreach (KeyValuePair<string, string> dbProperty in DBProperties)
                if (dbProperty.Key == "ID")
                    continue;
                else
                    if (dbProperty.Value == null)
                        sql += ", " + dbProperty.Key;
                    else
                        sql += ", " + dbProperty.Key + " as " + dbProperty.Value;

            sql += " FROM " + Table;

            if (!string.IsNullOrWhiteSpace(whereCondition) && whereParams != null)
            {
                sql += " " + whereCondition;
                da.Execute(sql, whereParams, CommandType.Text);
            }
            else
            {
                sql += " WHERE " + DBProperties["ID"] + " = ?";
                da.Execute(sql, da.newParam("ID", ID));
            }

            PropertyInfo[] properties = this.GetType().GetProperties();

            foreach (PropertyInfo prop in properties)
                if (prop != null && da.ReturnDataRow != null)
                    if (da.ReturnDataRow.Table.Columns.Contains(prop.Name))
                        prop.SetValue(this, (da.ReturnDataRow[prop.Name] != DBNull.Value ? da.ReturnDataRow[prop.Name] : null), null);

            if (string.IsNullOrWhiteSpace(ID) || da.ReturnDataRow == null)
                throw new Exception("Given " + this.GetType().Name + " ID did not return an existing " + this.GetType().Name + ": " + initialID);
        }
    }
}
