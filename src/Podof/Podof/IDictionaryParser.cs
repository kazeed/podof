using Podof.Model;

namespace Podof
{
    public interface IDictionaryParser
    {
        void Dispose();
        PdfDictionary Parse(byte[] input);
    }
}