using System;
using Microsoft.Extensions.Configuration;

namespace Withywoods.Configuration;

/// <summary>
/// Extensions on <see cref="IConfiguration"/>
/// </summary>
public static class ConfigurationExtensions
{
    /// <summary>
    /// Get configuration section.
    /// Throws an ArgumentException with a clear message if the section is not found.
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="sectionKey"></param>
    /// <returns></returns>
    public static IConfigurationSection TryGetSection(this IConfiguration configuration, string sectionKey)
    {
        var section = configuration.GetSection(sectionKey);
        return section
               ?? throw new ArgumentException($"Missing section \"{sectionKey}\" in configuration", nameof(sectionKey));
    }
}
