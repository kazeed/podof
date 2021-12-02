using System;
using System.Diagnostics;
using System.IO;
using Podof.Model;
using Podof.Util;

namespace Podof
{
    public class DocumentParser : IDocumentParser
    {
        private const char Space = ' ';
        private byte[] file;

        private readonly IObjectParser objectParser;

        public DocumentParser(IObjectParser objectParser)
        {
            this.objectParser = objectParser ?? throw new ArgumentNullException(nameof(objectParser));
        }

        public PdfDocument Parse(byte[] file)
        {
            if (file == null || file.Length == 0) throw new ArgumentException(nameof(file));
            if (!IsPdf()) throw new NotSupportedException("The provided file is not a PDF.");

            this.file = file;

            if (!IsLinearized)
            {

                var locators = GetLocators(GetCrossRefLocation());
                var objects = new PdfObject[locators.Length];
                for (int i = 0; i < locators.Length; i++)
                {
                    objects[i] = objectParser.Parse(file, locators[i]);
                }

                return new PdfDocument
                {
                    Objects = objects
                };
            }
            else
            {
                throw new NotImplementedException();
            }

        }

        private bool IsPdf() => file.Search("%PDF-".AsBytes()) != -1;

        private bool IsLinearized => file.Search("/Linearized".AsBytes()) != -1;

        private long GetCrossRefLocation(long offset = 0)
        {
            const string marker = "startxref";
            var start = file.Search(marker.AsBytes(), offset) + marker.Length;
            using var ms = new MemoryStream(file);
            ms.Seek(start, SeekOrigin.Begin);
            var encoding = System.Text.Encoding.GetEncoding(1252);
            using var reader = new StreamReader(ms, encoding);
            if (reader.Peek() == '\n' || reader.Peek() == '\r') _ = reader.Read(); // If line ends with CRLF, discard one char.
            return long.Parse(reader.ReadLine());
        }

        private long[] GetLocators(long offset)
        {
            Debug.WriteLine("Enter GetPointers.");

            if (file is null || file.Length == 0) throw new ArgumentNullException(nameof(file));
            if (offset < 0) throw new ArgumentOutOfRangeException(nameof(offset), $"Must be positive.");

            using var ms = new MemoryStream(file);
            ms.Seek(offset, SeekOrigin.Begin);
            ms.Seek("xref".Length + 1, SeekOrigin.Current); // If line ends with LF

            var encoding = System.Text.Encoding.GetEncoding(1252);
            using var reader = new StreamReader(ms);
            if (reader.Peek() == '\n' || reader.Peek() == '\r') _ = reader.Read(); // If line ends with CRLF, discard one char.
            var len = long.Parse(reader.ReadLine().Split(Space)[1]);
            var locators = new long[len];
            for (var i = 0; i <= len; i++)
            {
                var vals = reader.ReadLine().Split(Space);
                if (vals[2][0] == 'n') locators[i] = long.Parse(vals[0]);
            }

            return locators;
        }


    }
}
