using System;
using System.Collections.Generic;
using System.Text;

namespace Certs
{
    class Certificate
    {
        public List<string> Chain { get; }
        public string CertID { get; }
        public string IssuerName { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
        public string SubjectName { get; }
        public string PubKey { get; }
        public string Q { get; }
        public string IssuerID { get; }

    }
}
