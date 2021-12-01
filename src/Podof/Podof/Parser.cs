using System;
using System.Diagnostics;
using System.IO;

namespace Podof
{
    public class Parser
    {
        private const char Space = ' ';
        private readonly byte[] file;

        public Parser(byte[] file) => this.file = file ?? throw new ArgumentNullException(nameof(file));

        public long[] GetPointers (long offset)
        {
            Debug.WriteLine("Enter GetPointers.");

            if (file is null || file.Length == 0) throw new ArgumentNullException(nameof(file));
            if (offset < 0) throw new ArgumentOutOfRangeException(nameof(offset), $"Must be positive.");

            using var ms = new MemoryStream(file);
            ms.Seek(offset, SeekOrigin.Begin);
            ms.Seek("xref\n".Length, SeekOrigin.Current);

            var encoding = System.Text.Encoding.GetEncoding(1252);
            using var reader = new StreamReader(ms);
            var len = long.Parse(reader.ReadLine().Split(Space)[1]);
            var pointers = new long[len];
            for (var i = 0; i <= len; i++)
            {
                var vals = reader.ReadLine().Split(Space);
                if (vals[2][0] == 'n') pointers[i] = long.Parse(vals[0]);
            }

            return pointers;
        }
    }
}
