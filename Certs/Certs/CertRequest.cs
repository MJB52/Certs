using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Certs
{
    class CertRequest
    {
        public string Name { get; set; }
        public RSAParameters publicKey { get; set; }
    }
}
