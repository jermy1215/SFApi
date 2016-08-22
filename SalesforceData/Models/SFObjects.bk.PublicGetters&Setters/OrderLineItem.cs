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
    public class OrderLineItem : RestAdapter, SFObjectModel
    {
        [JsonProperty(PropertyName = "id")]
        public string ID { get; set; }
        [JsonProperty("OwnerId")]
        public string OwnerId { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("CurrencyIsoCode")]
        public string CurrencyIsoCode { get; set; }
        [JsonProperty("CreatedDate")]
        public string CreatedDate { get; set; }
        [JsonProperty("CreatedById")]
        public string CreatedById { get; set; }
        [JsonProperty("LastModifiedDate")]
        public string LastModifiedDate { get; set; }
        [JsonProperty("LastModifiedById")]
        public string LastModifiedById { get; set; }
        [JsonProperty("SystemModstamp")]
        public string SystemModstamp { get; set; }
        [JsonProperty("Order_Header__c")]
        public OrderHeader OrderHeader { get; set; }
        [JsonProperty("External_Order_Detail_ID__c")]
        public string ExternalOrderDetailID { get; set; }
        [JsonProperty("Product__c")]
        public string Product { get; set; }
        [JsonProperty("Product_Code__c")]
        public string ProductCode { get; set; }
        [JsonProperty("Calculated_Discount__c")]
        public double CalculatedDiscount { get; set; }
        [JsonProperty("Quantity__c")]
        public double Quantity { get; set; }
        [JsonProperty("List_Price__c")]
        public double ListPrice { get; set; }
        [JsonProperty("Discount__c")]
        public double Discount { get; set; }
        [JsonProperty("Prepay_Owner__c")]
        public object PrepayOwner { get; set; }
        [JsonProperty("Tax__c")]
        public double Tax { get; set; }
        [JsonProperty("Promo_Code__c")]
        public object PromoCode { get; set; }
        [JsonProperty("JB_Load_Date__c")]
        public string JBLoadDate { get; set; }
        [JsonProperty("JB_Action_Type__c")]
        public string JBActionType { get; set; }
        [JsonProperty("Event__c")]
        public string Event { get; set; }
        [JsonProperty("Sold_To_Account__c")]
        public object SoldToAccount { get; set; }
        [JsonProperty("Location__c")]
        public string Location { get; set; }
        [JsonProperty("Product_Type__c")]
        public string ProductType { get; set; }
        [JsonProperty("Date_to_Invoice__c")]
        public object DateToInvoice { get; set; }
        [JsonProperty("Commission_Treatment__c")]
        public object CommissionTreatment { get; set; }
        [JsonProperty("Total_Price__c")]
        public double TotalPrice { get; set; }
        [JsonProperty("Course_Version__c")]
        public object CourseVersion { get; set; }
        [JsonProperty("Delivery_Provider__c")]
        public string DeliveryProvider { get; set; }
        [JsonProperty("Delivery_Format__c")]
        public string DeliveryFormat { get; set; }
        [JsonProperty("Selling_Price__c")]
        public double SellingPrice { get; set; }
        [JsonProperty("Country__c")]
        public string Country { get; set; }
        [JsonProperty("Enrollment_Options__c")]
        public string EnrollmentOptions { get; set; }
        [JsonProperty("Available_Seats__c")]
        public double AvailableSeats { get; set; }
        [JsonProperty("Event_Status__c")]
        public string EventStatus { get; set; }
        [JsonProperty("VAT__c")]
        public object VAT { get; set; }
        [JsonProperty("VAT_Rate__c")]
        public object VATRate { get; set; }
        [JsonProperty("Web_Registration__c")]
        public bool WebRegistration { get; set; }
        [JsonProperty("Fee_Type__c")]
        public string FeeType { get; set; }
        [JsonProperty("Product_Family__c")]
        public string ProductFamily { get; set; }
        [JsonProperty("Allow_Price_Override__c")]
        public bool AllowPriceOverride { get; set; }
        [JsonProperty("External_Event_ID__c")]
        public string ExternalEventID { get; set; }
        [JsonProperty("Discount_Percent__c")]
        public object DiscountPercent { get; set; }
        [JsonProperty("Start_Date_Time__c")]
        public string StartDateTime { get; set; }
        [JsonProperty("End_Date_Time__c")]
        public string EndDateTime { get; set; }
        [JsonProperty("External_Order_ID__c")]
        public object ExternalOrderID { get; set; }
        [JsonProperty("FX_Rate__c")]
        public object FXRate { get; set; }
        [JsonProperty("Tax_Type__c")]
        public object TaxType { get; set; }
        public OauthToken Token;
        private string Label = "OrderLineItem";
        private string SFObjectName = "Order_Line_Item__c";

        public OrderLineItem()
        {
            if (DynamicConfig.PasswordToken != null)
                Token = DynamicConfig.PasswordToken;

            SerializableProperties = Helpers.SerializableProperties.Get(Label);
        }

        public OrderLineItem(string id)
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
            GetRequestFill<OrderLineItem>();
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
            PostRequestFill<OrderLineItem>();
            Get();
        }

        public void Update()
        {
            //handle properties
            SerializableProperties = Helpers.SerializableProperties.Get(Label, "Update");
            RequiredProperties = Helpers.RequiredProperties.Get(Label, "Update");
            Helpers.RequiredProperties.ValidateRequired(this, Label);
            //do not update null values
            Helpers.SerializableProperties.ExcludeNull<OrderLineItem>(this, ref SerializableProperties);

            //setup request
            RequestUrl = string.Format("{0}{1}/sobjects/{2}/{3}", Token.InstanceUrl, Token.Version.Url, SFObjectName, ID);
            HeaderValues.Add("Authorization", string.Format("{0} {1}", Token.TokenType, Token.Token));
            JsonRequest = this;

            //make request
            PatchRequestFill<OrderLineItem>();
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
            DeleteRequest<OrderLineItem>();
        }
    }
}
