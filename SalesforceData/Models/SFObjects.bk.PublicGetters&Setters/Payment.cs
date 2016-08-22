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
    public class Payment : RestAdapter, SFObjectModel
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
        [JsonProperty("LastViewedDate")]
        public object LastViewedDate { get; set; }
        [JsonProperty("LastReferencedDate")]
        public object LastReferencedDate { get; set; }
        [JsonProperty("Order_Header__c")]
        public OrderHeader OrderHeader { get; set; }
        [JsonProperty("Payment_Method__c")]
        public string PaymentMethod { get; set; }
        [JsonProperty("Details__c")]
        public string Details { get; set; }
        [JsonProperty("Confirmation__c")]
        public object Confirmation { get; set; }
        [JsonProperty("Amount_Paid__c")]
        public double AmountPaid { get; set; }
        [JsonProperty("JB_Load_Date__c")]
        public object JBLoadDate { get; set; }
        [JsonProperty("JB_Action_Type__c")]
        public object JBActionType { get; set; }
        [JsonProperty("PrePayment__c")]
        public object PrePayment { get; set; }
        [JsonProperty("Pass_Count__c")]
        public object PassCount { get; set; }
        [JsonProperty("Approval_Confirmation__c")]
        public object ApprovalConfirmation { get; set; }
        [JsonProperty("Approval_Details__c")]
        public object ApprovalDetails { get; set; }
        [JsonProperty("CC_Auth_Code__c")]
        public object CCAuthCode { get; set; }
        [JsonProperty("CC_Merchant_Order_Number__c")]
        public object CCMerchantOrderNumber { get; set; }
        [JsonProperty("CC_Type__c")]
        public object CCType { get; set; }
        [JsonProperty("CC_Expire_Date__c")]
        public object CCExpireDate { get; set; }
        [JsonProperty("CC_Issued_By__c")]
        public object CCIssuedBy { get; set; }
        [JsonProperty("CC_Name_On_Card__c")]
        public object CCNameOnCard { get; set; }
        [JsonProperty("EVO_Token__c")]
        public object EVOToken { get; set; }
        [JsonProperty("EVO_Response_Text__c")]
        public object EVOResponseText { get; set; }
        [JsonProperty("EVO_Response_Code__c")]
        public object EVOResponseCode { get; set; }
        [JsonProperty("First_digit_CC__c")]
        public object FirstDigitCC { get; set; }
        [JsonProperty("Last_4_digit_CC__c")]
        public object Last4DigitCC { get; set; }
        [JsonProperty("Card_Type__c")]
        public object CardType { get; set; }
        [JsonProperty("Expiration_Date__c")]
        public object ExpirationDate { get; set; }
        [JsonProperty("EVO_Transaction_ID__c")]
        public object EVOTransactionID { get; set; }
        [JsonProperty("EVO_Transaction_Date__c")]
        public object EVOTransactionDate { get; set; }
        [JsonProperty("Credit__c")]
        public object Credit { get; set; }
        [JsonProperty("First_Name__c")]
        public object FirstName { get; set; }
        [JsonProperty("Last_Name__c")]
        public string LastName { get; set; }
        [JsonProperty("Bill_To_Account__c")]
        public string BillToAccount { get; set; }
        [JsonProperty("Payment_Type__c")]
        public object PaymentType { get; set; }
        [JsonProperty("Email__c")]
        public object Email { get; set; }
        [JsonProperty("Bill_To_Address__c")]
        public object BillToAddress { get; set; }
        [JsonProperty("Order_Header_Status__c")]
        public string OrderHeaderStatus { get; set; }
        [JsonProperty("Invoice_Notes_Verification__c")]
        public bool InvoiceNotesVerification { get; set; }
        [JsonProperty("Bill_To_City__c")]
        public string BillToCity { get; set; }
        [JsonProperty("Bill_To_Country__c")]
        public string BillToCountry { get; set; }
        [JsonProperty("Bill_To_State_Province__c")]
        public string BillToStateProvince { get; set; }
        [JsonProperty("Bill_To_Street__c")]
        public string BillToStreet { get; set; }
        [JsonProperty("Bill_To_Zip_Postal_Code__c")]
        public string BillToZipPostalCode { get; set; }
        [JsonProperty("Invoice_Notes__c")]
        public string InvoiceNotes { get; set; }
        [JsonProperty("Payment_Status__c")]
        public string PaymentStatus { get; set; }
        [JsonProperty("Cancelled_Enrollment__c")]
        public Enrollment CancelledEnrollment { get; set; }
        [JsonProperty("Corresponding_Adjustment__c")]
        public object CorrespondingAdjustment { get; set; }
        [JsonProperty("Payment_Balance__c")]
        public object PaymentBalance { get; set; }
        [JsonProperty("EIN_VAT__c")]
        public object EINVAT { get; set; }
        [JsonProperty("PrePayment_Card_Type__c")]
        public object PrePaymentCardType { get; set; }
        [JsonProperty("LOI_Authorized2__c")]
        public bool LOIAuthorized2 { get; set; }
        [JsonProperty("Voucher_Authorized2__c")]
        public bool VoucherAuthorized2 { get; set; }
        public OauthToken Token;
        private string Label = "Payment";
        private string SFObjectName = "Payment__c";

        public Payment()
        {
            if (DynamicConfig.PasswordToken != null)
                Token = DynamicConfig.PasswordToken;

            SerializableProperties = Helpers.SerializableProperties.Get(Label);
        }

        public Payment(string id)
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
            GetRequestFill<Payment>();
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
            PostRequestFill<Payment>();
            Get();
        }

        public void Update()
        {
            //handle properties
            SerializableProperties = Helpers.SerializableProperties.Get(Label, "Update");
            RequiredProperties = Helpers.RequiredProperties.Get(Label, "Update");
            Helpers.RequiredProperties.ValidateRequired(this, Label);
            //do not update null values
            Helpers.SerializableProperties.ExcludeNull<Payment>(this, ref SerializableProperties);

            //setup request
            RequestUrl = string.Format("{0}{1}/sobjects/{2}/{3}", Token.InstanceUrl, Token.Version.Url, SFObjectName, ID);
            HeaderValues.Add("Authorization", string.Format("{0} {1}", Token.TokenType, Token.Token));
            JsonRequest = this;

            //make request
            PatchRequestFill<Payment>();
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
            DeleteRequest<Payment>();
        }
    }
}
