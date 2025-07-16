# @SerializerExtensions

`@SerializerExtensions` provides a set of extension methods to simplify serialization and deserialization tasks in .NET applications.

## Features

- Serialize objects to JSON, XML, or binary formats
- Deserialize data back to objects
- Customizable serialization settings
- Easy integration with existing projects

## Installation

```bash
dotnet add package AlphaX.Extensions.Serializer
```

## Usage

```csharp
using AlphaX.Extensions.Serializer;

// Serialize to JSON
string json = myObject.ToJson();

// Deserialize from JSON
MyType obj = json.FromJson<MyType>();
```

## Supported Formats

- XML

## API Reference

- `DeserializeFromXml`
- `SerializeToXml`

## Contributing

Contributions are welcome! Please submit issues or pull requests via GitHub.

## License

MIT
