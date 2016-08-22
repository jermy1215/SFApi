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
    public class OrderHeader : RestAdapter, SFObjectModel
    {
        [JsonProperty(PropertyName = "id")]
        private string _ID;
        [JsonProperty("OwnerId")]
        private string _OwnerId;
        [JsonProperty("Name")]
        private string _Name;
        [JsonProperty("CurrencyIsoCode")]
        private string _CurrencyIsoCode;
        [JsonProperty("CreatedDate")]
        private string _CreatedDate;
        [JsonProperty("CreatedById")]
        private string _CreatedById;
        [JsonProperty("LastModifiedDate")]
        private string _LastModifiedDate;
        [JsonProperty("LastModifiedById")]
        private string _LastModifiedById;
        [JsonProperty("SystemModstamp")]
        private string _SystemModstamp;
        [JsonProperty("LastViewedDate")]
        private string _LastViewedDate;
        [JsonProperty("LastReferencedDate")]
        private string _LastReferencedDate;
        [JsonProperty("External_Order_Header_ID__c")]
        private string _ExternalOrderHeaderID;
        [JsonProperty("Opportunity__c")]
        private string _Opportunity;
        [JsonProperty("Selling_Country__c")]
        private string _SellingCountry;
        [JsonProperty("Sold_To_Street__c")]
        private string _SoldToStreet;
        [JsonProperty("Sold_To_City__c")]
        private string _SoldToCity;
        [JsonProperty("Sold_To_State_Province__c")]
        private string _SoldToStateProvince;
        [JsonProperty("Sold_To_Zip_Postal_Code__c")]
        private string _SoldToZipPostalCode;
        [JsonProperty("Sold_To_Country__c")]
        private string _SoldToCountry;
        [JsonProperty("Sold_To_Company__c")]
        private string _SoldToCompany;
        [JsonProperty("Bill_To_Company__c")]
        private string _BillToCompany;
        [JsonProperty("Bill_To_Street__c")]
        private string _BillToStreet;
        [JsonProperty("Bill_To_City__c")]
        private string _BillToCity;
        [JsonProperty("Bill_To_State_Province__c")]
        private string _BillToStateProvince;
        [JsonProperty("Bill_To_Zip_Postal_Code__c")]
        private string _BillToZipPostalCode;
        [JsonProperty("Bill_To_Country__c")]
        private string _BillToCountry;
        [JsonProperty("Partner_Code__c")]
        private string _PartnerCode;
        [JsonProperty("External_PrePayment_Card_ID__c")]
        private string _ExternalPrePaymentCardID;
        [JsonProperty("Bill_To_Address_Number__c")]
        private string _BillToAddressNumber;
        [JsonProperty("Sold_To_Address_Number__c")]
        private string _SoldToAddressNumber;
        [JsonProperty("JB_Load_Date__c")]
        private string _JBLoadDate;
        [JsonProperty("JB_Action_Type__c")]
        private string _JBActionType;
        [JsonProperty("Currency__c")]
        private string _Currency;
        [JsonProperty("Description__c")]
        private string _Description;
        [JsonProperty("Order_Source__c")]
        private string _OrderSource;
        [JsonProperty("Prepay_Source__c")]
        private string _PrepaySource;
        [JsonProperty("Order_Date__c")]
        private string _OrderDate;
        [JsonProperty("Order_Number__c")]
        private string _OrderNumber;
        [JsonProperty("Order_Status__c")]
        private string _OrderStatus;
        [JsonProperty("Cancel_Reason__c")]
        private string _CancelReason;
        [JsonProperty("Transfer_Charge__c")]
        private string _TransferCharge;
        [JsonProperty("Cancellation_Charge__c")]
        private string _CancellationCharge;
        [JsonProperty("Business_Days_To_Start_Date__c")]
        private string _BusinessDaysToStartDate;
        [JsonProperty("Transfer_Reason__c")]
        private string _TransferReason;
        [JsonProperty("Offering_ID__c")]
        private string _OfferingID;
        [JsonProperty("Order_Total__c")]
        private string _OrderTotal;
        [JsonProperty("PrePayment_Number__c")]
        private string _PrePaymentNumber;
        [JsonProperty("Amount_Remaining__c")]
        private string _AmountRemaining;
        [JsonProperty("Manager__c")]
        private string _Manager;
        [JsonProperty("Total_Price__c")]
        private string _TotalPrice;
        [JsonProperty("Order_SubTotal__c")]
        private string _OrderSubTotal;
        [JsonProperty("Payment_Total__c")]
        private string _PaymentTotal;
        [JsonProperty("External_Order_Create_Date__c")]
        private string _ExternalOrderCreateDate;
        [JsonProperty("External_Order_Modify_Date__c")]
        private string _ExternalOrderModifyDate;
        [JsonProperty("Culture_Code__c")]
        private string _CultureCode;
        [JsonProperty("External_Bill_To_Account_ID__c")]
        private string _ExternalBillToAccountID;
        [JsonProperty("External_Bill_To_Address_ID__c")]
        private string _ExternalBillToAddressID;
        [JsonProperty("External_Sold_To_Account_ID__c")]
        private string _ExternalSoldToAccountID;
        [JsonProperty("External_Sold_To_Address_ID__c")]
        private string _ExternalSoldToAddressID;
        [JsonProperty("EIN_VAT__c")]
        private string _EINVATC;
        public OauthToken Token;
        private string Label = "OrderHeader";
        private string SFObjectName = "Order_Header__c";

        #region public getters & setters
        public string ID { get { return _ID; } set { _ID = value; } }
        public string OwnerId { get { return _OwnerId; } set { _OwnerId = value; } }
        public string Name { get { return _Name; } set { _Name = value; } }
        public string CurrencyIsoCode { get { return _CurrencyIsoCode; } set { _CurrencyIsoCode = value; } }
        public string CreatedDate { get { return _CreatedDate; } set { _CreatedDate = value; } }
        public string CreatedById { get { return _CreatedById; } set { _CreatedById = value; } }
        public string LastModifiedDate { get { return _LastModifiedDate; } set { _LastModifiedDate = value; } }
        public string LastModifiedById { get { return _LastModifiedById; } set { _LastModifiedById = value; } }
        public string SystemModstamp { get { return _SystemModstamp; } set { _SystemModstamp = value; } }
        public string LastViewedDate { get { return _LastViewedDate; } set { _LastViewedDate = value; } }
        public string LastReferencedDate { get { return _LastReferencedDate; } set { _LastReferencedDate = value; } }
        public string ExternalOrderHeaderID { get { return _ExternalOrderHeaderID; } set { _ExternalOrderHeaderID = value; } }
        public string Opportunity { get { return _Opportunity; } set { _Opportunity = value; } }
        public string SellingCountry { get { return _SellingCountry; } set { _SellingCountry = value; } }
        public string SoldToStreet { get { return _SoldToStreet; } set { _SoldToStreet = value; } }
        public string SoldToCity { get { return _SoldToCity; } set { _SoldToCity = value; } }
        public string SoldToStateProvince { get { return _SoldToStateProvince; } set { _SoldToStateProvince = value; } }
        public string SoldToZipPostalCode { get { return _SoldToZipPostalCode; } set { _SoldToZipPostalCode = value; } }
        public string SoldToCountry { get { return _SoldToCountry; } set { _SoldToCountry = value; } }
        public string SoldToCompany { get { return _SoldToCompany; } set { _SoldToCompany = value; } }
        public string BillToCompany { get { return _BillToCompany; } set { _BillToCompany = value; } }
        public string BillToStreet { get { return _BillToStreet; } set { _BillToStreet = value; } }
        public string BillToCity { get { return _BillToCity; } set { _BillToCity = value; } }
        public string BillToStateProvince { get { return _BillToStateProvince; } set { _BillToStateProvince = value; } }
        public string BillToZipPostalCode { get { return _BillToZipPostalCode; } set { _BillToZipPostalCode = value; } }
        public string BillToCountry { get { return _BillToCountry; } set { _BillToCountry = value; } }
        public string PartnerCode { get { return _PartnerCode; } set { _PartnerCode = value; } }
        public string ExternalPrePaymentCardID { get { return _ExternalPrePaymentCardID; } set { _ExternalPrePaymentCardID = value; } }
        public string BillToAddressNumber { get { return _BillToAddressNumber; } set { _BillToAddressNumber = value; } }
        public string SoldToAddressNumber { get { return _SoldToAddressNumber; } set { _SoldToAddressNumber = value; } }
        public string JBLoadDate { get { return _JBLoadDate; } set { _JBLoadDate = value; } }
        public string JBActionType { get { return _JBActionType; } set { _JBActionType = value; } }
        public string Currency { get { return _Currency; } set { _Currency = value; } }
        public string Description { get { return _Description; } set { _Description = value; } }
        public string OrderSource { get { return _OrderSource; } set { _OrderSource = value; } }
        public string PrepaySource { get { return _PrepaySource; } set { _PrepaySource = value; } }
        public string OrderDate { get { return _OrderDate; } set { _OrderDate = value; } }
        public string OrderNumber { get { return _OrderNumber; } set { _OrderNumber = value; } }
        public string OrderStatus { get { return _OrderStatus; } set { _OrderStatus = value; } }
        public string CancelReason { get { return _CancelReason; } set { _CancelReason = value; } }
        public string TransferCharge { get { return _TransferCharge; } set { _TransferCharge = value; } }
        public string CancellationCharge { get { return _CancellationCharge; } set { _CancellationCharge = value; } }
        public string BusinessDaysToStartDate { get { return _BusinessDaysToStartDate; } set { _BusinessDaysToStartDate = value; } }
        public string TransferReason { get { return _TransferReason; } set { _TransferReason = value; } }
        public string OfferingID { get { return _OfferingID; } set { _OfferingID = value; } }
        public string OrderTotal { get { return _OrderTotal; } set { _OrderTotal = value; } }
        public string PrePaymentNumber { get { return _PrePaymentNumber; } set { _PrePaymentNumber = value; } }
        public string AmountRemaining { get { return _AmountRemaining; } set { _AmountRemaining = value; } }
        public string Manager { get { return _Manager; } set { _Manager = value; } }
        public string TotalPrice { get { return _TotalPrice; } set { _TotalPrice = value; } }
        public string OrderSubTotal { get { return _OrderSubTotal; } set { _OrderSubTotal = value; } }
        public string PaymentTotal { get { return _PaymentTotal; } set { _PaymentTotal = value; } }
        public string ExternalOrderCreateDate { get { return _ExternalOrderCreateDate; } set { _ExternalOrderCreateDate = value; } }
        public string ExternalOrderModifyDate { get { return _ExternalOrderModifyDate; } set { _ExternalOrderModifyDate = value; } }
        public string CultureCode { get { return _CultureCode; } set { _CultureCode = value; } }
        public string ExternalBillToAccountID { get { return _ExternalBillToAccountID; } set { _ExternalBillToAccountID = value; } }
        public string ExternalBillToAddressID { get { return _ExternalBillToAddressID; } set { _ExternalBillToAddressID = value; } }
        public string ExternalSoldToAccountID { get { return _ExternalSoldToAccountID; } set { _ExternalSoldToAccountID = value; } }
        public string ExternalSoldToAddressID { get { return _ExternalSoldToAddressID; } set { _ExternalSoldToAddressID = value; } }
        public string EINVATC { get { return _EINVATC; } set { _EINVATC = value; } }
        #endregion

        public OrderHeader()
        {
            if (DynamicConfig.PasswordToken != null)
                Token = DynamicConfig.PasswordToken;

            SerializableProperties = Helpers.SerializableProperties.Get(Label);
        }

        public OrderHeader(string id)
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
            GetRequestFill<OrderHeader>();
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
            PostRequestFill<OrderHeader>();
            Get();
        }

        public void Update()
        {
            //handle properties
            SerializableProperties = Helpers.SerializableProperties.Get(Label, "Update");
            RequiredProperties = Helpers.RequiredProperties.Get(Label, "Update");
            Helpers.RequiredProperties.ValidateRequired(this, Label);
            //do not update null values
            Helpers.SerializableProperties.ExcludeNull<OrderHeader>(this, ref SerializableProperties);

            //setup request
            RequestUrl = string.Format("{0}{1}/sobjects/{2}/{3}", Token.InstanceUrl, Token.Version.Url, SFObjectName, ID);
            HeaderValues.Add("Authorization", string.Format("{0} {1}", Token.TokenType, Token.Token));
            JsonRequest = this;

            //make request
            PatchRequestFill<OrderHeader>();
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
            DeleteRequest<OrderHeader>();
        }
    }
}
