using System;
using System.Diagnostics.CodeAnalysis;

namespace FSE_API.Model
{
    [ExcludeFromCodeCoverage]
    public class ProjectUserVO
    {
        public Project Projects { get; set; }
        public string UserName { get; set; }

    }
}
