using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesforceData
{
    public interface SFObjectModel
    {
        void Get();
        void Create();
        void Update();
        void Delete();
    }
}
