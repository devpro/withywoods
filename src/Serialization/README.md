# Withywoods Common Serialization Library

This library can be used by an .NET project.

- Extension to serialize / deserialize in JSON format
  - It currently uses Newtonsoft.Json but will be reviewed when a better solution will be integrated into .NET framework

```csharp
using Withywoods.Serialization.Json;

var jsonString = myObject.ToJson();
var newObject = myString.FromJson<MyType>();
```
