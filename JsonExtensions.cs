using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RexStudios.CsharpExtensions
{
    public static class JsonExtensions
    {
        //public static string ToJson(this object obj) {
        //    JavaScriptSerializer serializer = new JavaScriptSerializer();
        //    return serializer.Serialize(obj);
        //}

        //public static string ToJson(this object obj, int recursionDepth) {
        //    JavaScriptSerializer serializer = new JavaScriptSerializer();
        //    serializer.RecursionLimit = recursionDepth;
        //    return serializer.Serialize(obj);
        //}

        //public static T FromJson<T>(this object obj) {
        //    JavaScriptSerializer serializer = new JavaScriptSerializer();
        //    return serializer.Deserialize<T>(obj as string);
        //}

        /// <summary>
        /// Dumps the object as a json string
        /// Can be used for logging object contents.
        /// https://www.extensionmethod.net/csharp/object/dump
        /// </summary>
        /// <typeparam name="T">Type of the object.</typeparam>
        /// <param name="obj">The object to dump. Can be null</param>
        /// <param name="indent">To indent the result or not</param>
        /// <returns>the a string representing the object content</returns>
        public static string Dump<T>(this T obj, bool indent = false)
        {
            return JsonConvert.SerializeObject(obj, indent ? Formatting.Indented : Formatting.None,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
