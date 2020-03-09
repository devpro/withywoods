using System;
using System.Collections.Generic;
using Withywoods.Dal.MongoDb.Serialization;

namespace Withywoods.Dal.MongoDb.IntegrationTests
{
    public class MongoDbConfiguration : IMongoDbConfiguration
    {
        public string ConnectionString => Environment.GetEnvironmentVariable("MongoDB__ConnectionString")
            ?? "mongodb://localhost:27017";

        public string DatabaseName => "withywoods_integrationtests";

        public List<string> SerializationConventions =>
            new List<string>
            {
                ConventionValues.CamelCaseElementName,
                ConventionValues.EnumAsString,
                ConventionValues.IgnoreExtraElements,
                ConventionValues.IgnoreNullValues
            };
    }
}
