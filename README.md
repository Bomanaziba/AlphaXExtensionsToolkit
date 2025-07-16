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

### Methods

- **GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue>, TKey key)**
    - Returns the value for the specified key or the default value if the key is not found.

- **AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue>, TKey key, TValue value)**
    - Adds a new key-value pair or updates the value if the key already exists.

---

## DocumentExtensions

### Methods

- **ToJson(this Document document)**
    - Serializes a `Document` object to a JSON string.

- **FromJson(this string json)**
    - Deserializes a JSON string to a `Document` object.

---

## GenericExtensions

### Methods

- **IsNullOrDefault<T>(this T obj)**
    - Checks if the object is `null` or its default value.

- **SafeCast<T>(this object obj)**
    - Safely casts an object to type `T`, returning default if the cast fails.

---

## HttpContentExtensions

### Methods

- **ReadAsStringAsyncSafe(this HttpContent content)**
    - Reads the HTTP content as a string asynchronously, handling exceptions.

- **ReadAsJsonAsync<T>(this HttpContent content)**
    - Reads the HTTP content and deserializes it to type `T`.

---

## SerializerExtensions

### Methods

- **SerializeToJson<T>(this T obj)**
    - Serializes an object to a JSON string.

- **DeserializeFromJson<T>(this string json)**
    - Deserializes a JSON string to an object of type `T`.

---

## StringExtensions

### Methods

- **IsNullOrEmpty(this string str)**
    - Checks if a string is `null` or empty.

- **ToTitleCase(this string str)**
    - Converts a string to title case.

- **RemoveWhitespace(this string str)**
    - Removes all whitespace from a string.

---

> For detailed usage and examples, refer to the inline documentation in each source file.