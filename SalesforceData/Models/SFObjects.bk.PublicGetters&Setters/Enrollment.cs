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
    public class Enrollment : RestAdapter, SFObjectModel
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
        [JsonProperty("LastActivityDate")]
        private object _LastActivityDate;
        [JsonProperty("LastViewedDate")]
        private object _LastViewedDate;
        [JsonProperty("LastReferencedDate")]
        private object _LastReferencedDate;
        [JsonProperty("External_Enrollment_ID__c")]
        private string _ExternalEnrollmentID;
        [JsonProperty("Student_Name__c")]
        private string _StudentName;
        [JsonProperty("Email__c")]
        private string _Email;
        [JsonProperty("Student_Company__c")]
        private string _StudentCompany;
        [JsonProperty("Ship_To_Street__c")]
        private string _ShipToStreet;
        [JsonProperty("Order_Line_Item__c")]
        private OrderLineItem _OrderLineItem;
        [JsonProperty("JB_Load_Date__c")]
        private string _JBLoadDate;
        [JsonProperty("JB_Action_Type__c")]
        private string _JBActionType;
        [JsonProperty("Ship_To_City__c")]
        private string _ShipToCity;
        [JsonProperty("Ship_To_State_Province__c")]
        private string _ShipToStateProvince;
        [JsonProperty("Ship_To_Zip_Postal_Code__c")]
        private string _ShipToZipPostalCode;
        [JsonProperty("Ship_To_Country__c")]
        private string _ShipToCountry;
        [JsonProperty("Product__c")]
        private string _Product;
        [JsonProperty("Product_Code__c")]
        private string _ProductCode;
        [JsonProperty("Event__c")]
        private string _Event;
        [JsonProperty("EventStatus__c")]
        private string _EventStatus;
        [JsonProperty("EventLocation__c")]
        private string _EventLocation;
        [JsonProperty("Selling_Price__c")]
        private double _SellingPrice;
        [JsonProperty("cc_Email__c")]
        private object _CcEmail;
        [JsonProperty("External_Student_Address_ID__c")]
        private object _ExternalStudentAddressID;
        [JsonProperty("External_Company_ID__c")]
        private object _ExternalCompanyID;
        [JsonProperty("Cancel_Transfer_Reason__c")]
        private object _CancelTransferReason;
        [JsonProperty("Enrollment_Status__c")]
        private string _EnrollmentStatus;
        [JsonProperty("Enrollment_Type__c")]
        private object _EnrollmentType;
        [JsonProperty("Exam_Re_Take__c")]
        private bool _ExamReTake;
        [JsonProperty("Exam_Score__c")]
        private object _ExamScore;
        [JsonProperty("Hours_Completed__c")]
        private bool _HoursCompleted;
        [JsonProperty("SoldtoAccountID__c")]
        private string _SoldtoAccountID;
        [JsonProperty("Sold_To_Account__c")]
        private string _SoldToAccount;
        [JsonProperty("Student_Manager__c")]
        private object _StudentManager;
        [JsonProperty("Product_Id__c")]
        private string _ProductId;
        [JsonProperty("Tax_Rate__c")]
        private double _TaxRate;
        [JsonProperty("Tax__c")]
        private double _Tax;
        [JsonProperty("Order_Header__c")]
        private OrderHeader _OrderHeader;
        [JsonProperty("End_Date_Time__c")]
        private string _EndDateTime;
        [JsonProperty("Product_Family__c")]
        private string _ProductFamily;
        [JsonProperty("Delivery_Format__c")]
        private string _DeliveryFormat;
        [JsonProperty("Total_Price__c")]
        private double _TotalPrice;
        [JsonProperty("Transferred_Enrollment__c")]
        private object _TransferredEnrollment;
        [JsonProperty("Substituted_Enrollment__c")]
        private object _SubstitutedEnrollment;
        [JsonProperty("Start_Date_Time__c")]
        private string _StartDateTime;
        [JsonProperty("Start_Date__c")]
        private string _StartDate;
        [JsonProperty("End_Date__c")]
        private string _EndDate;
        public OauthToken Token;
        private string Label = "Enrollment";
        private string SFObjectName = "Enrollment__c";

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
        public object LastActivityDate { get { return _LastActivityDate; } set { _LastActivityDate = value; } }
        public object LastViewedDate { get { return _LastViewedDate; } set { _LastViewedDate = value; } }
        public object LastReferencedDate { get { return _LastReferencedDate; } set { _LastReferencedDate = value; } }
        public string ExternalEnrollmentID { get { return _ExternalEnrollmentID; } set { _ExternalEnrollmentID = value; } }
        public string StudentName { get { return _StudentName; } set { _StudentName = value; } }
        public string Email { get { return _Email; } set { _Email = value; } }
        public string StudentCompany { get { return _StudentCompany; } set { _StudentCompany = value; } }
        public string ShipToStreet { get { return _ShipToStreet; } set { _ShipToStreet = value; } }
        public OrderLineItem OrderLineItem { get { return _OrderLineItem; } set { _OrderLineItem = value; } }
        public string JBLoadDate { get { return _JBLoadDate; } set { _JBLoadDate = value; } }
        public string JBActionType { get { return _JBActionType; } set { _JBActionType = value; } }
        public string ShipToCity { get { return _ShipToCity; } set { _ShipToCity = value; } }
        public string ShipToStateProvince { get { return _ShipToStateProvince; } set { _ShipToStateProvince = value; } }
        public string ShipToZipPostalCode { get { return _ShipToZipPostalCode; } set { _ShipToZipPostalCode = value; } }
        public string ShipToCountry { get { return _ShipToCountry; } set { _ShipToCountry = value; } }
        public string Product { get { return _Product; } set { _Product = value; } }
        public string ProductCode { get { return _ProductCode; } set { _ProductCode = value; } }
        public string Event { get { return _Event; } set { _Event = value; } }
        public string EventStatus { get { return _EventStatus; } set { _EventStatus = value; } }
        public string EventLocation { get { return _EventLocation; } set { _EventLocation = value; } }
        public double SellingPrice { get { return _SellingPrice; } set { _SellingPrice = value; } }
        public object CcEmail { get { return _CcEmail; } set { _CcEmail = value; } }
        public object ExternalStudentAddressID { get { return _ExternalStudentAddressID; } set { _ExternalStudentAddressID = value; } }
        public object ExternalCompanyID { get { return _ExternalCompanyID; } set { _ExternalCompanyID = value; } }
        public object CancelTransferReason { get { return _CancelTransferReason; } set { _CancelTransferReason = value; } }
        public string EnrollmentStatus { get { return _EnrollmentStatus; } set { _EnrollmentStatus = value; } }
        public object EnrollmentType { get { return _EnrollmentType; } set { _EnrollmentType = value; } }
        public bool ExamReTake { get { return _ExamReTake; } set { _ExamReTake = value; } }
        public object ExamScore { get { return _ExamScore; } set { _ExamScore = value; } }
        public bool HoursCompleted { get { return _HoursCompleted; } set { _HoursCompleted = value; } }
        public string SoldtoAccountID { get { return _SoldtoAccountID; } set { _SoldtoAccountID = value; } }
        public string SoldToAccount { get { return _SoldToAccount; } set { _SoldToAccount = value; } }
        public object StudentManager { get { return _StudentManager; } set { _StudentManager = value; } }
        public string ProductId { get { return _ProductId; } set { _ProductId = value; } }
        public double TaxRate { get { return _TaxRate; } set { _TaxRate = value; } }
        public double Tax { get { return _Tax; } set { _Tax = value; } }
        public OrderHeader OrderHeader { get { return _OrderHeader; } set { _OrderHeader = value; } }
        public string EndDateTime { get { return _EndDateTime; } set { _EndDateTime = value; } }
        public string ProductFamily { get { return _ProductFamily; } set { _ProductFamily = value; } }
        public string DeliveryFormat { get { return _DeliveryFormat; } set { _DeliveryFormat = value; } }
        public double TotalPrice { get { return _TotalPrice; } set { _TotalPrice = value; } }
        public object TransferredEnrollment { get { return _TransferredEnrollment; } set { _TransferredEnrollment = value; } }
        public object SubstitutedEnrollment { get { return _SubstitutedEnrollment; } set { _SubstitutedEnrollment = value; } }
        public string StartDateTime { get { return _StartDateTime; } set { _StartDateTime = value; } }
        public string StartDate { get { return _StartDate; } set { _StartDate = value; } }
        public string EndDate { get { return _EndDate; } set { _EndDate = value; } }
        #endregion

        public Enrollment()
        {
            if (DynamicConfig.PasswordToken != null)
                Token = DynamicConfig.PasswordToken;

            SerializableProperties = Helpers.SerializableProperties.Get(Label);
        }

        public Enrollment(string id)
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
            GetRequestFill<Enrollment>();
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
            PostRequestFill<Enrollment>();
            Get();
        }

        public void Update()
        {
            //handle properties
            SerializableProperties = Helpers.SerializableProperties.Get(Label, "Update");
            RequiredProperties = Helpers.RequiredProperties.Get(Label, "Update");
            Helpers.RequiredProperties.ValidateRequired(this, Label);
            //do not update null values
            Helpers.SerializableProperties.ExcludeNull<Enrollment>(this, ref SerializableProperties);

            //setup request
            RequestUrl = string.Format("{0}{1}/sobjects/{2}/{3}", Token.InstanceUrl, Token.Version.Url, SFObjectName, ID);
            HeaderValues.Add("Authorization", string.Format("{0} {1}", Token.TokenType, Token.Token));
            JsonRequest = this;

            //make request
            PatchRequestFill<Enrollment>();
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
            DeleteRequest<Enrollment>();
        }
    }
}
