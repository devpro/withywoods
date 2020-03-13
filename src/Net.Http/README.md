# Withywoods Common Configuration Library

This library can be used by an .NET project.

- Specialized connectivity exception, while calling an API for example

```csharp
using Withywoods.Net.Http.Exceptions;

var response = await client.GetAsync(url);

// ...

if (string.IsNullOrEmpty(stringResult))
{
    throw new ConnectivityException($"Empty response received while calling {url}");
}
```
