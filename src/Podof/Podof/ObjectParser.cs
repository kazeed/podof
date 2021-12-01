using System;
using System.IO;
using System.Text;
using Podof.Model;
using Podof.Util;

namespace Podof
{
    internal class ObjectParser : IObjectParser
    {
        private readonly IDictionaryParser _parser;

        public ObjectParser(IDictionaryParser parser)
        {
            _parser = parser;
        }

        public PdfObject Parse(byte[] file, long offset)
        {
            if (file == null || file.Length == 0) throw new ArgumentException(nameof(file));

            using var ms = new MemoryStream();
            var encoding = Encoding.GetEncoding(1252);
            using var reader = new StreamReader(ms, encoding);
            var idRaw = reader.ReadLine();
            var identifier = new PdfIdentifier(idRaw);
            var end = file.Search("endobj".AsBytes());
            var hasStream = file.Search("stream".AsBytes(), offset, end);
            var propsBuffer = new char[end - offset];
            var len = end - (offset + idRaw.Length);
            reader.Read(propsBuffer, 0, (int)len);
            var properties = _parser.Parse(new string(propsBuffer).AsBytes());

            return new PdfObject
            {
                Identifier = identifier,
                Properties = properties,
            };

        }
    }
}
