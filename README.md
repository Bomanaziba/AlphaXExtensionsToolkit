# AlphaXExtensionsToolkit
AlphaXExtensionsToolkit provides a set of extension methods for string manipulation in .NET, implemented in `AlphaX.Extensions.String.StringExtensions`. The current features include:

## Features

## StringExtensions Methods

### FromHexStringToBase64String

Converts a hex string to a Base64 string.

```csharp
using AlphaX.Extensions.String;

string hex = "48656C6C6F";
string base64 = hex.FromHexStringToBase64String(); // "SGVsbG8="
```

- Throws `ArgumentOutOfRangeException` if the hex string length is odd.
- Throws `FormatException` if the hex string contains invalid characters.

### FromHexStringToHexByteArray

Converts a hex string to a byte array.

```csharp
using AlphaX.Extensions.String;

string hex = "48656C6C6F";
byte[] bytes = hex.FromHexStringToHexByteArray(); // [72, 101, 108, 108, 111]
```

- Throws `FormatException` if the hex string contains invalid characters.

### GenerateNamePrefix

Generates a name prefix from a string.

```csharp
using AlphaX.Extensions.String;

string prefix1 = StringExtensions.GenerateNamePrefix("John"); // "Jn"
string prefix2 = StringExtensions.GenerateNamePrefix("John Doe"); // "JD"
```

- For single-word names, returns first and last character.
- For multi-word names, returns first character of first word and first character of last word.


These extensions are designed to streamline common string operations and promote cleaner, more maintainable code. Contributions are encouraged!