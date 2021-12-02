using Podof.Model;

namespace Podof
{
    public interface IDocumentParser
    {
        PdfDocument Parse(byte[] file);
    }
}
