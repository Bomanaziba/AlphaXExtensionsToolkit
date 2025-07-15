using System;
using Xunit;
using AlphaX.Extensions.Serializer;

namespace AlphaX.Extensions.Serializer.Tests
{
    public class SerializerExtensionsTest
    {

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


        #region SerializeToXml Tests

        [Fact]
        public void SerializeToXml_ValidObject_ReturnsValidXml()
        {
            var person = new Person { Name = "Alice", Age = 28 };
            string xml = person.SerializeToXml();
            Assert.Contains("<Person", xml);
            Assert.Contains("<Name>Alice</Name>", xml);
            Assert.Contains("<Age>28</Age>", xml);
        }

        [Fact]
        public void SerializeToXml_NullObject_ThrowsArgumentNullException()
        {
            Person person = null;
            Assert.Throws<ArgumentNullException>(() => person.SerializeToXml());
        }

        [Fact]
        public void SerializeToXml_ObjectWithDefaultValues_ReturnsXmlWithDefaults()
        {
            var person = new Person();
            string xml = person.SerializeToXml();
            Assert.Contains("<Person", xml);
            Assert.DoesNotContain("<Name>", xml); // Should be empty
            Assert.Contains("<Age>0</Age>", xml);
        }

        [Fact]
        public void SerializeToXml_ObjectWithSpecialCharacters_EncodesCorrectly()
        {
            var person = new Person { Name = "O'Reilly & Sons <Inc.>", Age = 40 };
            string xml = person.SerializeToXml();

            Assert.Contains("&lt;Inc.&gt;", xml);
            Assert.Contains("O'Reilly &amp; Sons", xml);
        }

        #endregion

    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

}