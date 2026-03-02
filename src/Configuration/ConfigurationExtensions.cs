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
        return configuration.GetSection(sectionKey)
               ?? throw new ArgumentException($"Missing section \"{sectionKey}\" in configuration", nameof(sectionKey));
    }

    public static T TryGetSection<T>(this IConfiguration configuration, string sectionKey)
    {
        var section = configuration.TryGetSection(sectionKey);
        return section.Get<T>()
               ?? throw new InvalidOperationException($"Section \"{sectionKey}\" value cannot be read as \"{nameof(T)}\"");
    }
}
