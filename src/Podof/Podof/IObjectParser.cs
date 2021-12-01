using Podof.Model;

namespace Podof
{
    public interface IObjectParser
    {
        PdfObject Parse(byte[] file, long offset);
    }
}
