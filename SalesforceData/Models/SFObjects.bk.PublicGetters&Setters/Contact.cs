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
    public class Contact : RestAdapter, SFObjectModel
    {
        [JsonProperty(PropertyName = "id")]
        private string _ID;
        [JsonProperty(PropertyName = "AccountId")]
        private string _AccountId;
        [JsonProperty(PropertyName = "LastName")]
        private string _LastName;
        [JsonProperty(PropertyName = "FirstName")]
        private string _FirstName;
        [JsonProperty(PropertyName = "MailingStreet")]
        private string _Street;
        [JsonProperty(PropertyName = "MailingCity")]
        private string _City;
        [JsonProperty(PropertyName = "MailingState")]
        private string _State;
        [JsonProperty(PropertyName = "MailingPostalCode")]
        private string _PostalCode;
        [JsonProperty(PropertyName = "MailingCountry")]
        private string _Country;
        [JsonProperty(PropertyName = "Phone")]
        private string _Phone;
        [JsonProperty(PropertyName = "Fax")]
        private string _Fax;
        [JsonProperty(PropertyName = "MobilePhone")]
        private string _MobilePhone;
        [JsonProperty(PropertyName = "HomePhone")]
        private string _HomePhone;
        [JsonProperty(PropertyName = "Email")]
        private string _Email;
        [JsonProperty(PropertyName = "Title")]
        private string _Title;
        [JsonProperty(PropertyName = "Department")]
        private string _Department;
        [JsonProperty(PropertyName = "LeadSource")]
        private string _LeadSource;
        [JsonProperty(PropertyName = "Description")]
        private string _Description;
        [JsonProperty(PropertyName = "CurrencyIsoCode")]
        private string _CurrencyCode;
        [JsonProperty(PropertyName = "CreatedDate")]
        private string _CreatedDate;
        [JsonProperty(PropertyName = "CreatedById")]
        private string _CreatedById;
        [JsonProperty(PropertyName = "LastModifiedDate")]
        private string _LastModifiedDate;
        [JsonProperty(PropertyName = "LastModifiedById")]
        private string _LastModifiedById;
        public Account _Account = new Account();
        public Account Account
        {
            get { return _Account; }
            set { _Account = value; }
        }
        public OauthToken Token;
        private string Label = "Contact";
        private string SFObjectName = "Contact";

        #region public getters & setters
        public string ID { get { return _ID; } set { _ID = value; } }
        public string AccountId { get { return _AccountId; } set { _AccountId = value; } }
        public string LastName { get { return _LastName; } set { _LastName = value; } }
        public string FirstName { get { return _FirstName; } set { _FirstName = value; } }
        public string Street { get { return _Street; } set { _Street = value; } }
        public string City { get { return _City; } set { _City = value; } }
        public string State { get { return _State; } set { _State = value; } }
        public string PostalCode { get { return _PostalCode; } set { _PostalCode = value; } }
        public string Country { get { return _Country; } set { _Country = value; } }
        public string Phone { get { return _Phone; } set { _Phone = value; } }
        public string Fax { get { return _Fax; } set { _Fax = value; } }
        public string MobilePhone { get { return _MobilePhone; } set { _MobilePhone = value; } }
        public string HomePhone { get { return _HomePhone; } set { _HomePhone = value; } }
        public string Email { get { return _Email; } set { _Email = value; } }
        public string Title { get { return _Title; } set { _Title = value; } }
        public string Department { get { return _Department; } set { _Department = value; } }
        public string LeadSource { get { return _LeadSource; } set { _LeadSource = value; } }
        public string Description { get { return _Description; } set { _Description = value; } }
        public string CurrencyCode { get { return _CurrencyCode; } set { _CurrencyCode = value; } }
        public string CreatedDate { get { return _CreatedDate; } set { _CreatedDate = value; } }
        public string CreatedById { get { return _CreatedById; } set { _CreatedById = value; } }
        public string LastModifiedDate { get { return _LastModifiedDate; } set { _LastModifiedDate = value; } }
        public string LastModifiedById { get { return _LastModifiedById; } set { _LastModifiedById = value; } }
        #endregion

        public Contact()
        {
            if (DynamicConfig.PasswordToken != null)
                Token = DynamicConfig.PasswordToken;

            SerializableProperties = Helpers.SerializableProperties.Get(Label);
        }

        public Contact(string id)
        {
            if (DynamicConfig.PasswordToken != null)
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
    }
}
