﻿using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Withywoods.AspNetCoreApiSample
{
    /// <summary>
    /// Application main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Entry point.
        /// </summary>
        /// <param name="args"></param>
        [ExcludeFromCodeCoverage]
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Create web host builder.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
        }
    }
}
