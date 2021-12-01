using System;
using System.IO;
using System.Text;

namespace Podof.Util
{
    internal static class Extensions
    {
        internal static long Search(this byte[] array, byte[] sequence, long offset = 0, long stopAt = 0)
        {
            if (array is null || array.Length == 0) throw new ArgumentNullException(nameof(array));
            if (sequence is null || sequence.Length == 0) throw new ArgumentNullException(nameof(sequence));
            if (offset < 0) throw new ArgumentOutOfRangeException(nameof(offset));

            var buffer = new byte[sequence.Length];
            var stop = stopAt > 0 ? stopAt : sequence.Length;
            using var ms = new MemoryStream(array);
            for (long i = 0; i < stop; i++)
            {
                ms.Seek(offset + i, SeekOrigin.Begin);
                _ = ms.Read(buffer, 0, buffer.Length);
                if (buffer == sequence) return offset + i;
            }

            return -1;
        }


        internal static byte[] AsBytes(this char[] vs)
        {
            var encoding = Encoding.GetEncoding(1252);
            return encoding.GetBytes(vs);
        }

        internal static byte[] AsBytes(this string vs)
        {
            var encoding = Encoding.GetEncoding(1252);
            return encoding.GetBytes(vs);
        }

        internal static string AsString(this byte[] vs)
        {
            var encoding = Encoding.GetEncoding(1252);
            return encoding.GetString(vs);
        }
    }
}
