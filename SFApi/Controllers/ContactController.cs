using SFApi.ActionFilters;
using _Json;
using SalesforceData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SFApi.Controllers
{
    [SalesforceDataActionFilter]
    public class ContactController : Controller
    {
        [HttpGet]
        [Route("Contact/{id}")]
        public string Get(string id)
        {
            try
            {
                Contact contact = new Contact();
                contact.ID = id;
                contact.Get();
                return JsonHelper.JsonString(new { Contact = contact, Result = new { Status = "Success", Message = "Successfully retrieved Contact details." } });
            }
            catch (Exception ex)
            {
                return JsonHelper.JsonString(new { Result = new { Status = "Failure", Message = ex.Message } });
            }
        }

        [HttpGet]
        [Route("Contact/GetByEmail")]
        public ActionResult GetByEmail([Bind] string email)
        {
            try
            {
                SoqlQuery contactQuery = new SoqlQuery(new WhereField("Email", email), "Contact");
                Contact contact = new Contact(contactQuery.GetId());

                if (string.IsNullOrWhiteSpace(contact.Email))
                    throw new Exception("No contact found having email '" + email + "'");
                
                return JsonHelper.JsonContent(new { Contact = contact, Result = new { Status = "Success", Message = "Successfully retrieved Contact details by email." } });
            }
            catch (Exception ex)
            {
                return JsonHelper.JsonContent(new { Result = new { Status = "Failure", Message = ex.Message } });
            }
        }

        [HttpPost]
        [Route("Contact/GetAllByIds")]
        public string GetAllByIds([Bind] string contactIds)
        {
            try
            {
                //get enrollments under the Order Header Id
                SoqlQuery contactsQuery = new SoqlQuery(SalesforceData.Helpers.SerializableProperties.Get("Contact", "Get"),
                    new WhereField("Id", contactIds, false, "in"),
                    "Contact");
                //enrollmentsQuery.Query = @"SELECT Id FROM Enrollment__c WHERE Order_Line_Item__r.Order_Header__r.id = '" + orderHeaderId + "'";
                List<Contact> contacts = contactsQuery.GetObjects<Contact>();

                if (contacts.Count < 1)
                    throw new Exception("No contacts found matching the Ids provided.");

                return JsonHelper.JsonString(new { Contacts = contacts, Result = new { Status = "Success", Message = "Successfully retrieved Contacts by Ids." } });
            }
            catch (Exception ex)
            {
                return JsonHelper.JsonString(new { Result = new { Status = "Failure", Message = ex.Message } });
            }
        }

        [HttpPost]
        [Route("Contact")]
        public string Post([Bind] Contact contact)
        {
            try
            {
                contact.Create();
                return JsonHelper.JsonString(new { Contact = contact, Result = new { Status = "Success", Message = "Successfully created Contact." } });
            }
            catch (Exception ex)
            {
                return JsonHelper.JsonString(new { Result = new { Status = "Failure", Message = ex.Message } });
            }
        }

        [HttpPut]
        [Route("Contact/{id}")]
        public string Put(string id, [Bind] Contact contact)
        {
            try
            {
                contact.ID = id;
                contact.Update();
                return JsonHelper.JsonString(new { Contact = contact, Result = new { Status = "Success", Message = "Successfully updated Contact details." } });
            }
            catch (Exception ex)
            {
                return JsonHelper.JsonString(new { Result = new { Status = "Failure", Message = ex.Message } });
            }
        }

        [HttpDelete]
        [Route("Contact/{id}")]
        public string Delete(string id)
        {
            try
            {
                Contact contact = new Contact();
                contact.ID = id;
                contact.Delete();
                return JsonHelper.JsonString(new { Contact = contact, Result = new { Status = "Success", Message = "Successfully deleted Contact." } });
            }
            catch (Exception ex)
            {
                return JsonHelper.JsonString(new { Result = new { Status = "Failure", Message = ex.Message } });
            }
        }
    }
}