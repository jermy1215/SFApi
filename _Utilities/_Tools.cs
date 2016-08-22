using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace _Utilities
{
    public class _Tools
    {
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
                    if (requiredField == null)
                        throw new Exception("Missing required properties for " + type.Name);

                    required = (List<string>)requiredField.GetValue(obj);
                }
                else
                {
                    required = (List<string>)requiredProp.GetValue(obj, null);
                }

                foreach (string field in required)
                {
                    object fieldToCheck = null;
                    PropertyInfo fieldProp = obj.GetType().GetProperty(field);
                    if (field.Contains("."))
                        fieldToCheck = GetSubObjectField(obj, field.Substring(0, field.IndexOf(".")), field.Substring(field.IndexOf(".") + 1));
                    else
                        if (fieldProp != null)
                            fieldToCheck = fieldProp.GetValue(obj, null);

                    if (fieldToCheck == null)
                    {
                        FieldInfo fieldField = obj.GetType().GetField(field);
                        if (fieldField != null)
                            fieldToCheck = fieldField.GetValue(obj);
                    }

                    if (fieldToCheck == null)
                        //if (string.IsNullOrEmpty(obj.GetType().GetProperty(field).ToString()))
                        //message += obj.GetType().Name.ToUpper() + "." + field + ", ";
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

        public static object GetSubObjectField(object obj, string subObjName, string prop)
        {
            object subObject = obj.GetType().GetField(subObjName).GetValue(obj);

            if (subObject == null)
                throw new Exception(string.Format("Sub-Object '{0}' is empty and contains required properties.", subObjName));

            object returnObj = null;
            PropertyInfo subProp = subObject.GetType().GetProperty(prop);
            if (subProp != null)
                returnObj = subProp.GetValue(subObject, null);
            else
            {
                FieldInfo subField = subObject.GetType().GetField(prop);
                if (subField != null)
                    returnObj = subField.GetValue(subObject);
            }

            return returnObj;
        }

        public static void ValidateRequired(object obj)
        {
            ValidateRequired(new List<object>() { obj });
        }

        public static void ValidateList(Dictionary<string, object> fields)
        {
            string message = "";
            foreach (KeyValuePair<string, object> entry in fields)
            {
                try
                {
                    if (entry.Value == null)
                        message += entry.Key + ", ";
                    else if (string.IsNullOrWhiteSpace(entry.Value.ToString()))
                        message += entry.Key + ", ";
                }
                catch (Exception ex)
                {
                    message += entry.Key + ", ";
                }
            }

            if (!string.IsNullOrWhiteSpace(message))
                throw new Exception(string.Format("The following required fields are missing: {0}"
                        , (message.LastIndexOf(',') > 0 ? message.Substring(0, message.LastIndexOf(',')) : message))
                    );
        }

        /// <summary>
        /// Given a base object and a merge object,
        /// populate the base with all merge object properties
        /// where the base object's properties are null
        /// </summary>
        /// <typeparam name="T">The type of the 2 objects used in the merge</typeparam>
        /// <param name="baseObject">The receiving object that will be returned containing merged data</param>
        /// <param name="mergeObject">The giving object that will impart any missing data to the base object</param>
        /// <returns></returns>
        public static T ObjectMerge<T>(T baseObject, T mergeObject)
        {
            Type objType = mergeObject.GetType();
            PropertyInfo[] props = objType.GetProperties();
            foreach (PropertyInfo prop in props)
            {
                object value = prop.GetValue(baseObject, null);
                if (ObjNullOrWhitespace(value))
                    prop.SetValue(baseObject, prop.GetValue(mergeObject, null), null);
            }
            return baseObject;
        }

        public static bool ObjNullOrWhitespace(object obj)
        {
            if (obj == null)
                return true;
            else if (obj.GetType() == typeof(string))
                if (string.IsNullOrWhiteSpace(obj.ToString()))
                    return true;

            return false;
        }

        /// <summary>
        /// Function to return a DateTime value from an object conversion.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>DateTime value of object's string value.</returns>
        public static DateTime ToDateTime(object value)
        {
            DateTime returnDateTime = new DateTime();
            DateTime.TryParse(value.ToString(), out returnDateTime);
            return returnDateTime;
        }

        /// <summary>
        /// Function to return an Integer value from an object conversion. If the object's string value is not an Integer, we return 0.0.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Integer value of object's string value. 0.0 if the string value of the object is not a valid Integer.</returns>
        public static decimal ToInt(object value)
        {
            int returnInt = 0;
            Int32.TryParse(value.ToString(), out returnInt);
            return returnInt;
        }

        /// <summary>
        /// Function to return a Decimal value from an object conversion. If the object's string value is not a Decimal, we return 0.0.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Decimal value of object's string value. 0.0 if the string value of the object is not a valid decimal.</returns>
        public static decimal ToDecimal(object value)
        {
            decimal returnDecimal = 0;
            decimal.TryParse(value.ToString(), out returnDecimal);
            return returnDecimal;
        }

        /// <summary>
        /// Function to return a Decimal value from an object conversion. If the object's string value is not a Decimal, we return 0.0.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Decimal value of object's string value. 0.0 if the string value of the object is not a valid decimal.</returns>
        public static decimal? ToNullableDecimal(object value)
        {
            if (value == null)
                return null;

            return ToDecimal(value);
        }
    }
}