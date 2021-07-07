using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RexStudios.CsharpExtensions
{
    public static class BitExtension
    {

        /// <summary>
        /// Reverses the specified value up to the number of bits.
        /// </summary>
        /// <param name="i">The bits to reverse.</param>
        /// <param name="bits">The number of bits to reverse from the left (lsb).  Other bits are discarded.</param>
        /// <returns>The reversed bits value.</returns>
        public static ulong Reverse(this ulong i, int bits)
        {
            i = ((i >> 1) & 0x5555555555555555) | ((i & 0x5555555555555555) << 1);
            i = ((i >> 2) & 0x3333333333333333) | ((i & 0x3333333333333333) << 2);
            i = ((i >> 4) & 0x0F0F0F0F0F0F0F0F) | ((i & 0x0F0F0F0F0F0F0F0F) << 4);
            i = ((i >> 8) & 0x00FF00FF00FF00FF) | ((i & 0x00FF00FF00FF00FF) << 8);
            i = ((i >> 16) & 0x0000FFFF0000FFFF) | ((i & 0x0000FFFF0000FFFF) << 16);
            i = (i >> 32) | (i << 32);
            return i >> (64 - bits);
        }

        /// <summary>
        /// Returns a <see cref="String"/> formatted as hex with spaces that represents the current ushort[] array.
        /// </summary>
        /// <param name="data">The byte[] array.</param>
        /// <returns>A <see cref="String"/> formatted as hex with spaces that represents the current ushort[] array.</returns>
        public static string ToStringHex(this ushort[] data)
        {
            StringBuilder datastring = new StringBuilder(data.Length * 5);

            for (var i = 0; i < data.Length; i++)
            {
                datastring.AppendFormat("{0,4:x4} ", data[i]);
            }

            // remove the last space
            datastring.Remove(datastring.Length - 1, 1);

            return datastring.ToString();
        }

        public static byte[] ToBytes(this ushort[] data)
        {
            var bytes = new byte[data.Length * 2];

            for (int i = 0; i < data.Length; i++)
            {
                var shortWord = data[i];

                bytes[i * 2] = (byte)((shortWord >> 8) & 0xFF);
                bytes[i * 2 + 1] = (byte)(shortWord & 0xFF);
            }

            return bytes;
        }

        /// <summary>
        /// Returns a <see cref="String"/> formatted as hex with spaces that represents the current byte[] array.
        /// </summary>
        /// <param name="data">The byte[] array.</param>
        /// <returns>A <see cref="String"/> formatted as hex with spaces that represents the current byte[] array.</returns>
        public static string ToStringHex(this byte[] data)
        {
            StringBuilder datastring = new StringBuilder(data.Length * 3);

            for (var i = 0; i < data.Length; i++)
            {
                datastring.AppendFormat("{0,2:x2} ", data[i]);
            }

            // remove the last space
            datastring.Remove(datastring.Length - 1, 1);

            return datastring.ToString();
        }

        /// <summary>
        /// Returns a <see cref="String"/> formatted in ASCII characters that represents the current byte[] array.
        /// </summary>
        /// <param name="data">The byte[] array.</param>
        /// <returns>A <see cref="String"/> formatted in ASCII characters that represents the current byte[] array.</returns>
        public static string ToStringAscii(this byte[] data)
        {
            StringBuilder datastring = new StringBuilder(data.Length);

            for (var i = 0; i < data.Length; i++)
            {
                if (20 > data[i])
                {
                    datastring.Append((char)0xB7);
                }
                else
                {
                    datastring.Append((char)data[i]);
                }
            }

            return datastring.ToString();
        }

    }
}
