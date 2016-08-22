using _Connections;
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
    public class Account : _RestAdapter, SFObjectModel
    {
        [JsonProperty(PropertyName = "Id")]
        public string ID { get; set; }
        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "BillingStreet")]
        public string Street { get; set; }
        [JsonProperty(PropertyName = "BillingCity")]
        public string City { get; set; }
        [JsonProperty(PropertyName = "BillingState")]
        public string State { get; set; }
        [JsonProperty(PropertyName = "BillingCountry")]
        public string Country { get; set; }
        [JsonProperty(PropertyName = "BillingPostalCode")]
        public string PostalCode { get; set; }
        [JsonProperty(PropertyName = "CreatedDate")]
        public string CreatedDate { get; set; }
        [JsonProperty(PropertyName = "CreatedById")]
        public string CreatedById { get; set; }
        [JsonProperty(PropertyName = "LastModifiedDate")]
        public string LastModifiedDate { get; set; }
        [JsonProperty(PropertyName = "LastModifiedById")]
        public string LastModifiedById { get; set; }

        //[JsonProperty(PropertyName = "Id")]
        //private string _ID { get; set; }
        //public string ID { get { return _ID; } set { _ID = value; } }
        //[JsonProperty(PropertyName = "Name")]
        //private string _Name { get; set; }
        //public string AccountName { get { return _Name; } set { _Name = value; } }
        //[JsonProperty(PropertyName = "BillingStreet")]
        //private string _Street { get; set; }
        //public string Street { get { return _Street; } set { _Street = value; } }
        //[JsonProperty(PropertyName = "BillingCity")]
        //private string _City { get; set; }
        //public string City { get { return _City; } set { _City = value; } }
        //[JsonProperty(PropertyName = "BillingState")]
        //private string _State { get; set; }
        //public string State { get { return _State; } set { _State = value; } }
        //[JsonProperty(PropertyName = "BillingCountry")]
        //private string _Country { get; set; }
        //public string Country { get { return _Country; } set { _Country = value; } }
        //[JsonProperty(PropertyName = "BillingPostalCode")]
        //private string _PostalCode { get; set; }
        //public string PostalCode { get { return _PostalCode; } set { _PostalCode = value; } }
        //[JsonProperty(PropertyName = "CreatedDate")]
        //private string _CreatedDate { get; set; }
        //public string CreatedDate { get { return _CreatedDate; } set { _CreatedDate = value; } }
        //[JsonProperty(PropertyName = "CreatedById")]
        //private string _CreatedById { get; set; }
        //public string CreatedById { get { return _CreatedById; } set { _CreatedById = value; } }
        //[JsonProperty(PropertyName = "LastModifiedDate")]
        //private string _LastModifiedDate { get; set; }
        //public string LastModifiedDate { get { return _LastModifiedDate; } set { _LastModifiedDate = value; } }
        //[JsonProperty(PropertyName = "LastModifiedById")]
        //private string _LastModifiedById { get; set; }
        //public string LastModifiedById { get { return _LastModifiedById; } set { _LastModifiedById = value; } }

        public OauthToken Token;
        private string Label = "Account";
        private string SFObjectName = "Account";

        public Account()
        {
            if (DynamicConfig.PasswordToken != null && Token == null)
                Token = DynamicConfig.PasswordToken;

            SerializableProperties = Helpers.SerializableProperties.Get(Label);
        }

        public Account(string id)
        {
            if (DynamicConfig.PasswordToken != null && Token == null)
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

        public void TryDelete()
        {
            try
            {
                Delete();
            }
            catch (Exception ex)
            {
            }
        }
    }
}
