using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Certs
{
    class Certificate
    {
        public Guid CertID { get; set; }
        public string IssuerName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string SubjectName { get; set; }
        public string Algorithm = "RSA";
        public RSAParameters PubKey { get; set; }
        public Guid IssuerID { get; set; }
        public byte [] SignedCert { get; set; } = null;
    }
}
