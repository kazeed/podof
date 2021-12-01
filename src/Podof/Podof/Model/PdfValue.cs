using System.Text;

namespace Podof.Model
{
    public abstract class PdfValue
    {
        public abstract override string ToString();

        public byte[] AsBytes()
        {
            var encoding = Encoding.GetEncoding(1252);
            return encoding.GetBytes(this.ToString());
        }
    }
}
