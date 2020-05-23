using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
namespace FSE.API.Test.Repository
{
    [CollectionDefinition("RepoUnitTest")]
    public class RepoTestCollection:ICollectionFixture<DocumentStoreClassFixture>
    {
    }
}
