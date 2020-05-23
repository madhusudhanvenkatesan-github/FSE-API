using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace FSE.API.DomainModel
{
    [ExcludeFromCodeCoverage]
    public class ProjectUserVO
    {
        public Project Projects { get; set; }
        public string UserName { get; set; }
    }
}
