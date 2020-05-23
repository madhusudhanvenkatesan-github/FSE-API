using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace FSE.API.Database
{
	[ExcludeFromCodeCoverage]
	public class DBSettings
	{
		public RavenDB RavenDb { get; set; }
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
