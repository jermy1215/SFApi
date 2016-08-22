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
    public class Contact : _RestAdapter, SFObjectModel
    {
        [JsonProperty(PropertyName = "Id")]
        public string ID { get; set; }
        [JsonProperty(PropertyName = "AccountId")]
        public string AccountId { get { return _Account.ID; } set { _Account.ID = value; } }
        [JsonProperty(PropertyName = "LastName")]
        public string LastName { get; set; }
        [JsonProperty(PropertyName = "FirstName")]
        public string FirstName { get; set; }
        [JsonProperty(PropertyName = "MailingStreet")]
        public string Street { get; set; }
        [JsonProperty(PropertyName = "MailingCity")]
        public string City { get; set; }
        [JsonProperty(PropertyName = "MailingState")]
        public string State { get; set; }
        [JsonProperty(PropertyName = "MailingPostalCode")]
        public string PostalCode { get; set; }
        [JsonProperty(PropertyName = "MailingCountry")]
        public string Country { get; set; }
        [JsonProperty(PropertyName = "Fax")]
        public string Fax { get; set; }
        [JsonProperty(PropertyName = "Phone")]
        public string Phone { get; set; }
        [JsonProperty(PropertyName = "HomePhone")]
        public string HomePhone { get; set; }
        [JsonProperty(PropertyName = "Email")]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "Title")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "Department")]
        public string Department { get; set; }
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
        public Account _Account = new Account();
        public Account Account
        {
            get { return _Account; }
            set { _Account = value; }
        }
        public OauthToken Token;
        private string Label = "Contact";
        private string SFObjectName = "Contact";

        public Contact()
        {
            if (DynamicConfig.PasswordToken != null && Token == null)
                Token = DynamicConfig.PasswordToken;

            SerializableProperties = Helpers.SerializableProperties.Get(Label);
        }

        public Contact(string id)
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
            GetRequestFill<Contact>();
            if (!string.IsNullOrWhiteSpace(AccountId))
                Account.TryGet();
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
            PostRequestFill<Contact>();
            Get();
        }

        public void Update()
        {
            //handle properties
            SerializableProperties = Helpers.SerializableProperties.Get(Label, "Update");
            RequiredProperties = Helpers.RequiredProperties.Get(Label, "Update");
            Helpers.RequiredProperties.ValidateRequired(this, Label);
            //do not update null values
            Helpers.SerializableProperties.ExcludeNull<Contact>(this, ref SerializableProperties);

            //setup request
            RequestUrl = string.Format("{0}{1}/sobjects/{2}/{3}", Token.InstanceUrl, Token.Version.Url, SFObjectName, ID);
            HeaderValues.Add("Authorization", string.Format("{0} {1}", Token.TokenType, Token.Token));
            JsonRequest = this;

            //make request
            PatchRequestFill<Contact>();
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
            DeleteRequest<Contact>();
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
