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
    public class LeadController : Controller
    {
        [HttpGet]
        [Route("Lead/{id}")]
        public string Get(string id)
        {
            try
            {
                Lead lead = new Lead();
                lead.ID = id;
                lead.Get();
                return JsonHelper.JsonString(new { Lead = lead, Result = new { Status = "Success", Message = "Successfully retrieved Lead details." } });
            }
            catch (Exception ex)
            {
                return JsonHelper.JsonString(new { Result = new { Status = "Failure", Message = ex.Message } });
            }
        }
        
        [HttpGet]
        [Route("Lead/GetByEmail")]
        public ActionResult GetByEmail([Bind] string email)
        {
            try
            {
                SoqlQuery leadQuery = new SoqlQuery(new WhereField("Email", email), "Lead");
                Lead lead = new Lead(leadQuery.GetId());

                if (string.IsNullOrWhiteSpace(lead.Email))
                    throw new Exception("No lead found having email '" + email + "'");

                List<string> fields = new List<string>() { "Lead", "Id", "Account__c", "FirstName", "LastName", "Email", "Phone", "Street", "City", "State", "PostalCode", "Country", "Result", "Status", "Message" };
                return JsonHelper.JsonContent(new { Lead = lead, Result = new { Status = "Success", Message = "Successfully retrieved Lead details by email." } }, fields);
            }
            catch (Exception ex)
            {
                return JsonHelper.JsonContent(new { Result = new { Status = "Failure", Message = ex.Message } });
            }
        }

        [HttpPost]
        [Route("Lead/convertToContact")]
        public string ConvertToContact([Bind] Lead lead)
        {
            try
            {
                Contact contact = new Contact();
                contact.FirstName = lead.FirstName;
                contact.LastName = lead.LastName;
                contact.Email = lead.Email;
                contact.Phone = lead.Phone;
                contact.Street = lead.Street;
                contact.City = lead.City;
                contact.State = lead.State;
                contact.PostalCode = lead.PostalCode;
                contact.Country = lead.Country;
                contact.CurrencyCode = lead.CurrencyCode;

                //TO-DO: set account ID. We can use the company, but we usually match on address but not the contact address...
                contact.Create();
                lead.Delete();

                return JsonHelper.JsonString(new { Contact = contact, Result = new { Status = "Success", Message = "Successfully converted Lead to Contact." } });
            }
            catch (Exception ex)
            {
                return JsonHelper.JsonString(new { Result = new { Status = "Failure", Message = ex.Message } });
            }
        }

        [HttpPost]
        [Route("Lead/{id}")]
        public string Post([Bind] Lead lead)
        {
            try
            {
                lead.Create();
                return JsonHelper.JsonString(new { Lead = lead, Result = new { Status = "Success", Message = "Successfully created Lead." } });
            }
            catch (Exception ex)
            {
                return JsonHelper.JsonString(new { Result = new { Status = "Failure", Message = ex.Message } });
            }
        }

        [HttpPut]
        [Route("Lead/{id}")]
        public string Put(string id, [Bind] Lead lead)
        {
            try
            {
                lead.ID = id;
                lead.Update();
                return JsonHelper.JsonString(new { Lead = lead, Result = new { Status = "Success", Message = "Successfully updated Lead details." } });
            }
            catch (Exception ex)
            {
                return JsonHelper.JsonString(new { Result = new { Status = "Failure", Message = ex.Message } });
            }
        }

        [HttpDelete]
        [Route("Lead/{id}")]
        public string Delete(string id)
        {
            try
            {
                Lead lead = new Lead();
                lead.ID = id;
                lead.Delete();
                return JsonHelper.JsonString(new { Lead = lead, Result = new { Status = "Success", Message = "Successfully deleted Lead." } });
            }
            catch (Exception ex)
            {
                return JsonHelper.JsonString(new { Result = new { Status = "Failure", Message = ex.Message } });
            }
        }
    }
}