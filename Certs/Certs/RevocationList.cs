using System;
using System.Collections.Generic;
using System.Text;

namespace Certs
{
    //simply 2 models for storing dat
    class RevocationList
    {
        public string IssuerName { get; set; }
        public string Algorithm = "RSA";
        public DateTime LastUpdated { get; set; }
        public DateTime NextUpdate { get; set; }
        public List<RevokedCert> RevList { get; set; } = new List<RevokedCert>();
        public string SignedHash { get; set; } = string.Empty;
    }
    class RevokedCert
    {
        public Guid SerialNo { get; set; }
        public string SubjectName { get; set; }
        public DateTime RevocationDate { get; set; }
    }
}
