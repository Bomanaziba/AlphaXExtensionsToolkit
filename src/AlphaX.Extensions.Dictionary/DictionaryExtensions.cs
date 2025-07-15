using System.Collections;
using System.Dynamic;
using System.Reflection;

namespace AlphaX.Extensions.Dictionary;

public static class DictionaryExtensions
{

    /// <summary>
    /// Tos get generic parameters values string.
    /// </summary>
    /// <param name="parameter">The parameter.</param>
    /// <typeparam name="T"></typeparam>
    public static List<KeyValuePair<string, string>> ToGetGenericParametersValuesString<T>(this T parameter)
    {
        var objectNameValues = new List<KeyValuePair<string, string>>();

        var propertyInfos = GetObjectProp<T>();

        foreach (PropertyInfo propertyInfo in propertyInfos)
        {
            var objName = propertyInfo.Name;

            var tr = parameter.GetType().GetProperty(propertyInfo.Name);
            object objValue = tr.GetValue(parameter, null);

            if (objValue == null) continue;

            var singleValue = new KeyValuePair<string, string>(objName, objValue.ToString());

            if (objValue == null) continue;

            objectNameValues.Add(singleValue);
        }

        return objectNameValues;
    }

    /// <summary>
    /// Tos get generic parameters values object.
    /// </summary>
    /// <param name="parameter">The parameter.</param>
    /// <typeparam name="T"></typeparam>
    public static List<KeyValuePair<string, object>> ToGetGenericParametersValuesObject<T>(this T parameter)
    {
        var objectNameValues = new List<KeyValuePair<string, object>>();

        var propertyInfos = GetObjectProp<T>();

        foreach (PropertyInfo propertyInfo in propertyInfos)
        {
            var objName = propertyInfo.Name;

            object objValue = parameter.GetType().GetProperty(propertyInfo.Name).GetValue(parameter, null);

            var singleValue = new KeyValuePair<string, object>(objName, objValue);

            if (objValue == null) continue;

            objectNameValues.Add(singleValue);
        }

        return objectNameValues;
    }

    /// <summary>
    /// Dictionaries to object formatter.
    /// </summary>
    /// <param name="dictionary">The dictionary.</param>
    /// <typeparam name="T"></typeparam>
    public static T DictionaryToObjectFormatter<T>(this IDictionary<string, object> dictionary)
    {
        IDictionary dictionaryObj = (IDictionary)dictionary;

        Dictionary<string, object> newDictionary = CastDictionary(dictionaryObj)
                                .ToDictionary(entry => (string)entry.Key, entry => entry.Value);

        return System.Text.Json.JsonSerializer.Deserialize<T>(DictionaryToObject(newDictionary));
    }


    /// <summary>
    /// Dictionaries to object.
    /// </summary>
    /// <param name="dictionary">The dictionary.</param>
    private static string DictionaryToObject(IDictionary<String, Object> dictionary)
    {
        var expandoObj = new ExpandoObject();
        var expandoObjCollection = expandoObj as ICollection<KeyValuePair<String, Object>>;

        foreach (var keyValuePair in dictionary)
        {
            expandoObjCollection.Add(keyValuePair);
        }

        dynamic eoDynamic = expandoObj;

        return System.Text.Json.JsonSerializer.Serialize(eoDynamic);
    }


    /// <summary>
    /// Casts dictionary.
    /// </summary>
    /// <param name="dictionary">The dictionary.</param>
    private static IEnumerable<DictionaryEntry> CastDictionary(IDictionary dictionary)
    {
        foreach (DictionaryEntry entry in dictionary)
        {
            yield return entry;
        }
    }

    /// <summary>
    /// Gets object prop.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static PropertyInfo[] GetObjectProp<T>()
    {
        PropertyInfo[] propertyInfos;

        propertyInfos = typeof(T).GetProperties();

        Array.Sort(propertyInfos,
        delegate (PropertyInfo propertyInfo1, PropertyInfo propertyInfo2)
            { return propertyInfo1.Name.CompareTo(propertyInfo2.Name); });

        return propertyInfos;

    }
}
