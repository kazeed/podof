using System;

namespace Podof.Model
{
    public class PdfDouble : PdfValue
    {
        public PdfDouble(double value) => this.Value = value;

        public double Value { get; }

        public override string ToString() => "${this.Value}";
    }
}
