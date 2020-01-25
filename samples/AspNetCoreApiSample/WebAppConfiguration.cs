﻿using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Withywoods.AspNetCore;
using Withywoods.Configuration;

namespace Withywoods.AspNetCoreApiSample
{
    internal class WebAppConfiguration : IWebAppConfiguration
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
        public WebAppConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #endregion

        #region Public properties

        public string InMemoryDatabaseName = "TaskList";

        public string HealthChecksEndpoint = "/health";

        #endregion

        #region IWebAppConfiguration properties

        OpenApiInfo IWebAppConfiguration.SwaggerDefinition => _configuration.TryGetSection(_ApiDefinitionSectionKey).Get<OpenApiInfo>();

        string IWebAppConfiguration.AssemblyName => typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

        #endregion
    }
}
