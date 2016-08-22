using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SalesforceData.Helpers
{
    public class SerializableProperties
    {
        //function to get list of properties, throwing a custom error if the input is invalid
        public static List<string> Get(string obj, string action = "All")
        {
            try
            {
                List<string> props = SerializableProps[obj][action];
                return props;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("No list of serializable object properties could be found for object: {0}, action: {1}", obj, action));
            }
        }

        public static void ExcludeNull<T>(object obj, ref List<string> list)
        {
            if (obj.GetType() != typeof(T))
                throw new Exception("Type of object passed does not match type argument when attempting to exclude null/empty values from serialization list.");

            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo prop in properties)
            {
                JsonPropertyAttribute jsonProp = prop.GetCustomAttribute<JsonPropertyAttribute>();
                if (jsonProp == null)
                    continue;

                string jsonPropName = jsonProp.PropertyName;
                if (list.Contains(jsonPropName))
                {
                    var propValue = prop.GetValue(obj);
                    if (propValue == null)
                        list.Remove(jsonPropName);
                    else if (prop.PropertyType == typeof(String))
                        if (string.IsNullOrWhiteSpace(propValue.ToString()))
                            list.Remove(jsonPropName);
                }
            }
        }

        //values must be the SF Json property name and not the .NET class' property name i.e.: 'id' instead of 'ID'
        private static Dictionary<string, Dictionary<string, List<string>>> SerializableProps = new Dictionary<string, Dictionary<string, List<string>>>()
        {
            #region Account
             {"Account", new Dictionary<string, List<string>>()
                {
                    {"All", new List<string>()
                        {
                            "Id",
                            "Name",
                            "BillingStreet",
                            "BillingCity",
                            "BillingState",
                            "BillingCountry",
                            "BillingPostalCode",
                            "CreatedDate",
                            "CreatedById",
                            "LastModifiedDate",
                            "LastModifiedById"
                        }
                    },//end
                    {"Create", new List<string>()
                        {
                            "Name",
                            "BillingStreet",
                            "BillingCity",
                            "BillingState",
                            "BillingCountry",
                            "BillingPostalCode"
                        }
                    },//end
                    {"Update", new List<string>()
                        {
                            "Name",
                            "BillingStreet",
                            "BillingCity",
                            "BillingState",
                            "BillingCountry",
                            "BillingPostalCode"
                        }
                    }//end
                }
            },//end object
            #endregion

            #region Contact
            {"Contact", new Dictionary<string, List<string>>()
                {
                    {"All", new List<string>()
                        {
                            "Id",
                            "Account",
                            "AccountId",
                            "LastName",
                            "FirstName",
                            "MailingStreet",
                            "MailingCity",
                            "MailingState",
                            "MailingPostalCode",
                            "MailingCountry",
                            "Fax",
                            "Phone",
                            //"MobilePhone",
                            "Email",
                            "Title",
                            "Department",
                            "LeadSource",
                            "CurrencyIsoCode",
                            "CreatedDate",
                            "CreatedById",
                            "LastModifiedDate",
                            "LastModifiedById"
                        }
                    },//end
                    {"Get", new List<string>()
                        {
                            "Id",
                            "AccountId",
                            "LastName",
                            "FirstName",
                            "MailingStreet",
                            "MailingCity",
                            "MailingState",
                            "MailingPostalCode",
                            "MailingCountry",
                            "Fax",
                            "Phone",
                            //"MobilePhone",
                            "Email",
                            "Title",
                            "Department",
                            "LeadSource",
                            "CurrencyIsoCode",
                            "CreatedDate",
                            "CreatedById",
                            "LastModifiedDate",
                            "LastModifiedById"
                        }
                    },//end
                    {"Create", new List<string>()
                        {
                            "AccountId",
                            "LastName",
                            "FirstName",
                            "MailingStreet",
                            "MailingCity",
                            "MailingState",
                            "MailingPostalCode",
                            "MailingCountry",
                            "Phone",
                            //"MobilePhone",
                            "Fax",
                            "Email",
                            "Title",
                            "Department",
                            "LeadSource",
                            "CurrencyIsoCode"
                        }
                    },//end
                    {"Update", new List<string>()
                        {
                            "AccountId",
                            "LastName",
                            "FirstName",
                            "MailingStreet",
                            "MailingCity",
                            "MailingState",
                            "MailingPostalCode",
                            "MailingCountry",
                            "Fax",
                            "Phone",
                            //"MobilePhone",
                            "Email",
                            "Title",
                            "Department",
                            "LeadSource",
                            "CurrencyIsoCode"
                        }
                    }//end
                }
            },//end object
            #endregion

            #region Lead
             {"Lead", new Dictionary<string, List<string>>()
                {
                    {"All", new List<string>()
                        {
                            "Id",
                            "Account__c",
                            "Company",
                            "LastName",
                            "FirstName",
                            "Street",
                            "City",
                            "State",
                            "PostalCode",
                            "Country",
                            "Phone",
                            //"MobilePhone",
                            "Email",
                            "Title",
                            "LeadSource",
                            "CurrencyIsoCode",
                            "CreatedDate",
                            "CreatedById",
                            "LastModifiedDate",
                            "LastModifiedById"
                        }
                    },//end
                    {"Create", new List<string>()
                        {
                            "Account__c",
                            "Company",
                            "LastName",
                            "FirstName",
                            "Street",
                            "City",
                            "State",
                            "PostalCode",
                            "Country",
                            "Phone",
                            //"MobilePhone",
                            "Email",
                            "Title",
                            "LeadSource",
                            "CurrencyIsoCode"
                        }
                    },//end
                    {"Update", new List<string>()
                        {
                            "Account__c",
                            "Company",
                            "LastName",
                            "FirstName",
                            "Street",
                            "City",
                            "State",
                            "PostalCode",
                            "Country",
                            "Phone",
                            //"MobilePhone",
                            "Email",
                            "Title",
                            "LeadSource",
                            "CurrencyIsoCode"
                        }
                    }//end
                }
            },//end object
            #endregion
        };
    }
}
