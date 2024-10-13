using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;


namespace BuildingBlocks.UtilsMethods;

public static class AtomicModifier
{
    /// <summary>
    /// Actualiza parcialmente un objeto, teniendo en cuenta las propiedades que llegan en el objeto document.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="document"></param>
    /// <returns></returns>
    public static T PatchObject<T> ( T source, JObject document ) where T : class
    {
        Type type = typeof ( T );

        IDictionary<string, object> dict = type?.GetProperties ().ToDictionary ( e => e.Name, e => e.GetValue ( source ) )!;

        string json = document.ToString ();

        var patchedObject = JsonConvert.DeserializeObject<T> ( json );

        foreach (KeyValuePair<string, object> pair in dict)
        {
            if (document.TryGetValue ( pair.Key, StringComparison.OrdinalIgnoreCase, out JToken? value ))
            {
                PropertyInfo property = type?.GetProperty ( pair.Key )!;

                property.SetValue ( source, property.GetValue ( patchedObject ) );
            }
        }

        return source;
    }
}