using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Certs
{
    //model for requesting a cert
    class CertRequest
    {
        public string Name { get; set; }
        public RSAParameters publicKey { get; set; }
    }
}
