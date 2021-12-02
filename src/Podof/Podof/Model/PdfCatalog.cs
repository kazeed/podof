using System;
using System.Collections.Generic;
using System.Text;

namespace Podof.Model
{
    public class PdfCatalog : PdfObject
    {
        public PdfCatalog(PdfObject catalogObj)
        {
        }

        public PdfString Version { get; set; }

        public PdfDictionary Extensions { get; set; }

        public PdfIdentifier PagesRef { get; set; }

        public object PageLabels { get; set; } // missing number tree

        public PdfDictionary Names { get; set; }

        public PdfIdentifier DestsRef { get; set; }

        public PdfDictionary VieweverPreferences { get; set; }

        public PdfString PageLayout { get; set; }

        public PdfString PageMode { get; set; }

        public PdfIdentifier OutlinesRef { get; set; }

        public PdfIdentifier ThreadsRef { get; set; }

        public PdfArray OpenActionArray { get; set; }

        public PdfDictionary OpenActionDictionary { get; set; }

        public PdfDictionary AA { get; set; }

        public PdfDictionary URI { get; set; }

        public PdfDictionary AcroForm { get; set; }

        public PdfIdentifier Metadata { get; set; }

        public PdfDictionary StructTreeRoot { get; set; }

        public PdfDictionary MarkInfo { get; set; }

        public PdfString Language { get; set; }

        public PdfDictionary SpiderInfo { get; set; }

        public PdfArray OutputIntents { get; set; }

        public PdfDictionary OCProperties { get; set; }

        public PdfDictionary PieceInfo { get; set; }

        public PdfDictionary Perms { get; set; }

        public PdfDictionary Legal { get; set; }

        public PdfArray Requirements { get; set; }

        public PdfDictionary Collection { get; set; }

        public bool NeeedsRendering { get; set; }



    }
}
