using System;
using Xunit;
using AlphaX.Extensions.String;
using AlphaX.Extensions.String.Helpers;

namespace AlphaX.Extensions.String.Tests
{
    public class StringExtensionsTest
    {

        #region FromHexStringToBase64String Tests

        [Fact]
        public void FromHexStringToBase64String_ValidHex_ReturnsCorrectBase64()
        {
            // "48656C6C6F" is "Hello" in hex, base64 should be "SGVsbG8="
            string hex = "48656C6C6F";
            string expectedBase64 = "SGVsbG8=";
            string actualBase64 = hex.FromHexStringToBase64String();
            Assert.Equal(expectedBase64, actualBase64);
        }

        [Fact]
        public void FromHexStringToBase64String_EmptyString_ReturnsEmptyBase64()
        {
            string hex = "";
            string expectedBase64 = "";
            string actualBase64 = hex.FromHexStringToBase64String();
            Assert.Equal(expectedBase64, actualBase64);
        }

        [Fact]
        public void FromHexStringToBase64String_SingleByteHex_ReturnsCorrectBase64()
        {
            // "FF" is 255, base64 should be "/w=="
            string hex = "FF";
            string expectedBase64 = "/w==";
            string actualBase64 = hex.FromHexStringToBase64String();
            Assert.Equal(expectedBase64, actualBase64);
        }

        [Fact]
        public void FromHexStringToBase64String_InvalidHex_ThrowsFormatException()
        {
            string invalidHex = "ZZ";
            Assert.Throws<FormatException>(() => invalidHex.FromHexStringToBase64String());
        }

        [Fact]
        public void FromHexStringToBase64String_OddLengthHex_ThrowsArgumentOutOfRangeException()
        {
            string oddLengthHex = "ABC";
            Assert.Throws<ArgumentOutOfRangeException>(() => oddLengthHex.FromHexStringToBase64String());
        }

        #endregion

        #region FromHexStringToHexByteArray Tests

        [Fact]
        public void FromHexStringToHexByteArray_ValidHex_ReturnsCorrectByteArray()
        {
            // "48656C6C6F" is "Hello" in hex
            string hex = "48656C6C6F";
            byte[] expected = new byte[] { 0x48, 0x65, 0x6C, 0x6C, 0x6F };
            byte[] actual = hex.FromHexStringToHexByteArray();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FromHexStringToHexByteArray_EmptyString_ReturnsEmptyArray()
        {
            string hex = "";
            byte[] expected = new byte[0];
            byte[] actual = hex.FromHexStringToHexByteArray();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FromHexStringToHexByteArray_SingleByteHex_ReturnsSingleByteArray()
        {
            string hex = "FF";
            byte[] expected = new byte[] { 0xFF };
            byte[] actual = hex.FromHexStringToHexByteArray();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FromHexStringToHexByteArray_InvalidHex_ThrowsFormatException()
        {
            string invalidHex = "ZZ";
            Assert.Throws<FormatException>(() => invalidHex.FromHexStringToHexByteArray());
        }

        [Fact]
        public void FromHexStringToHexByteArray_OddLengthHex_ThrowsArgumentOutOfRangeException()
        {
            string oddLengthHex = "ABC";
            Assert.Throws<ArgumentOutOfRangeException>(() => oddLengthHex.FromHexStringToHexByteArray());
        }

        #endregion

        #region GenerateNamePrefix Tests

        [Theory]
        [InlineData("John", "Jn")]
        [InlineData("Alice", "Ae")]
        [InlineData("Bob", "Bb")]
        [InlineData("John Doe", "JD")]
        [InlineData("Alice Smith", "AS")]
        [InlineData("Bob Marley", "BM")]
        [InlineData("Mary Jane Watson", "MW")]
        [InlineData("A", "AA")]
        [InlineData("A B", "AB")]
        [InlineData("A B C", "AC")]
        [InlineData("  John  Doe  ", "JD")] // Leading/trailing/multiple spaces
        [InlineData("John  Doe", "JD")] // Multiple spaces between names
        [InlineData("", "")] // Empty string
        public void GenerateNamePrefix_ReturnsExpectedPrefix(string input, string expected)
        {
            string actual = input.GenerateNamePrefix();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GenerateNamePrefix_SingleSpace_ReturnsEmptyPrefix()
        {
            string input = " ";
            string expected = "";
            string actual = input.GenerateNamePrefix();
            Assert.Equal(expected, actual);
        }

        #endregion


        #region DeserializeFromXml Tests

        [Fact]
        public void DeserializeFromXml_ValidXml_ReturnsDeserializedObject()
        {
            string xml = @"<Person><Name>John Doe</Name><Age>30</Age></Person>";
            Person person = xml.DeserializeFromXml<Person>();
            Assert.NotNull(person);
            Assert.Equal("John Doe", person.Name);
            Assert.Equal(30, person.Age);
        }

        [Fact]
        public void DeserializeFromXml_EmptyXml_ThrowsInvalidOperationException()
        {
            string xml = "";
            Assert.Throws<InvalidOperationException>(() => xml.DeserializeFromXml<Person>());
        }

        [Fact]
        public void DeserializeFromXml_InvalidXml_ThrowsInvalidOperationException()
        {
            string xml = "<InvalidXml>";
            Assert.Throws<InvalidOperationException>(() => xml.DeserializeFromXml<Person>());
        }

        [Fact]
        public void DeserializeFromXml_XmlWithExtraElements_IgnoresExtraElements()
        {
            string xml = @"<Person><Name>Jane</Name><Age>25</Age><Extra>Value</Extra></Person>";
            Person person = xml.DeserializeFromXml<Person>();
            Assert.NotNull(person);
            Assert.Equal("Jane", person.Name);
            Assert.Equal(25, person.Age);
        }

        #endregion
    }
}

