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
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="sectionKey"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">If the section is not found</exception>
    public static IConfigurationSection TryGetSection(this IConfiguration configuration, string sectionKey)
    {
        var section = configuration.GetSection(sectionKey);
        return section
               ?? throw new ArgumentException($"Missing section \"{sectionKey}\" in configuration", nameof(sectionKey));
    }
}
