# @GenericsExtensions

A collection of generic extension methods for .NET, designed to simplify common operations and enhance code readability.

## Features

- Type-safe generic utilities
- Extension methods for collections, enums, and more
- Easy integration into existing projects

## Installation

```bash
dotnet add package AlphaX.Extensions.Generics
```

## Usage

```csharp
using AlphaX.Extensions.Generics;

// Example: Safe casting
var result = obj.AsOrDefault<MyType>();

// Example: Collection utilities
var distinctItems = myList.DistinctBy(x => x.Property);
```

## API

- `GetGenericTypeName`
- `ObjectToByteArray`
- `ByteArrayToObject`
- `GetObjectProp`


## Contributing

Contributions are welcome! Please submit issues or pull requests via GitHub.

## License

MIT