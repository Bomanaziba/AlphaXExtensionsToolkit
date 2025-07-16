# DictionaryExtensions

A collection of extension methods for working with `IDictionary<TKey, TValue>` in .NET.

## Features

- Safe retrieval of values with defaults
- Bulk addition and removal of entries
- Value transformation and filtering utilities

## Installation

```shell
dotnet add package AlphaX.Extensions.Dictionary
```

## Usage

```csharp
using AlphaX.Extensions.Dictionary;

var dict = new Dictionary<string, int>();
dict.AddOrUpdate("key", 42);
int value = dict.GetValueOrDefault("key", -1);
```

## API

- `ToGetGenericParametersValuesString`
- `ToGetGenericParametersValuesObject`
- `DictionaryToObjectFormatter`
- `DictionaryToObjectString`
- `CastDictionary`

## Contributing

Contributions are welcome! Please submit issues or pull requests via GitHub.

## License

MIT