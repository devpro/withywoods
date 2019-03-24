using System;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.Swagger;
using Withywoods.AspNetCore;

namespace Withywoods.AspNetCoreApiSample
{
    internal class ApiConfiguration : IWebAppConfiguration
    {
        #region Private constants

        private const string _ApiDefinitionSectionKey = "ApiDefinition";

        #endregion

        #region Private fields & Constructor

        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public ApiConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #endregion

        #region IWebAppConfiguration properties

        Info IWebAppConfiguration.SwaggerDefinition => GetSection(_ApiDefinitionSectionKey).Get<Info>();

        string IWebAppConfiguration.AssemblyName => System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

        #endregion

        #region Public properties

        public string InMemoryDatabaseName = "TodoList";

        #endregion

        #region Private methods

        private IConfigurationSection GetSection(string sectionKey)
        {
            var section = _configuration.GetSection(sectionKey);
            if (section == null)
                throw new ArgumentException($"Missing section \"{sectionKey}\" in configuration file");

            return section;
        }

        #endregion
    }
}
