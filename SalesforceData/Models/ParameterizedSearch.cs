using _Connections;
using SalesforceData.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesforceData
{
    public class ParameterizedSearch : _RestAdapter
    {
        //inputs
        [JsonProperty(PropertyName = "q")]
        public string Query { get; set; }
        [JsonProperty(PropertyName = "sobjects")]
        public List<SObject> SObjects { get; set; }
        [JsonProperty(PropertyName = "fields")]
        public List<string> GlobalFields { get; set; }
        public int _GlobalLimit = 10;
        [JsonProperty(PropertyName = "overallLimit")]
        public int GlobalLimit
        {
            get { return _GlobalLimit; }
            set { _GlobalLimit = value; }
        }
        public int _DefaultLimit = 10;
        [JsonProperty(PropertyName = "defaultLimit")]
        public int DefaultLimit
        {
            get { return _DefaultLimit; }
            set { _DefaultLimit = value; }
        }
        public bool ResultsReturned = false;
        private OauthToken Token;

        #region initializers
        /// <summary>
        /// Basic initialization. Token and Version are needed, so if this initilizaiton is chosen, a Token and Version must be supplied before executing a query.
        /// </summary>
        public ParameterizedSearch()
        {
            GlobalFields = new List<string>();
        }

        /// <summary>
        /// Empty search initialization. Only basic Token and Version are set along with default limits.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="version"></param>
        public ParameterizedSearch(OauthToken token)
        {
            Token = token;
            GlobalFields = new List<string>();
        }

        /// <summary>
        /// Full initialization with all options set.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="query"></param>
        /// <param name="sObjects"></param>
        public ParameterizedSearch(OauthToken token, string query, List<SObject> sObjects)
        {
            Token = token;
            Query = query;
            SObjects = sObjects;
        }

        /// <summary>
        /// Single sObject full initialization with all options set.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="query"></param>
        /// <param name="sObject"></param>
        public ParameterizedSearch(OauthToken token, string query, SObject sObject)
        {
            Token = token;
            Query = query;
            SObjects = new List<SObject>() { sObject };
        }

        /// <summary>
        /// Flat sObject, multiple return fields, where dictionary.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="query"></param>
        /// <param name="name"></param>
        /// <param name="returnFields"></param>
        /// <param name="where"></param>
        /// <param name="limit"></param>
        public ParameterizedSearch(OauthToken token, string query, string name, List<string> returnFields, Dictionary<string, string> where, int limit = 10)
        {
            Token = token;
            Query = query;
            SObjects = new List<SObject>() { new SObject(returnFields, name, limit, where) };
        }

        /// <summary>
        /// Flat sObject, multiple return fields.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="query"></param>
        /// <param name="name"></param>
        /// <param name="returnFields"></param>
        /// <param name="where"></param>
        /// <param name="limit"></param>
        public ParameterizedSearch(OauthToken token, string query, string name, List<string> returnFields, string where, int limit = 10)
        {
            Token = token;
            Query = query;
            SObjects = new List<SObject>() { new SObject(returnFields, name, limit, where) };
        }

        /// <summary>
        /// Flat sObject, single return field, where dictionary.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="query"></param>
        /// <param name="name"></param>
        /// <param name="returnField"></param>
        /// <param name="where"></param>
        /// <param name="limit"></param>
        public ParameterizedSearch(OauthToken token, string query, string name, string returnField, Dictionary<string, string> where, int limit = 10)
        {
            Token = token;
            Query = query;
            SObjects = new List<SObject>() { new SObject(returnField, name, limit, where) };
        }

        /// <summary>
        /// Flat sObject, single return field.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="query"></param>
        /// <param name="name"></param>
        /// <param name="returnField"></param>
        /// <param name="where"></param>
        /// <param name="limit"></param>
        public ParameterizedSearch(OauthToken token, string query, string name, string returnField, string where, int limit = 10)
        {
            Token = token;
            Query = query;
            SObjects = new List<SObject>() { new SObject(returnField, name, limit, where) };
        }
        #endregion

        public T Execute<T>()
        {
            try
            {
                //setup request
                RequestUrl = string.Format("{0}{1}/parameterizedSearch", Token.InstanceUrl, Token.Version.Url);
                HeaderValues.Add("Authorization", string.Format("{0} {1}", Token.TokenType, Token.Token));
                JsonRequest = this;
                SerializableProperties = new List<string>()
                {
                    "q", "fields", "sobjects", "name", "limit", "where", "overallLimit", "defaultLimit"
                };

                Helpers.SerializableProperties.ExcludeNull<ParameterizedSearch>(this, ref SerializableProperties);

                //make request
                return PostRequest<T>();
            }
            catch (Exception ex)
            {
                return (T)typeof(T).GetConstructor(new Type[] { }).Invoke(new object[] { });
            }
        }

        public class SObject
        {
            [JsonProperty(PropertyName = "fields")]
            public List<string> ReturnFields { get; set; }
            [JsonProperty(PropertyName = "name")]
            public string Name { get; set; }
            [JsonProperty(PropertyName = "limit")]
            public int Limit { get; set; }
            [JsonProperty(PropertyName = "where")]
            public string Where { get; set; }

            public SObject()
            {

            }

            public SObject(List<string> returnFields, string sObjectName, int sObjectLimit)
            {
                ReturnFields = returnFields;
                Name = sObjectName;
                Limit = sObjectLimit;
            }

            public SObject(List<string> returnFields, string sObjectName, int sObjectLimit, string where)
            {
                ReturnFields = returnFields;
                Name = sObjectName;
                Limit = sObjectLimit;
                Where = where;
            }

            public SObject(List<string> returnFields, string sObjectName, int sObjectLimit, Dictionary<string, string> where)
            {
                ReturnFields = returnFields;
                Name = sObjectName;
                Limit = sObjectLimit;
                BuildWhere(where);
            }

            public SObject(string returnField, string sObjectName, int sObjectLimit, string where)
            {
                ReturnFields = new List<string>() { returnField };
                Name = sObjectName;
                Limit = sObjectLimit;
                Where = where;
            }

            public SObject(string returnField, string sObjectName, int sObjectLimit, Dictionary<string, string> where)
            {
                ReturnFields = new List<string>() { returnField };
                Name = sObjectName;
                Limit = sObjectLimit;
                BuildWhere(where);
            }

            public void BuildWhere(Dictionary<string, string> where)
            {
                if (where.Count < 1)
                    return;

                Where = string.Format("{0} = '{1}'", where.ElementAt(0).Key, where.ElementAt(0).Value);
                for (int i = 1; i < where.Count; i++)
                    Where += string.Format(" and {0} = '{1}'", where.ElementAt(i).Key, where.ElementAt(i).Value);
            }
        }
    }
}
