using System;
using System.Collections.Generic;
using System.Text;

namespace Certs
{
    class Certificate
    {
        public List<string> Chain { get; set; }
        public Guid CertID { get; set; }
        public string IssuerName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string SubjectName { get; set; }
        public string PubKey { get; set; }
        public string N { get; set; }
        public Guid IssuerID { get; set; }
        public string SignedCert { get; set; }
    }
}
