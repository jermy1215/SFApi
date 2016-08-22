using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SalesforceData.Helpers
{
    class RequiredProperties
    {
        //function to get list of properties, throwing a custom error if the input is invalid
        public static List<string> Get(string obj, string action = "All")
        {
            try
            {
                return GetAllActions(obj)[action];
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("No list of required object properties could be found for object: {0}, action: {1}", obj, action));
            }
        }

        public static Dictionary<string, List<string>> GetAllActions(string obj)
        {
            try
            {
                return RequiredProps[obj];
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("No list of required object properties could be found for object: {0}", obj));
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
                    if (prop.PropertyType == typeof(String))
                    {
                        if (string.IsNullOrWhiteSpace(prop.GetValue(obj).ToString()))
                            list.Remove(jsonPropName);
                    }
                    else
                    {
                        if (prop.GetValue(obj) == null)
                            list.Remove(jsonPropName);
                    }
                }
            }
        }

        private static Dictionary<string, Dictionary<string, List<string>>> RequiredProps = new Dictionary<string, Dictionary<string, List<string>>>()
        {
            #region Account
             {"Account", new Dictionary<string, List<string>>()
                {
                    {"Get", new List<string>()
                        {
                            "ID"
                        }
                    },//end
                    {"Create", new List<string>()
                        {
                            "Name",
                            "Street",
                            "City",
                            //"State",
                            "Country",
                            "PostalCode"
                        }
                    },//end
                    {"Update", new List<string>()
                        {
                            "ID"
                        }
                    },//end
                    {"Delete", new List<string>()
                        {
                            "ID"
                        }
                    }//end
                }
            },//end object
            #endregion

            #region Contact
            {"Contact", new Dictionary<string, List<string>>()
                {
                    {"Get", new List<string>()
                        {
                            "ID"
                        }
                    },//end
                    {"Create", new List<string>()
                        {
                            "AccountId",
                            "LastName",
                            "FirstName",
                            "Street",
                            "City",
                            //"State",
                            "PostalCode",
                            "Country",
                            //"Phone",
                            "Email",
                            //"CurrencyCode"
                        }
                    },//end
                    {"Update", new List<string>()
                        {
                            "ID"
                        }
                    },//end
                    {"Delete", new List<string>()
                        {
                            "ID"
                        }
                    },//end
                }
            },//end object
            #endregion

            #region Lead
             {"Lead", new Dictionary<string, List<string>>()
                {
                    {"Get", new List<string>()
                        {
                            "ID"
                        }
                    },//end
                    {"Create", new List<string>()
                        {
                            "Company",
                            "LastName",
                            "FirstName",
                            "Street",
                            "City",
                            "State",
                            "PostalCode",
                            "Country",
                            //"Phone",
                            "Email",
                            //"CurrencyCode"
                        }
                    },//end
                    {"Update", new List<string>()
                        {
                            "ID"
                        }
                    },//end
                    {"Delete", new List<string>()
                        {
                            "ID"
                        }
                    },//end
                }
            },//end object
            #endregion
        };

        /// <summary>
        /// Validate  required fiels in a single object
        /// </summary>
        /// <param name="obj"></param>
        public static void ValidateRequired(object obj, string validationType = null)
        {
            ValidateRequired(new List<object>() { obj }, validationType);
        }

        /// <summary>
        /// Validate required fields in objects
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="validationType"></param>
        public static void ValidateRequired(List<object> objects, string validationType = null)
        {
            string message = "";
            foreach (object obj in objects)
            {
                List<string> required = new List<string>();
                Type type = obj.GetType();
                PropertyInfo[] props = type.GetProperties();
                PropertyInfo requiredProp = type.GetProperty("RequiredProperties");
                if (requiredProp == null)
                {
                    FieldInfo requiredField = type.GetField("RequiredProperties");
                    required = (List<string>)requiredField.GetValue(obj);
                }
                else
                    required = (List<string>)requiredProp.GetValue(obj, null);

                foreach (string field in required)
                {
                    object fieldToCheck = null;
                    if (field.Contains("."))
                        fieldToCheck = GetSubObjectField(obj, field.Substring(0, field.IndexOf(".")), field.Substring(field.IndexOf(".") + 1));
                    else
                        fieldToCheck = type.GetProperty(field).GetValue(obj, null);

                    if (fieldToCheck == null)
                        message += field + ", ";
                    else if (string.IsNullOrWhiteSpace(fieldToCheck.ToString()))
                        message += field + ", ";
                }
            }

            if (!string.IsNullOrWhiteSpace(message))
                throw new Exception(string.Format("The following required fields are missing{0}: {1}"
                        , (validationType != null ? " for " + validationType : "")
                        , (message.LastIndexOf(',') > 0 ? message.Substring(0, message.LastIndexOf(',')) : message))
                    );
        }

        /// <summary>
        /// Get an object that is a child of another object
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="subObj"></param>
        /// <param name="prop"></param>
        /// <returns></returns>
        public static object GetSubObjectField(object obj, string subObj, string prop)
        {
            object subObject = obj.GetType().GetField(subObj).GetValue(obj);

            if (subObject == null)
                throw new Exception(string.Format("Sub-Object '{0}' is empty and contains required properties.", subObj));

            PropertyInfo subProp = subObject.GetType().GetProperty(prop);
            return (subProp == null ? null : subProp.GetValue(subObject, null));
        }
    }
}
