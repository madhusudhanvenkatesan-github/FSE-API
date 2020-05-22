using System;
using System.Diagnostics.CodeAnalysis;

namespace FSE_API.Database
{
    [ExcludeFromCodeCoverage]
    public class Settings
    {
        public RavenDB RavenDB { get; set; }
    }
    [ExcludeFromCodeCoverage]
    public class RavenDB
    {
        public string[] Urls { get; set; }
        public string DatabaseName { get; set; }
        public string CertPath { get; set; }
        public string CertPass { get; set; }
    }
}
