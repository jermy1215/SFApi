using GKConnections;
using SalesforceData.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SalesforceData
{
    public class Account : RestAdapter, SFObjectModel
    {
        [JsonProperty(PropertyName = "id")]
        private string _ID;
        [JsonProperty(PropertyName = "Name")]
        private string _Name;
        [JsonProperty(PropertyName = "BillingStreet")]
        private string _Street;
        [JsonProperty(PropertyName = "BillingCity")]
        private string _City;
        [JsonProperty(PropertyName = "BillingState")]
        private string _State;
        [JsonProperty(PropertyName = "BillingCountry")]
        private string _Country;
        [JsonProperty(PropertyName = "BillingPostalCode")]
        private string _PostalCode;
        [JsonProperty(PropertyName = "CreatedDate")]
        private string _CreatedDate;
        [JsonProperty(PropertyName = "CreatedById")]
        private string _CreatedById;
        [JsonProperty(PropertyName = "LastModifiedDate")]
        private string _LastModifiedDate;
        [JsonProperty(PropertyName = "LastModifiedById")]
        private string _LastModifiedById;
        public OauthToken Token;
        private string Label = "Account";
        private string SFObjectName = "Account";

        #region public getters & setters
		public string ID { get { return _ID; } set { _ID = value; } }
		public string Name { get { return _Name; } set { _Name = value; } }
		public string Street { get { return _Street; } set { _Street = value; } }
		public string City { get { return _City; } set { _City = value; } }
		public string State { get { return _State; } set { _State = value; } }
		public string Country { get { return _Country; } set { _Country = value; } }
		public string PostalCode { get { return _PostalCode; } set { _PostalCode = value; } }
		public string CreatedDate { get { return _CreatedDate; } set { _CreatedDate = value; } }
		public string CreatedById { get { return _CreatedById; } set { _CreatedById = value; } }
		public string LastModifiedDate { get { return _LastModifiedDate; } set { _LastModifiedDate = value; } }
		public string LastModifiedById { get { return _LastModifiedById; } set { _LastModifiedById = value; } }
		#endregion

        public Account()
        {
            if (DynamicConfig.PasswordToken != null)
                Token = DynamicConfig.PasswordToken;

            SerializableProperties = Helpers.SerializableProperties.Get(Label);
        }

        public Account(string id)
        {
            if (DynamicConfig.PasswordToken != null)
                Token = DynamicConfig.PasswordToken;

            ID = id;
            TryGet();
        }

        public void TryGet()
        {
            try
            {
                Get();
            }
            catch (Exception ex)
            {
                ID = null;
            }
        }

        public void Get()
        {
            //handle properties
            SerializableProperties = Helpers.SerializableProperties.Get(Label);
            RequiredProperties = Helpers.RequiredProperties.Get(Label, "Get");
            Helpers.RequiredProperties.ValidateRequired(this, Label);
            
            //setup request
            RequestUrl = string.Format("{0}{1}/sobjects/{2}/{3}", Token.InstanceUrl, Token.Version.Url, SFObjectName, ID);
            HeaderValues.Add("Authorization", string.Format("{0} {1}", Token.TokenType, Token.Token));
            
            //make request
            GetRequestFill<Account>();
        }

        public void Create()
        {
            //handle properties
            SerializableProperties = Helpers.SerializableProperties.Get(Label, "Create");
            RequiredProperties = Helpers.RequiredProperties.Get(Label, "Create");
            Helpers.RequiredProperties.ValidateRequired(this, Label);

            //setup request
            RequestUrl = string.Format("{0}{1}/sobjects/{2}", Token.InstanceUrl, Token.Version.Url, SFObjectName);
            HeaderValues.Add("Authorization", string.Format("{0} {1}", Token.TokenType, Token.Token));
            JsonRequest = this;

            //make request
            PostRequestFill<Account>();
            Get();
        }

        public void Update()
        {
            //handle properties
            SerializableProperties = Helpers.SerializableProperties.Get(Label, "Update");
            RequiredProperties = Helpers.RequiredProperties.Get(Label, "Update");
            Helpers.RequiredProperties.ValidateRequired(this, Label);
            //do not update null values
            Helpers.SerializableProperties.ExcludeNull<Account>(this, ref SerializableProperties);

            //setup request
            RequestUrl = string.Format("{0}{1}/sobjects/{2}/{3}", Token.InstanceUrl, Token.Version.Url, SFObjectName, ID);
            HeaderValues.Add("Authorization", string.Format("{0} {1}", Token.TokenType, Token.Token));
            JsonRequest = this;

            //make request
            PatchRequestFill<Account>();
            Get();
        }

        public void Delete()
        {
            //handle properties
            RequiredProperties = Helpers.RequiredProperties.Get(Label, "Delete");
            Helpers.RequiredProperties.ValidateRequired(this, Label);

            //setup request
            RequestUrl = string.Format("{0}{1}/sobjects/{2}/{3}", Token.InstanceUrl, Token.Version.Url, SFObjectName, ID);
            HeaderValues.Add("Authorization", string.Format("{0} {1}", Token.TokenType, Token.Token));
            
            //make request
            DeleteRequest<Account>();
        }
    }
}
