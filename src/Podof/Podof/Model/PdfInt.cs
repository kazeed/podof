using System;

namespace Podof.Model
{
    public class PdfInt : PdfValue
    {
        public PdfInt(int value) => this.Value = value;

        public int Value { get; }

        public override string ToString() => $"{this.Value}";
    }
}
