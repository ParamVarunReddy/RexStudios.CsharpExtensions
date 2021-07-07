using System;
using System.Collections.Generic;
using System.Text;

namespace RexStudios.CsharpExtensions
{
    public static class ArrayExtension
    {
        /// <summary>
        /// Sets all values.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the array that will be modified.</typeparam>
        /// <param name="array">The one-dimensional, zero-based array</param>
        /// <param name="value">The value.</param>
        /// <returns>A reference to the changed array.</returns>
        public static T[] SetAllValues<T>(this T[] array, T value)
        where T : struct
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = value;
            }

            return array;
        }

        /// <summary>
        /// Get the array slice between the two indexes.
        /// Inclusive for start index, exclusive for end index.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <param name="array">The one-dimensional, zero-based array that will be sliced from.</param>
        /// <param name="index">The start index.</param>
        /// <param name="end">The end index.  If end is negative, it is treated like length.</param>
        /// <returns>The resulting array.</returns>
        public static T[] Slice<T>(this T[] array, int index, int end)
        {
            // Handles negative ends
            if (end < 0)
            {
                end = index - end - 1;
            }

            int len = end - index;

            // Return new array
            T[] res = new T[len];
            for (int i = 0; i < len; i++)
            {
                res[i] = array[i + index];
            }

            return res;
        }

        /// <summary>
        /// Checks if the Arrays are equal.
        /// </summary>
        /// <typeparam name="T">Array type.</typeparam>
        /// <param name="array1">The <see cref="Array"/> that contains data to compare with.</param>
        /// <param name="array2">The <see cref="Array"/> that contains data to compare to.</param>
        /// <param name="index">A 32-bit integer that represents the index in the arrays at which comparing begins.</param>
        /// <param name="length">A 32-bit integer that represents the number of elements to compare.</param>
        /// <returns>
        /// Returns <c>true</c> if all element match and <c>false</c> otherwise.
        /// </returns>
        public static bool ArrayEqual<T>(this T[] array1, T[] array2, int index, int length)
            where T : IEquatable<T>
        {
            if (array1.Length != array2.Length)
            {
                return false;
            }

            for (int i = index; i < length; i++)
            {
                if (!array1[i].Equals(array2[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
		/// Returns empty T array.
		/// </summary>
		public static T[] Empty<T>()
        {
            return Array.Empty<T>();
        }
    }
}
