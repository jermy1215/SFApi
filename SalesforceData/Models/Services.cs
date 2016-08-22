using _Connections;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SalesforceData
{
    public class SFServices : _RestAdapter
    {
        public SFVersion LatestVersion = new SFVersion();
        public List<SFVersion> Versions = new List<SFVersion>();
        public List<SFObject> Objects = new List<SFObject>();
        private OauthToken Token;

        public SFServices()
        {

        }

        public SFServices(OauthToken token, SFVersion version = null)
        {
            Token = token;
            GetVersions();
            if (version == null)
                version = LatestVersion;

            //GetObjects(version);
        }

        /// <summary>
        /// Gets all versions and sets the latest version based on the results
        /// </summary>
        /// <param name="instanceUrl"></param>
        public void GetVersions()
        {
            RequestUrl = Token.InstanceUrl + "/services/data";
            Versions = GetRequest<List<SFVersion>>().OrderByDescending(v => v.Version).ToList();
            if (Versions.Count > 0)
                LatestVersion = Versions.ElementAt(0);
        }

        /// <summary>
        /// Gets all objects for the specified version
        /// </summary>
        /// <param name="version"></param>
        public void GetObjects(SFVersion version)
        {
            RequestUrl = string.Format("{0}{1}/sobjects", Token.InstanceUrl, version.Url);
            HeaderValues.Add("Authorization", string.Format("{0} {1}", Token.TokenType, Token.Token));
            Objects = GetRequest<SFObjectOuter>().SFObjects;
        }
    }

    /// <summary>
    /// Salesforce Version object
    /// </summary>
    public class SFVersion
    {
        [JsonProperty(PropertyName = "label")]
        public string Label { get; set; }
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
        [JsonProperty(PropertyName = "version")]
        public string Version { get; set; }

        public SFVersion()
        {

        }
    }

    /// <summary>
    /// Outer object representing the outer JSON object of the sobject response
    /// </summary>
    public class SFObjectOuter
    {
        [JsonProperty(PropertyName = "sobjects")]
        public List<SFObject> SFObjects { get; set; }

        public SFObjectOuter()
        {

        }
    }

    /// <summary>
    /// Salesforce Object object
    /// </summary>
    public class SFObject
    {
        [JsonProperty(PropertyName = "activateable")]
        public bool Activateable { get; set; }
        [JsonProperty(PropertyName = "createable")]
        public bool Createable { get; set; }
        [JsonProperty(PropertyName = "custom")]
        public bool Custom { get; set; }
        [JsonProperty(PropertyName = "customSetting")]
        public bool CustomSetting { get; set; }
        [JsonProperty(PropertyName = "deletable")]
        public bool Deletable { get; set; }
        [JsonProperty(PropertyName = "deprecatedAndHidden")]
        public bool DeprecatedAndHidden { get; set; }
        [JsonProperty(PropertyName = "feedEnabled")]
        public bool FeedEnabled { get; set; }
        [JsonProperty(PropertyName = "keyPrefix")]
        public string KeyPrefix { get; set; }
        [JsonProperty(PropertyName = "label")]
        public string Label { get; set; }
        [JsonProperty(PropertyName = "labelPlural")]
        public string LabelPlural { get; set; }
        [JsonProperty(PropertyName = "layoutable")]
        public bool Layoutable { get; set; }
        [JsonProperty(PropertyName = "mergeable")]
        public bool Mergeable { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "queryable")]
        public bool Queryable { get; set; }
        [JsonProperty(PropertyName = "replicateable")]
        public bool Replicateable { get; set; }
        [JsonProperty(PropertyName = "retrieveable")]
        public bool Retrieveable { get; set; }
        [JsonProperty(PropertyName = "searchable")]
        public bool Searchable { get; set; }
        [JsonProperty(PropertyName = "triggerable")]
        public bool Triggerable { get; set; }
        [JsonProperty(PropertyName = "undeletable")]
        public bool Undeletable { get; set; }
        [JsonProperty(PropertyName = "updateable")]
        public bool Updateable { get; set; }
        [JsonProperty(PropertyName = "urls")]
        public Dictionary<string, string> Urls { get; set; }

        public SFObject()
        {

        }
    }
}
