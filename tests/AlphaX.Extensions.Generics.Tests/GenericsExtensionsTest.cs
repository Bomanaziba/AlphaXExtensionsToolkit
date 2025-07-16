using System;
using Xunit;
using AlphaX.Extensions.Generics;
using System.Text.Json;
using Newtonsoft.Json;
using AlphaX.Extensions.Generics.Tests.Model;

namespace AlphaX.Extensions.Generics.Tests
{
    public class GenericsExtensionsTest
    {
        #region GetGenericTypeName Tests

        [Fact]
        public void GetGenericTypeName_ShouldReturnTypeName_ForNonGenericType()
        {
            // Arrange
            Type type = typeof(int);

            // Act
            string result = type.GetGenericTypeName();

            // Assert
            Assert.Equal("Int32", result);
        }

        [Fact]
        public void GetGenericTypeName_ShouldReturnGenericTypeName_ForGenericType()
        {
            // Arrange
            Type type = typeof(List<string>);

            // Act
            string result = type.GetGenericTypeName();

            // Assert
            Assert.Equal("List<String>", result);
        }

        [Fact]
        public void GetGenericTypeName_ShouldReturnGenericTypeName_ForMultipleGenericArguments()
        {
            // Arrange
            Type type = typeof(Dictionary<int, string>);

            // Act
            string result = type.GetGenericTypeName();

            // Assert
            Assert.Equal("Dictionary<Int32,String>", result);
        }

        [Fact]
        public void GetGenericTypeName_ShouldThrowArgumentNullException_WhenTypeIsNull()
        {
            // Arrange
            Type type = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => type.GetGenericTypeName());
        }

        [Fact]
        public void GetGenericTypeName_ShouldReturnCustomGenericTypeName()
        {
            // Arrange
            Type type = typeof(CustomGeneric0<int, string>);

            // Act
            string result = type.GetGenericTypeName();

            // Assert
            Assert.Equal("CustomGeneric0<Int32,String>", result);
        }

        #endregion

        #region GetGenericTypeName for Object Tests

        [Fact]
        public void GetGenericTypeName_ObjectIsNull_ThrowsArgumentNullException()
        {
            object obj = null;
            var ex = Assert.Throws<ArgumentNullException>(() => obj.GetGenericTypeName());
            Assert.Equal("obj", ex.ParamName);
        }

        [Fact]
        public void GetGenericTypeName_NonGenericType_ReturnsTypeName()
        {
            int value = 42;
            string typeName = value.GetGenericTypeName();
            Assert.Equal("Int32", typeName);
        }

        [Fact]
        public void GetGenericTypeName_GenericType_ReturnsGenericTypeName()
        {
            var list = new System.Collections.Generic.List<string>();
            string typeName = list.GetGenericTypeName();
            Assert.Equal("List<String>", typeName);
        }

        [Fact]
        public void GetGenericTypeName_NestedGenericType_ReturnsNestedGenericTypeName()
        {
            var dict = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<int>>();
            string typeName = dict.GetGenericTypeName();
            Assert.Equal("Dictionary<String,List`1>", typeName);
        }

        #endregion

        #region ObjectToByteArray Tests

        [Fact]
        public void ObjectToByteArray_WithValidObject_ReturnsNonEmptyByteArray()
        {
            // Arrange
            var obj = new TestClass0 { Id = 1, Name = "Test" };

            // Act
            byte[] result = obj.ObjectToByteArray();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);

            var json = System.Text.Encoding.UTF8.GetString(result);
            // Optionally, verify that the byte array can be result back to the original object
            var deserialized = JsonConvert.DeserializeObject<TestClass0>(json);
            Assert.Equal(obj.Id, deserialized.Id);
            Assert.Equal(obj.Name, deserialized.Name);
        }

        [Fact]
        public void ObjectToByteArray_WithNullObject_ThrowsArgumentNullException()
        {
            // Arrange
            TestClass0 obj = null;

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => obj.ObjectToByteArray());
            Assert.Equal("obj", ex.ParamName);
        }

        #endregion

        #region ByteArrayToObject Tests

        [Fact]
        public void ByteArrayToObject_WithValidByteArray_ReturnsDeserializedObject()
        {
            var original = new TestClass1 { Id = 1, Name = "AlphaX" };
            byte[] bytes = original.ObjectToByteArray();

            var result = bytes.ByteArrayToObject<TestClass1>();

            Assert.NotNull(result);
            Assert.Equal(original, result);
        }

        [Fact]
        public void ByteArrayToObject_WithNull_ThrowsArgumentNullException()
        {
            byte[] bytes = null;
            Assert.Throws<ArgumentNullException>(() => bytes.ByteArrayToObject<TestClass1>());
        }

        [Fact]
        public void ByteArrayToObject_WithEmptyArray_ThrowsArgumentNullException()
        {
            byte[] bytes = Array.Empty<byte>();
            Assert.Throws<ArgumentNullException>(() => bytes.ByteArrayToObject<TestClass1>());
        }

        [Fact]
        public void ByteArrayToObject_WithInvalidJson_ThrowsJsonException()
        {
            byte[] bytes = new byte[] { 0x01, 0x02, 0x03 };
            Assert.Throws<JsonReaderException>(() => bytes.ByteArrayToObject<TestClass1>());
        }

        #endregion

        #region GetObjectProp Tests

        [Fact]
        public void GetObjectProp_ReturnsSortedProperties_ByName()
        {
            var obj = new TestClass2();
            var props = obj.GetObjectProp();

            Assert.Equal(3, props.Length);
            Assert.Equal("A", props[0].Name);
            Assert.Equal("B", props[1].Name);
            Assert.Equal("C", props[2].Name);
        }

        [Fact]
        public void GetObjectProp_WorksForStruct()
        {
            var s = new TestStruct0();
            var props = s.GetObjectProp();

            Assert.Equal(2, props.Length);
            Assert.Equal("Y", props[0].Name);
            Assert.Equal("Z", props[1].Name);
        }

        [Fact]
        public void GetObjectProp_EmptyClass_ReturnsEmptyArray()
        {
            var obj = new EmptyClass0();
            var props = obj.GetObjectProp();

            Assert.Empty(props);
        }

        #endregion
    }
}
