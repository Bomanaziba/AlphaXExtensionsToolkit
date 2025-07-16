using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using AlphaX.Extensions.Dictionary;
using AlphaX.Extensions.Dictionary.Tests.Model;
using Newtonsoft.Json;
using Xunit;

namespace AlphaX.Extensions.Dictionary.Tests
{
    public class DictionaryExtensionsTest
    {
        #region ToGetGenericParametersValuesString Tests

        [Fact]
        public void ToGetGenericParametersValuesString_ReturnsKeyValuePairs_ForNonNullProperties()
        {
            var obj = new TestClass0
            {
                Id = 42,
                Name = "AlphaX",
                Value = 3.14,
                NullProp = null,
                IsActive = true
            };

            var result = obj.ToGetGenericParametersValuesString();

            Assert.Contains(result, kvp => kvp.Key == "Id" && int.Parse(kvp.Value) == 42);
            Assert.Contains(result, kvp => kvp.Key == "Name" && kvp.Value == "AlphaX");
            Assert.Contains(result, kvp => kvp.Key == "Value" && double.Parse(kvp.Value) == 3.14);
            Assert.Contains(result, kvp => kvp.Key == "IsActive" && bool.Parse(kvp.Value) == true);
            Assert.DoesNotContain(result, kvp => kvp.Key == "NullProp");
            Assert.Equal(4, result.Count);
        }

        [Fact]
        public void ToGetGenericParametersValuesString_ReturnsEmptyList_WhenAllPropertiesAreNull()
        {
            var obj = new TestClass0
            {
                NullProp = null,
                Name = null,
                Value = null
            };


            var result = obj.ToGetGenericParametersValuesString();

            Assert.Contains(result, kvp => kvp.Key == "Id" && int.Parse(kvp.Value) == 0);
            Assert.Contains(result, kvp => kvp.Key == "IsActive" && bool.Parse(kvp.Value) == false);

        }

        [Fact]
        public void ToGetGenericParametersValuesString_HandlesEmptyObject()
        {
            var obj = new { };

            var result = obj.ToGetGenericParametersValuesString();

            Assert.Empty(result);
        }

        [Fact]
        public void ToGetGenericParametersValuesString_HandlesPrimitiveTypes()
        {
            int number = 100;
            var result = number.ToGetGenericParametersValuesString();

            // Should be empty, as int has no public properties
            Assert.Empty(result);
        }

        #endregion

        #region ToGetGenericParametersValuesObject Tests

        [Fact]
        public void ToGetGenericParametersValuesObject_ReturnsKeyValuePairs_ForNonNullProperties()
        {
            var obj = new TestClass0
            {
                Id = 42,
                Name = "AlphaX",
                Value = 3.14,
                NullProp = null,
                IsActive = true
            };

            var result = obj.ToGetGenericParametersValuesObject();

            Assert.Contains(result, kvp => kvp.Key == "Id" && (int)kvp.Value == 42);
            Assert.Contains(result, kvp => kvp.Key == "Name" && (string)kvp.Value == "AlphaX");
            Assert.Contains(result, kvp => kvp.Key == "Value" && (double)kvp.Value == 3.14);
            Assert.Contains(result, kvp => kvp.Key == "IsActive" && (bool)kvp.Value == true);
            Assert.DoesNotContain(result, kvp => kvp.Key == "NullProp");
            Assert.Equal(4, result.Count);
        }

        [Fact]
        public void ToGetGenericParametersValuesObject_ReturnsEmptyList_WhenAllPropertiesAreNull()
        {
            var obj = new TestClass0
            {
                NullProp = null,
                Name = null,
                Value = null
            };

            var result = obj.ToGetGenericParametersValuesObject();

            Assert.Contains(result, kvp => kvp.Key == "Id" && (int)kvp.Value == 0);
            Assert.Contains(result, kvp => kvp.Key == "IsActive" && (bool)kvp.Value == false);
        }

        [Fact]
        public void ToGetGenericParametersValuesObject_HandlesEmptyObject()
        {
            var obj = new { };

            var result = obj.ToGetGenericParametersValuesObject();

            Assert.Empty(result);
        }

        [Fact]
        public void ToGetGenericParametersValuesObject_HandlesPrimitiveTypes()
        {
            int number = 100;
            var result = number.ToGetGenericParametersValuesObject();

            // Should be empty, as int has no public properties
            Assert.Empty(result);
        }

        #endregion

        #region DictionaryToObjectFormatter Tests


        [Fact]
        public void DictionaryToObjectFormatter_CreatesObject_WithAllProperties()
        {
            var dict = new Dictionary<string, object>
            {
                { "Id", 99 },
                { "Name", "TestName" },
                { "Value", 1.23 },
                { "NullProp", null }
            };

            var result = dict.DictionaryToObjectFormatter<TestClass0>();

            Assert.Equal(99, result.Id);
            Assert.Equal("TestName", result.Name);
            Assert.Equal(1.23, result.Value);
            Assert.Null(result.NullProp);
        }

