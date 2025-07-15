using System.Xml.Serialization;

namespace AlphaX.Extensions.Serializer;

public static class SerializerExtensions
{
    /// <summary>
    /// Deserializes from xml.
    /// </summary>
    /// <param name="xml">The xml.</param>
    /// <typeparam name="T"></typeparam>
    public static T DeserializeFromXml<T>(this string xml)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        using (StringReader reader = new StringReader(xml))
        {
            return (T)serializer.Deserialize(reader);
        }
    }

    /// <summary>
    /// Serializes to xml.
    /// </summary>
    /// <param name="obj">The obj.</param>
    /// <typeparam name="T"></typeparam>
    public static string SerializeToXml<T>(this T obj)
    {
        if (obj == null) throw new ArgumentNullException(nameof(obj), "Object to serialize cannot be null.");
        
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        using (StringWriter writer = new StringWriter())
        {
            serializer.Serialize(writer, obj);
            return writer.ToString();
        }
    }
}
