# Withywoods Common Configuration Library

This library can be used by an .NET project.

# IConfiguration extensions to help read configuration

```csharp
using Withywoods.Configuration;

var section = configuration.TryGetSection("mySectionName"); // will throw an exception with an explicit error message if the section doesn't exist
```
