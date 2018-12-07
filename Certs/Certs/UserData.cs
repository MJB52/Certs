using System;
using System.Collections.Generic;
using System.Text;

namespace Certs
{
    public class UserData
    {
        public Guid CAGuid { get; set; }
        public string pubKey { get; set; }
        public string privKey { get; set; }
        public string N { get; set; }
    }
}
