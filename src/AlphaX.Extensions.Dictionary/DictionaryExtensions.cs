using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using AlphaX.Extensions.Generics;
using Newtonsoft.Json;


namespace AlphaX.Extensions.Dictionary
{

    public static class DictionaryExtensions
    {
        /// <summary>
        /// Tos get generic parameters values string.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <typeparam name="T"></typeparam>
        public static List<KeyValuePair<string, string>> ToGetGenericParametersValuesString<T>(this T parameter)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(nameof(parameter)); // Return empty list if parameter is null
            }

            var objectNameValues = new List<KeyValuePair<string, string>>();

            var propertyInfos = parameter.GetObjectProp();

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
            if (parameter == null)
            {
                throw new ArgumentNullException(nameof(parameter)); // Return empty list if parameter is null
            }

            var objectNameValues = new List<KeyValuePair<string, object>>();

            var propertyInfos = parameter.GetObjectProp();

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
            if(dictionary == null)
            {
                throw new ArgumentNullException(nameof(dictionary)); // Return default value if dictionary is null
            }

            IDictionary dictionaryObj = (IDictionary)dictionary;

            Dictionary<string, object> newDictionary = dictionaryObj.CastDictionary()
                                    .ToDictionary(entry => (string)entry.Key, entry => entry.Value);

            return JsonConvert.DeserializeObject<T>(newDictionary.DictionaryToObjectString());
        }

        /// <summary>
        /// Dictionaries to object.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        public static string DictionaryToObjectString(this IDictionary<String, Object> dictionary)
        {
            if(dictionary == null)
            {
                throw new ArgumentNullException(nameof(dictionary)); // Return empty JSON object if dictionary is null or empty
            }

            var expandoObj = new ExpandoObject();
            var expandoObjCollection = expandoObj as ICollection<KeyValuePair<String, Object>>;

            foreach (var keyValuePair in dictionary)
            {
                expandoObjCollection.Add(keyValuePair);
            }

            dynamic eoDynamic = expandoObj;

            return JsonConvert.SerializeObject(eoDynamic);
        }

        /// <summary>
        /// Casts dictionary.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        public static IEnumerable<DictionaryEntry> CastDictionary(this IDictionary dictionary)
        {
            if(dictionary == null)
            {
                throw new ArgumentNullException(nameof(dictionary)); // Return empty collection if dictionary is null
            }

            foreach (DictionaryEntry entry in dictionary)
            {
                yield return entry;
            }
        }

    }
}
