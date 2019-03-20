using System;
using System.Runtime.CompilerServices;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace Devpro.Withywoods.Dal.MongoDb.UnitTests.Fakes
{
    internal class FakeEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
