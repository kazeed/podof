using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Podof.Model
{
    public class PdfIdentifier : PdfValue

    {
        public static Regex Pattern => new Regex(@"(\d+) (\d+) R|obj");

        public int Position { get; }

        public int Generation { get; }

        public bool IsRef { get; }

        public PdfIdentifier(string value)
        {
            if (string.IsNullOrEmpty(value) || !Pattern.IsMatch(value))
            {
                throw new ArgumentException($"'{nameof(value)}' cannot be null or empty.", nameof(value));
            }

            var elements = value.Split(' ');
            this.Position = int.Parse(elements[0], CultureInfo.InvariantCulture);
            this.Generation = int.Parse(elements[1], CultureInfo.InvariantCulture);
            this.IsRef = elements[2] == "R";
        }

        private PdfIdentifier(int pos, int gen, bool isRef)
        {
            this.Position = pos;
            this.Generation = gen;
            this.IsRef = isRef;
        }

        public override string ToString() => $"{this.Position} {this.Generation} {(this.IsRef ? "R" : "obj")}";

        public PdfIdentifier AsRef() => new PdfIdentifier(this.Position, this.Generation, true);
    }
}
