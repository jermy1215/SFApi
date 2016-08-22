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
    public class Lead : _RestAdapter, SFObjectModel
    {
        [JsonProperty(PropertyName = "Id")]
        public string ID { get; set; }
        [JsonProperty(PropertyName = "Account__c")]
        public string AccountId { get; set; }
        [JsonProperty(PropertyName = "Company")]
        public string Company { get; set; }
        [JsonProperty(PropertyName = "LastName")]
        public string LastName { get; set; }
        [JsonProperty(PropertyName = "FirstName")]
        public string FirstName { get; set; }
        [JsonProperty(PropertyName = "Street")]
        public string Street { get; set; }
        [JsonProperty(PropertyName = "City")]
        public string City { get; set; }
        [JsonProperty(PropertyName = "State")]
        public string State { get; set; }
        [JsonProperty(PropertyName = "PostalCode")]
        public string PostalCode { get; set; }
        [JsonProperty(PropertyName = "Country")]
        public string Country { get; set; }
        [JsonProperty(PropertyName = "Phone")]
        public string Phone { get; set; }
        [JsonProperty(PropertyName = "MobilePhone")]
        public string MobilePhone { get; set; }
        [JsonProperty(PropertyName = "Email")]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "Title")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "LeadSource")]
        public string LeadSource { get; set; }
        [JsonProperty(PropertyName = "CurrencyIsoCode")]
        public string CurrencyCode { get; set; }
        [JsonProperty(PropertyName = "CreatedDate")]
        public string CreatedDate { get; set; }
        [JsonProperty(PropertyName = "CreatedById")]
        public string CreatedById { get; set; }
        [JsonProperty(PropertyName = "LastModifiedDate")]
        public string LastModifiedDate { get; set; }
        [JsonProperty(PropertyName = "LastModifiedById")]
        public string LastModifiedById { get; set; }
        public OauthToken Token;
        private string Label = "Lead";
        private string SFObjectName = "Lead";

        public Lead()
        {
            if (DynamicConfig.PasswordToken != null && Token == null)
                Token = DynamicConfig.PasswordToken;

            SerializableProperties = Helpers.SerializableProperties.Get(Label);
        }

        public Lead(string id)
        {
            if (DynamicConfig.PasswordToken != null && Token == null)
                Token = DynamicConfig.PasswordToken;

            ID = id;
            SerializableProperties = Helpers.SerializableProperties.Get(Label);
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
            GetRequestFill<Lead>();
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
            PostRequestFill<Lead>();
            Get();
        }

        public void Update()
        {
            //handle properties
            SerializableProperties = Helpers.SerializableProperties.Get(Label, "Update");
            RequiredProperties = Helpers.RequiredProperties.Get(Label, "Update");
            Helpers.RequiredProperties.ValidateRequired(this, Label);
            //do not update null values
            Helpers.SerializableProperties.ExcludeNull<Lead>(this, ref SerializableProperties);

            //setup request
            RequestUrl = string.Format("{0}{1}/sobjects/{2}/{3}", Token.InstanceUrl, Token.Version.Url, SFObjectName, ID);
            HeaderValues.Add("Authorization", string.Format("{0} {1}", Token.TokenType, Token.Token));
            JsonRequest = this;

            //make request
            PatchRequestFill<Lead>();
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
            DeleteRequest<Lead>();
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
