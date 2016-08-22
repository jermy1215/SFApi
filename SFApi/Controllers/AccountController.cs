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
    public class AccountController : Controller
    {
        [HttpGet]
        [Route("Account/{id}")]
        public string Get(string id)
        {
            try
            {
                Account account = new Account();
                account.ID = id;
                account.Get();
                return JsonHelper.JsonString(new { Account = account, Result = new { Status = "Success", Message = "Successfully retrieved Account details." } });
            }
            catch (Exception ex)
            {
                return JsonHelper.JsonString(new { Result = new { Status = "Failure", Message = ex.Message } });
            }
        }

        [HttpGet]
        [Route("Account/GetByCompany")]
        public string GetByCompany([Bind] Account account)
        {
            try
            {
                account.RequiredProperties = new List<string>() { "Name", "BillingStreet", "BillingCity", "BillingState", "BillingPostalCode", "BillingCountry" };

                List<string> fields = new List<string>() { "Name", "BillingStreet", "BillingCity", "BillingState", "BillingPostalCode", "BillingCountry" };

                List<WhereField> whereFields = new List<WhereField>()
                {
                        new WhereField("BillingStreet", account.Street),
                        new WhereField("BillingCity", account.City),
                        new WhereField("BillingState", account.State),
                        new WhereField("BillingPostalCode", account.PostalCode),
                        new WhereField("BillingCountry", account.Country)
                };

                SoqlQuery accountQuery = new SoqlQuery(whereFields, "Account");
                account = new Account(accountQuery.GetId());

                return JsonHelper.JsonString(new { Account = account, Result = new { Status = "Success", Message = "Successfully retrieved Account details by company." } });
            }
            catch (Exception ex)
            {
                return JsonHelper.JsonString(new { Result = new { Status = "Failure", Message = ex.Message } });
            }
        }

        [HttpPost]
        [Route("Account")]
        public string Post([Bind] Account account)
        {
            try
            {
                account.Create();
                return JsonHelper.JsonString(new { Account = account, Result = new { Status = "Success", Message = "Successfully created Account." } });
            }
            catch (Exception ex)
            {
                return JsonHelper.JsonString(new { Result = new { Status = "Failure", Message = ex.Message } });
            }
        }

        [HttpPut]
        [Route("Account/{id}")]
        public string Put(string id, [Bind] Account account)
        {
            try
            {
                account.ID = id;
                account.Update();
                return JsonHelper.JsonString(new { Account = account, Result = new { Status = "Success", Message = "Successfully updated Account details." } });
            }
            catch (Exception ex)
            {
                return JsonHelper.JsonString(new { Result = new { Status = "Failure", Message = ex.Message } });
            }
        }

        [HttpDelete]
        [Route("Account/{id}")]
        public string Delete(string id)
        {
            try
            {
                Account account = new Account();
                account.ID = id;
                account.Delete();
                return JsonHelper.JsonString(new { Account = account, Result = new { Status = "Success", Message = "Successfully deleted Account." } });
            }
            catch (Exception ex)
            {
                return JsonHelper.JsonString(new { Result = new { Status = "Failure", Message = ex.Message } });
            }
        }
    }
}