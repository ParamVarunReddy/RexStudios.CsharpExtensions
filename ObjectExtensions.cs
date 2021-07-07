using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace RexStudios.CsharpExtensions
{
    public static class ObjectExtensions
    {
		/// <summary>
		/// Makes a copy from the object.
		/// Doesn't copy the reference memory, only data.
		/// https://www.extensionmethod.net/csharp/object/clone-t
		/// </summary>
		/// <typeparam name="T">Type of the return object.</typeparam>
		/// <param name="item">Object to be copied.</param>
		/// <returns>Returns the copied object.</returns>
		public static T Clone<T>(this object item)
		{
			if (item != null)
			{
				BinaryFormatter formatter = new BinaryFormatter();
				MemoryStream stream = new MemoryStream();

				formatter.Serialize(stream, item);
				stream.Seek(0, SeekOrigin.Begin);

				T result = (T)formatter.Deserialize(stream);

				stream.Close();

				return result;
			}
			else
				return default(T);
		}

		/// <summary>
		/// if the object this method is called on is not null, runs the given function and returns the value.
		/// if the object is null, returns default(TResult)
		/// https://www.extensionmethod.net/csharp/object/ifnotnull-t-tresult
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="target"></param>
		/// <param name="getValue"></param>
		/// <returns></returns>
		public static TResult IfNotNull<T, TResult>(this T target, Func<T, TResult> getValue)
		{
			if (target != null)
				return getValue(target);
			else
				return default(TResult);
		}

	}
}
