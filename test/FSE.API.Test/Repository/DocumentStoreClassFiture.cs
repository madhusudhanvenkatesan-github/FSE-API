using Raven.Client.Documents;
using Raven.TestDriver;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace FSE.API.Test.Repository
{
    [ExcludeFromCodeCoverage]
    public class DocumentStoreClassFixture : RavenTestDriver
    {
        public DocumentStoreClassFixture()
        {
            prepareDocumentStore(@".\RavenDBTestDir");
        }
        protected  DocumentStoreClassFixture(string path)
        {
            prepareDocumentStore(path);
        }
        public IDocumentStore Store { get; private set; }
        protected override void PreInitialize(IDocumentStore documentStore)
        {
            documentStore.Conventions.MaxNumberOfRequestsPerSession = 50;
        }
        protected virtual void prepareDocumentStore(string path)
        {
            ConfigureServer(new TestServerOptions
            {
                DataDirectory = path
            }) ;
            Store = GetDocumentStore();
            

        }
        public override void Dispose()
        {
            base.Dispose();
            if (!Store.WasDisposed) Store.Dispose();
        }
    }
}
