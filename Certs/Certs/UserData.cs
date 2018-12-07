using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Certs
{
    //model for userData
    public class UserData
    {
        public Guid CAGuid { get; set; }
        public RSAParameters pubKey { get; set; }
        public RSAParameters privKey { get; set; }
    }
}