        [Fact]
        public void DictionaryToObjectFormatter_CreatesObject_WithMissingProperties()
        {
            var dict = new Dictionary<string, object>
            {
                { "Id", 7 }
                // Name, Value, NullProp missing
            };

            var result = dict.DictionaryToObjectFormatter<TestClass0>();

            Assert.Equal(7, result.Id);
            Assert.Null(result.Name);
            Assert.Null(result.Value);
            Assert.Null(result.NullProp);
        }

        [Fact]
        public void DictionaryToObjectFormatter_ReturnsDefault_WhenDictionaryIsEmpty()
        {
            var dict = new Dictionary<string, object>();

            var result = dict.DictionaryToObjectFormatter<TestClass0>();

            Assert.Equal(0, result.Id);
            Assert.Null(result.Name);
            Assert.Null(result.Value);
            Assert.Null(result.NullProp);
        }

        [Fact]
        public void DictionaryToObjectFormatter_HandlesExtraDictionaryKeys()
        {
            var dict = new Dictionary<string, object>
            {
                { "Id", 1 },
                { "Name", "Extra" },
                { "Value", 2.5 },
                { "ExtraKey", "ShouldBeIgnored" }
            };

            var result = dict.DictionaryToObjectFormatter<TestClass0>();

            Assert.Equal(1, result.Id);
            Assert.Equal("Extra", result.Name);
            Assert.Equal(2.5, result.Value);
        }

        [Fact]
        public void DictionaryToObjectFormatter_ThrowsOnNullDictionary()
        {
            Dictionary<string, object> dict = null;
            Assert.Throws<ArgumentNullException>(() => dict.DictionaryToObjectFormatter<TestClass0>());
        }

        #endregion

        #region DictionaryToObjectString Tests

        [Fact]
        public void DictionaryToObjectString_SerializesDictionaryToJsonString()
        {
            var dict = new Dictionary<string, object>
            {
                { "Id", 10 },
                { "Name", "AlphaX" },
                { "Value", 2.5 }
            };

            var json = dict.DictionaryToObjectString();

            Assert.Contains("\"Id\":10", json);
            Assert.Contains("\"Name\":\"AlphaX\"", json);
            Assert.Contains("\"Value\":2.5", json);
        }

        [Fact]
        public void DictionaryToObjectString_HandlesEmptyDictionary()
        {
            var dict = new Dictionary<string, object>();

            var json = dict.DictionaryToObjectString();

            Assert.Equal("{}", json);
        }

        [Fact]
        public void DictionaryToObjectString_HandlesNullValues()
        {
            var dict = new Dictionary<string, object>
            {
                { "Id", 1 },
                { "NullProp", null }
            };

            var json = dict.DictionaryToObjectString();

            Assert.Contains("\"Id\":1", json);
            Assert.Contains("\"NullProp\":null", json);
        }

        [Fact]
        public void DictionaryToObjectString_HandlesNestedDictionary()
        {
            var nested = new Dictionary<string, object>
            {
                { "SubId", 5 }
            };

            var dict = new Dictionary<string, object>
            {
                { "Id", 1 },
                { "Nested", nested }
            };

            var json = dict.DictionaryToObjectString();

            Assert.Contains("\"Id\":1", json);
            Assert.Contains("\"Nested\":{\"SubId\":5}", json.Replace(" ", ""));
        }

        [Fact]
        public void DictionaryToObjectString_ThrowsOnNullDictionary()
        {
            Dictionary<string, object> dict = null;
            Assert.Throws<ArgumentNullException>(() => dict.DictionaryToObjectString());
        }

        #endregion

        #region CastDictionary Tests

        [Fact]
        public void CastDictionary_ReturnsAllEntries_ForNonEmptyDictionary()
        {
            IDictionary dict = new Dictionary<string, object>
            {
                { "A", 1 },
                { "B", "test" },
                { "C", null }
            };

            var result = dict.CastDictionary().ToList();

            Assert.Equal(3, result.Count);
            Assert.Contains(result, entry => (string)entry.Key == "A" && (int)entry.Value == 1);
            Assert.Contains(result, entry => (string)entry.Key == "B" && (string)entry.Value == "test");
            Assert.Contains(result, entry => (string)entry.Key == "C" && entry.Value == null);
        }

        [Fact]
        public void CastDictionary_ReturnsEmpty_ForEmptyDictionary()
        {
            IDictionary dict = new Dictionary<string, object>();

            var result = dict.CastDictionary().ToList();

            Assert.Empty(result);
        }

        [Fact]
        public void CastDictionary_ThrowsArgumentNullException_WhenDictionaryIsNull()
        {
            IDictionary dict = null;
            Assert.Throws<ArgumentNullException>(() =>
            {
                foreach (var _ in dict.CastDictionary()) { }
            });
        }

        [Fact]
        public void CastDictionary_WorksWithHashtable()
        {
            IDictionary dict = new Hashtable
            {
                { "X", 123 },
                { "Y", "abc" }
            };

            var result = dict.CastDictionary().ToList();

            Assert.Equal(2, result.Count);
            Assert.Contains(result, entry => (string)entry.Key == "X" && (int)entry.Value == 123);
            Assert.Contains(result, entry => (string)entry.Key == "Y" && (string)entry.Value == "abc");
        }

        #endregion
    }
}
