using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesforceData
{
    public interface OauthToken
    {
        string Token { get; set; }
        string InstanceUrl { get; set; }
        string TokenType { get; set; }
        SFVersion Version { get; set; }
    }
}
