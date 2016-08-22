using _Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;

namespace _Connections
{
    public class _RestAdapter
    {
        protected string RequestUrl;
        protected Dictionary<string, string> HeaderValues = new Dictionary<string, string>();
        protected object JsonRequest;
        protected string RequestBody;
        protected string ContentType;
        protected List<string> SerializableProperties = new List<string>();
        protected List<string> SerialIgnoreProperties = new List<string>();
        public List<string> RequiredProperties = new List<string>();
        public string Status;
        public string Message;

        /// <summary>
        /// Prepares the client's Http Headers
        /// </summary>
        /// <returns></returns>
        private void PrepareHeaders(ref HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            foreach (KeyValuePair<string, string> HeaderValue in HeaderValues)
            {

                //only add an accept that isn't added
                if (HeaderValue.Key == "Accept")
                {
                    if (!client.DefaultRequestHeaders.Accept.Contains(new MediaTypeWithQualityHeaderValue(HeaderValue.Value)))
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HeaderValue.Value));
                }
                else
                    client.DefaultRequestHeaders.Add(HeaderValue.Key, HeaderValue.Value);
            }
        }

        /// <summary>
        /// Prepares an Http Request object
        /// </summary>
        /// <returns></returns>
        private StringContent PrepareContent()
        {
            //request body
            if (JsonRequest != null)
            {
                if (SerializableProperties.Count > 0)
                {
                    if (SerialIgnoreProperties.Count > 0)
                        for (int i = 0; i < SerialIgnoreProperties.Count; i++)
                            SerializableProperties.Remove(SerialIgnoreProperties.ElementAt(i));

                    RequestBody = JsonHelper.JsonString(JsonRequest, SerializableProperties);
                }
                else
                    RequestBody = JsonHelper.JsonString(JsonRequest);
            }

            if (ContentType == null)
                ContentType = "application/json";
            if (string.IsNullOrWhiteSpace(RequestBody))
                RequestBody = "";

            StringContent requestContent = new StringContent(RequestBody, Encoding.UTF8, ContentType);

            return requestContent;
        }

        /// <summary>
        /// Disposes of the user-defined request data
        /// </summary>
        private void DisposeRequest()
        {
            RequestUrl = null;
            HeaderValues = new Dictionary<string, string>();
            JsonRequest = null;
            RequestBody = null;
        }

        public void HandleResponse(HttpResponseMessage response, string responseMessage)
        {
            DisposeRequest();

            if (response.IsSuccessStatusCode)
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.Created:
                        Message = "Create operation completed successfully.";
                        break;
                    case HttpStatusCode.NotModified:
                        Message = "Operation completed successfully. No update necessary.";
                        break;
                    case HttpStatusCode.Found:
                        Message = "Resource found.";
                        break;
                    default:
                        Message = "Operation completed successfully.";
                        break;
                }
                Status = "Success";
            }
            else
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                    Message = "No resource found for the given ID.";

                Status = "Failure";
                Message = responseMessage;
                throw new Exception(responseMessage);
            }
        }

        /// <summary>
        /// GET
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected T GetRequest<T>()
        {
            //execute
            HttpClient client = new HttpClient();
            PrepareHeaders(ref client);
            using (HttpResponseMessage response = client.GetAsync(RequestUrl).Result)
            using (HttpContent responseContent = response.Content)
            {
                string responseString = responseContent.ReadAsStringAsync().Result;
                HandleResponse(response, responseString);
                client.Dispose();
                return JsonHelper.JSONDecode<T>(responseString);
            }
        }

        /// <summary>
        /// GET
        /// </summary>
        /// <returns></returns>
        protected string GetRequest()
        {
            //execute
            HttpClient client = new HttpClient();
            PrepareHeaders(ref client);
            using (HttpResponseMessage response = client.GetAsync(RequestUrl).Result)
            using (HttpContent responseContent = response.Content)
            {
                string responseString = responseContent.ReadAsStringAsync().Result;
                HandleResponse(response, responseString);
                client.Dispose();
                return responseString;
            }
        }

        /// <summary>
        /// POST
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected T PostRequest<T>()
        {
            //execute
            HttpClient client = new HttpClient();
            PrepareHeaders(ref client);
            using (HttpResponseMessage response = client.PostAsync(RequestUrl, PrepareContent()).Result)
            using (HttpContent responseContent = response.Content)
            {
                string responseString = responseContent.ReadAsStringAsync().Result;
                HandleResponse(response, responseString);
                client.Dispose();
                //if (responseString == "[]")
                //    throw new Exception("SF returned an empty Json object which could not be decoded: '[]'");
                return JsonHelper.JSONDecode<T>(responseString);
            }
        }

        /// <summary>
        /// PUT
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected T PutRequest<T>()
        {
            //execute
            HttpClient client = new HttpClient();
            PrepareHeaders(ref client);
            using (HttpResponseMessage response = client.PutAsync(RequestUrl, PrepareContent()).Result)
            using (HttpContent responseContent = response.Content)
            {
                string responseString = responseContent.ReadAsStringAsync().Result;
                HandleResponse(response, responseString);
                client.Dispose();
                return JsonHelper.JSONDecode<T>(responseString);
            }
        }

        /// <summary>
        /// PATCH
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected T PatchRequest<T>()
        {
            //execute
            HttpClient client = new HttpClient();
            PrepareHeaders(ref client);
            HttpMethod httpMethod = new HttpMethod("PATCH");
            HttpRequestMessage requestMessage = new HttpRequestMessage(httpMethod, RequestUrl);
            requestMessage.Content = PrepareContent();
            using (HttpResponseMessage response = client.SendAsync(requestMessage).Result)
            using (HttpContent responseContent = response.Content)
            {
                string responseString = responseContent.ReadAsStringAsync().Result;
                HandleResponse(response, responseString);
                client.Dispose();
                return JsonHelper.JSONDecode<T>(responseString);
            }
        }

        /// <summary>
        /// DELETE
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected void DeleteRequest<T>()
        {
            //execute
            HttpClient client = new HttpClient();
            PrepareHeaders(ref client);
            using (HttpResponseMessage response = client.DeleteAsync(RequestUrl).Result)
            using (HttpContent responseContent = response.Content)
            {
                string responseString = responseContent.ReadAsStringAsync().Result;
                HandleResponse(response, responseString);
                client.Dispose();
            }
        }

        /// <summary>
        /// Fill the current object with the data of a response object
        /// </summary>
        /// <param name="responseObj">The response object</param>
        private void RequestFill<T>(T responseObj)
        {
            PropertyInfo[] properties = this.GetType().GetProperties();

            foreach (PropertyInfo prop in properties)
                if (prop != null && responseObj != null)
                {
                    PropertyInfo decodedProp = typeof(T).GetProperty(prop.Name);
                    if (decodedProp != null)
                        prop.SetValue(this, decodedProp.GetValue(responseObj), null);
                }
        }

        /// <summary>
        /// Fill the current object with the data of a GET request
        /// </summary>
        /// <typeparam name="T"></typeparam>
        protected void GetRequestFill<T>()
        {
            RequestFill<T>(GetRequest<T>());
        }

        /// <summary>
        /// Fill the current object with the data of a POST request
        /// </summary>
        /// <typeparam name="T"></typeparam>
        protected void PostRequestFill<T>()
        {
            RequestFill<T>(PostRequest<T>());
        }

        /// <summary>
        /// Fill the current object with the data of a PUT request
        /// </summary>
        /// <typeparam name="T"></typeparam>
        protected void PutRequestFill<T>()
        {
            RequestFill<T>(PutRequest<T>());
        }

        /// <summary>
        /// Fill the current object with the data of a PUT request
        /// </summary>
        /// <typeparam name="T"></typeparam>
        protected void PatchRequestFill<T>()
        {
            RequestFill<T>(PatchRequest<T>());
        }

        ///// <summary>
        ///// Fill the current object with the data of a DELETE request
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        //protected void DeleteRequestFill<T>()
        //{
        //    RequestFill<T>(DeleteRequest<T>().Result);
        //}
    }
}
