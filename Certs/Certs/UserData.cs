using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Certs
{
    public class UserData
    {
        public Guid CAGuid { get; set; }
        public RSAParameters pubKey { get; set; }
        public RSAParameters privKey { get; set; }
    }
}
