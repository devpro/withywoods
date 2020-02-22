# Withywoods Common Libraries

This libraries can be used by an .NET project.

## Configuration

- Extension on IConfiguration to help read configuration

```csharp
using Withywoods.Configuration;

var section = configuration.TryGetSection("mySectionName"); // will throw an exception with an explicit error message if the section doesn't exist
```

## Serialization

- Extension to serialize / deserialize in JSON format
  - It currently uses Newtonsoft.Json but will be reviewed with .NET Core 3.0 to remove this dependency and only use Microsoft .NET framework

```csharp
using Withywoods.Serialization.Json;

var jsonString = myObject.ToJson();
var newObject = myString.FromJson<MyType>();
```

## System

- String extensions

```csharp
using Withywoods.System;

var newString = "my example".FirstCharToUpper(); // newString = "My example"
```
