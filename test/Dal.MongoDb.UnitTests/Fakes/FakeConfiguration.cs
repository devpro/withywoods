using System.Collections.Generic;

namespace Devpro.Withywoods.Dal.MongoDb.UnitTests.Fakes
{
    internal class FakeConfiguration : IMongoDbConfiguration
    {
        public string ConnectionString => "mongodb://localhost:12345"; // should not exist!

        public string DatabaseName => "fakeDb";

        public List<string> SerializationConventions { get; set; }
    }
}
