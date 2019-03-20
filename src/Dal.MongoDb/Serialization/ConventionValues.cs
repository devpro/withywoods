namespace Devpro.Withywoods.Dal.MongoDb.Serialization
{
    /// <summary>
    /// Serialization convention possible values.
    /// </summary>
    public class ConventionValues
    {
        /// <summary>
        /// Ignore null values.
        /// </summary>
        public const string IgnoreNullValues = "IgnoreNullValues";

        /// <summary>
        /// Use camel case for element names.
        /// </summary>
        public const string CamelCaseElementName = "CamelCaseElementName";
        
        /// <summary>
        /// Use enum names.
        /// </summary>
        public const string EnumAsString = "EnumAsString";

        /// <summary>
        /// Ignore extra elements.
        /// </summary>
        public const string IgnoreExtraElements = "IgnoreExtraElements";
    }
}
