# AlphaXExtensionsToolkit - Extension Methods Documentation

This document provides an overview of all extension methods available in the following files under `src`:

- `DictionaryExtensions.cs`
- `DocumentExtensions.cs`
- `GenericExtensions.cs`
- `HttpContentExtensions.cs`
- `SerializerExtensions.cs`
- `StringExtensions.cs`

---

## DictionaryExtensions

### Installation

```shell
dotnet add package AlphaX.Extensions.Dictionary
```

### Methods

- `ToGetGenericParametersValuesString`
- `ToGetGenericParametersValuesObject`
- `DictionaryToObjectFormatter`
- `DictionaryToObjectString`
- `CastDictionary`

---

## DocumentExtensions

### Installation

Add the package to your project:

```shell
dotnet add package AlphaX.Extensions.Document
```

### Methods

- `ExtractDataFromExcel`

---

## GenericExtensions

### Installation

```bash
dotnet add package AlphaX.Extensions.Generics
```

### Methods

- `GetGenericTypeName`
- `ObjectToByteArray`
- `ByteArrayToObject`
- `GetObjectProp`

---

## HttpContentExtensions

### Installation

```bash
dotnet add package AlphaX.Extensions.HttpContent
```

### Methods

- `HttpContentToJsonString`
- `HttpContentToJsonStringAsync`
- `HttpContentToTypeAsync<T>`
- `HttpContentToType<T>`

---

## SerializerExtensions

### Installation

```bash
dotnet add package AlphaX.Extensions.Serializer
```

### Methods

- `DeserializeFromXml`
- `SerializeToXml`

---

## StringExtensions

### Installation

```bash
dotnet add package AlphaX.Extensions.String
```

### Methods

- `FromHexStringToBase64String`
- `FromHexStringToHexByteArray`
- `GenerateNamePrefix`


## Contributing

Contributions are welcome! Please submit issues or pull requests via GitHub.

## License

MIT

---

> For detailed usage and examples, refer to the inline documentation in each source file.