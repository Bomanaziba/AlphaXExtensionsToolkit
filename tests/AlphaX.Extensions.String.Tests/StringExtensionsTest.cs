using System;
using Xunit;
using AlphaX.Extensions.String;

namespace AlphaX.Extensions.String.Tests
{
    public class StringExtensionsTest
    {
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
    }
}