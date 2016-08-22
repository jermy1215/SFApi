using _Connections;
using _Utilities;
using _Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;
using SalesforceData.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SalesforceData
{
    public class SoqlQuery : _RestAdapter
    {
        //inputs
        public string Query { get; set; }
        public List<string> ReturnFields { get; set; }
        public List<WhereField> WhereFields { get; set; }
        public string From { get; set; }
        public int _Limit = 1;
        public int Limit
        {
            get { return _Limit; }
            set { _Limit = value; }
        }
        public bool ResultsReturned = false;
        private OauthToken Token;

        #region initializers
        /// <summary>
        /// Basic initialization. Token and Version are needed, so if this initilizaiton is chosen, a Token and Version must be supplied before executing a query.
        /// </summary>
        public SoqlQuery()
        {
            if (DynamicConfig.PasswordToken != null && Token == null)
                Token = DynamicConfig.PasswordToken;
        }

        public SoqlQuery(string query)
        {
            if (DynamicConfig.PasswordToken != null && Token == null)
                Token = DynamicConfig.PasswordToken;

            Query = query;
        }

        public SoqlQuery(List<string> returnFields, List<WhereField> whereFields, string from, int limit = 1)
        {
            if (DynamicConfig.PasswordToken != null && Token == null)
                Token = DynamicConfig.PasswordToken;

            ReturnFields = returnFields;
            WhereFields = whereFields;
            From = from;
            Limit = limit;
        }

        public SoqlQuery(List<string> returnFields, WhereField whereField, string from, int limit = 1)
        {
            if (DynamicConfig.PasswordToken != null && Token == null)
                Token = DynamicConfig.PasswordToken;

            ReturnFields = returnFields;
            WhereFields = new List<WhereField>() { whereField };
            From = from;
            Limit = limit;
        }

        public SoqlQuery(List<WhereField> whereFields, string from, int limit = 1)
        {
            if (DynamicConfig.PasswordToken != null && Token == null)
                Token = DynamicConfig.PasswordToken;

            WhereFields = whereFields;
            From = from;
            Limit = limit;
        }

        public SoqlQuery(WhereField whereField, string from, int limit = 1)
        {
            if (DynamicConfig.PasswordToken != null && Token == null)
                Token = DynamicConfig.PasswordToken;

            WhereFields = new List<WhereField>() { whereField };
            From = from;
            Limit = limit;
        }

        public SoqlQuery(string returnField, WhereField whereField, string from, int limit = 1)
        {
            if (DynamicConfig.PasswordToken != null && Token == null)
                Token = DynamicConfig.PasswordToken;

            ReturnFields = new List<string>() { returnField };
            WhereFields = new List<WhereField>() { whereField };
            From = from;
            Limit = limit;
        }
        #endregion

        public string GetId()
        {
            try
            {
                string emptyId = "";
                //setup request
                ReturnFields = new List<string>();
                BuildQuery();
                RequestUrl = string.Format("{0}{1}/query?q={2}", Token.InstanceUrl, Token.Version.Url, Query);
                HeaderValues.Add("Authorization", string.Format("{0} {1}", Token.TokenType, Token.Token));
                SerializableProperties = new List<string>();

                //make request
                string response = GetRequest();
                if (string.IsNullOrWhiteSpace(response))
                    return emptyId;

                JToken jTok = JObject.Parse(response);

                JToken records = jTok.SelectToken("records");
                JObject jObj = (JObject)records.First;

                JProperty jProp = jObj.Property("Id");
                if (jProp != null)
                    return jProp.Value.ToString();
                else
                    return emptyId;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public Dictionary<string, string> GetFields()
        {
            Dictionary<string, string> fields = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            bool notNull = false;
            try
            {
                //setup request
                BuildQuery();
                RequestUrl = string.Format("{0}{1}/query?q={2}", Token.InstanceUrl, Token.Version.Url, Query);
                HeaderValues.Add("Authorization", string.Format("{0} {1}", Token.TokenType, Token.Token));
                SerializableProperties = new List<string>();

                //make request
                string response = GetRequest();

                JToken jTok = JObject.Parse(response);

                JToken records = jTok.SelectToken("records");
                JObject primaryRecord = (JObject)records.First;

                //get only 1 sub record
                JObject subRecord = new JObject();
                foreach (JToken subToken in records.First)
                {
                    JToken subRecordToken = subToken.First();
                    if (subRecordToken != null)
                    {
                        if (subRecordToken.SelectToken("attributes") != null)
                        {
                            subRecord = (JObject)subRecordToken;
                            break;
                        }
                    }
                }

                foreach (string field in ReturnFields)
                {
                    JProperty jProp = primaryRecord.Property(field);
                    //if null, check if we have an expected prop in the subRecord before continuing
                    if (jProp == null)
                    {
                        if (subRecord != null)
                        {
                            string subField = field;
                            if (field.Contains("."))
                                subField = field.Substring(subField.IndexOf(".") + 1);
                            jProp = subRecord.Property(subField);
                            //if (jProp == null)
                            //    continue;
                        }
                        //else
                        //    continue;
                    }

                    if (jProp == null)
                        fields.Add(field, null);
                    else
                    {
                        fields.Add(field, jProp.Value.ToString());
                        //we do have a value somewhere, so this should not be an object filled with all nulls
                        notNull = true;
                    }
                }
            }
            catch (Exception ex)
            {
                //default fields to make sure there are expected keys even if null
                foreach (string field in ReturnFields)
                    fields.Add(field, null);
            }
            if (notNull)
                return fields;
            else
                return new Dictionary<string, string>();
        }

        public List<T> GetObjects<T>()
        {
            List<T> results = new List<T>();
            try
            {
                //setup request
                BuildQuery(true);
                RequestUrl = string.Format("{0}{1}/query?q={2}", Token.InstanceUrl, Token.Version.Url, Query);
                HeaderValues.Add("Authorization", string.Format("{0} {1}", Token.TokenType, Token.Token));
                SerializableProperties = new List<string>();

                //make request
                string response = GetRequest();

                JToken jTok = JObject.Parse(response);
                JToken records = jTok.SelectToken("records");

                foreach (JToken record in records)
                {
                    T currentObj = (T)typeof(T).GetConstructor(new Type[] { }).Invoke(new object[] { }); ;
                    if (record != null)
                    {
                        if (record.SelectToken("attributes") != null)
                        {
                            JObject jObj = (JObject)record;

                            PropertyInfo[] properties = typeof(T).GetProperties();

                            foreach (PropertyInfo prop in properties)
                            {
                                JsonPropertyAttribute jsonProp = prop.GetCustomAttribute<JsonPropertyAttribute>();

                                if (jsonProp != null)
                                {
                                    JProperty jProp = jObj.Property(jsonProp.PropertyName);
                                    if (jProp != null)
                                    {
                                        if (prop.PropertyType == typeof(String))
                                            prop.SetValue(currentObj, jProp.Value.ToString(), null);
                                        if (prop.PropertyType == typeof(Decimal?))
                                            prop.SetValue(currentObj, _Tools.ToNullableDecimal(jProp.Value), null);
                                        if (prop.PropertyType == typeof(Decimal))
                                            prop.SetValue(currentObj, _Tools.ToDecimal(jProp.Value), null);
                                        if (prop.PropertyType == typeof(Int32))
                                            prop.SetValue(currentObj, _Tools.ToInt(jProp.Value), null);
                                        if (prop.PropertyType == typeof(DateTime))
                                            prop.SetValue(currentObj, _Tools.ToDateTime(jProp.Value), null);
                                    }
                                }
                            }
                            results.Add(currentObj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return results;
        }

        public T Execute<T>()
        {
            try
            {
                //setup request
                ReturnFields = new List<string>();
                BuildQuery();
                RequestUrl = string.Format("{0}{1}/query?q={2}", Token.InstanceUrl, Token.Version.Url, Query);
                HeaderValues.Add("Authorization", string.Format("{0} {1}", Token.TokenType, Token.Token));
                SerializableProperties = new List<string>();

                //make request
                string response = GetRequest();

                JToken jTok = JObject.Parse(response);

                JToken records = jTok.SelectToken("records");
                JObject jObj = (JObject)records.First;

                return JsonHelper.JSONDecode<T>(jObj.ToString());
            }
            catch (Exception ex)
            {
                return (T)typeof(T).GetConstructor(new Type[] { }).Invoke(new object[] { });
            }
        }

        public void BuildQuery(bool noLimit = false)
        {
            if (string.IsNullOrWhiteSpace(Query))
                if (noLimit)
                        Query = string.Format("{0} {1}", BuildSelectFields(ReturnFields, From), BuildWhere(WhereFields));
                else
                    Query = string.Format("{0} {1} limit {2}", BuildSelectFields(ReturnFields, From), BuildWhere(WhereFields), Limit);
        }

        public string BuildSelectFields(List<string> selectFields, string sObjectName)
        {
            if (selectFields.Count < 1)
                return "SELECT id FROM " + sObjectName;

            string selectString = "SELECT " + selectFields.ElementAt(0);
            for (int i = 1; i < selectFields.Count; i++)
                selectString += "," + selectFields.ElementAt(i);
                
            selectString += " FROM " + sObjectName;

            return selectString;
        }

        public string BuildWhere(List<WhereField> whereFields)
        {
            if (whereFields.Count < 1)
                return "";

            if (whereFields.ElementAt(0).Operator.ToLower().Contains("in"))
                whereFields.ElementAt(0).Value = "(" + whereFields.ElementAt(0).Value + ")";

            String where = string.Format("WHERE {0} {1} {2}", whereFields.ElementAt(0).Name, whereFields.ElementAt(0).Operator, whereFields.ElementAt(0).Value);
            for (int i = 1; i < whereFields.Count; i++)
            {
                if (whereFields.ElementAt(i).Operator.ToLower().Contains("in"))
                    whereFields.ElementAt(i).Value = "(" + whereFields.ElementAt(i).Value + ")";

                where += string.Format(" and {0} {1} {2}", whereFields.ElementAt(i).Name, whereFields.ElementAt(i).Operator, whereFields.ElementAt(i).Value);
            }

            return where;
        }

        public class Response
        {
            [JsonProperty("records")]
            List<string> Records = new List<string>();

            public Response()
            {

            }
        }
    }

    public class WhereField
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public bool IncludeQuotes { get; set; }
        public string Operator { get; set; }

        public WhereField()
        {

        }

        public WhereField(string name, string value, bool includeQuotes = true, string soqlOperator = "=")
        {
            Name = name;
            Value = value;
            Operator = soqlOperator;
            //this is not great: thanks httpresponsemessage, great url encoding!
            if (!string.IsNullOrWhiteSpace(Value))
            {
                if (Value.Contains("%"))
                    Value = Value.Replace("%", "%25");
                
                if (Value.Contains("+"))
                    Value = Value.Replace("+", "%2B");
            }

            if (includeQuotes)
                Value = "'" + Value + "'";
        }
    }
}
