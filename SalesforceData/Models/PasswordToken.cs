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
    public class PasswordToken : _RestAdapter, OauthToken
    {
        [JsonProperty(PropertyName = "access_token")]
        public string Token { get; set; }
        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }
        [JsonProperty(PropertyName = "instance_url")]
        public string InstanceUrl { get; set; }
        [JsonProperty(PropertyName = "Id")]
        public string InstanceID { get; set; }
        [JsonProperty(PropertyName = "issued_at")]
        public string IssuedAt { get; set; }
        [JsonProperty(PropertyName = "signature")]
        public string Signature { get; set; }
        public SFVersion Version { get; set; }

        public PasswordToken()
        {

        }

        public PasswordToken(string environment)
        {
            _Oauth2.Oauth2_PasswordModel oauth2Model = _Oauth2.GetOauth2PasswordModel(_Enums.SFApps.Redacted, environment);
            RequestUrl = oauth2Model.RequestBaseUri + "/services/oauth2/token";
            ContentType = "application/x-www-form-urlencoded";
            RequestBody = string.Format("grant_type=password&client_id={0}&client_secret={1}&redirect_uri={2}&username={3}&password={4}",
                oauth2Model.ClientID,
                oauth2Model.ClientSecret,
                WebUtility.UrlEncode(oauth2Model.RedirectUri),
                oauth2Model.Username,
                oauth2Model.Password
            );

            Request();
            SFServices services = new SFServices(this);
            Version = services.LatestVersion;
        }

        public void Request()
        {
            PostRequestFill<PasswordToken>();
        }
    }
}
