namespace Podof.Model
{
    public class PdfObject
    {
        public PdfIdentifier Identifier { get; set; }
        
        public PdfDictionary Properties { get; set; }

        public string Type => ((PdfString)Properties[new PdfString("Type")]).Value;
    }
}
