using System;
using System.Collections.Generic;
using System.Linq;
using Podof.Model;
using Podof.Util;

namespace Podof
{
    public class DictionaryParser : IDisposable, IDictionaryParser
    {
        private const byte DicStartMarker = (byte)'<';
        private const byte DicEndMarker = (byte)'>';
        private const byte ArrayStartMarker = (byte)'[';
        private const byte ArrayEndMarker = (byte)']';
        private const byte StringMarker = (byte)'/';
        private const byte Space = (byte)' ';
        private const byte LF = (byte)'\n';
        private const byte RF = (byte)'\r';

        private List<byte> buffer = new List<byte>();

        private PdfDictionary tmpDic;
        private PdfArray tmpArray;
        private PdfString tmpKey;
        private PdfValue tmpValue;

        private bool disposedValue;

        public PdfDictionary Parse(byte[] input)
        {
            if (input is null || input.Length == 0)
            {
                throw new ArgumentNullException(nameof(input));
            }

            /*
             *   <</Type/Page/MediaBox [0 0 842 1191] (space or LF)
             *  /Rotate 0/Parent 3 0 R
             *  /Resources<</ProcSet[/PDF /ImageB /ImageC /Text]
             *  /Font 9 0 R>>
             *  /Contents 5 0 R
             *   >>
             */

            for (var i = 0; i < input.Length; i++)
            {
                var curr = input[i];
                var repeated = i != 0 && input[i - 1] == input[i]; // control to not act on duplicated byte markers.
                switch (curr)
                {
                    case DicStartMarker: // Dictionary Marker. Open dic in first one, ignore the second one.
                        if (this.tmpDic == null)
                        {
                            this.tmpDic = new PdfDictionary();
                        }
                        else
                        {
                            if (!repeated)
                            {
                                // infer the key, and leave it
                                this.tmpKey = new PdfString(this.buffer.ToArray().AsString().Trim());

                                // Read until end of dictionary....
                                var rest = input.TakeLast(input.Length - i).ToArray();
                                using var parser = new DictionaryParser();
                                this.tmpValue = parser.Parse(rest); // Parse next dictionary inside. Why does it miss /Font?
                                i += this.tmpValue.AsBytes().Length; // TODO: Will have adjust this finely.
                            }

                            break;
                        }

                        break;
                    case StringMarker:

                        if (this.tmpKey == null && this.buffer.Count == 0)
                        {
                            // This is the start of a key
                            this.Init();
                        }
                        else if (this.tmpKey == null && this.buffer.Count != 0)
                        {
                            // infer the key, and leave it
                            this.tmpKey = new PdfString(this.buffer.ToArray().AsString());

                            // full reset the builder to start reading anew
                            this.Init();
                        }
                        else if (this.tmpKey != null && this.buffer.Count != 0)
                        {
                            // Add pair to dictionary, reset tmp values
                            this.tmpValue = this.GetValue(this.buffer.ToArray().AsString());
                            if (this.tmpArray == null)
                            {
                                this.tmpDic.Add(this.tmpKey, this.tmpValue);
                            }
                            else
                            {
                                this.tmpArray.Add(this.tmpValue); // Ignore the key for now
                            }

                            this.Reset();
                        }

                        break;
                    case ArrayStartMarker:
                        if (this.tmpKey == null)
                        {
                            this.tmpKey = new PdfString(this.buffer.ToArray().AsString());
                            this.Init();
                        }

                        this.tmpArray = new PdfArray();

                        break;
                    case ArrayEndMarker:
                        if (this.tmpArray != null)
                        {
                            // Get last item
                            this.tmpValue = this.GetValue(this.buffer.ToArray().AsString());
                            this.tmpArray.Add(this.tmpValue);
                            this.Init();

                            // Close add value
                            this.tmpDic.Add(this.tmpKey, this.tmpArray);
                            this.Reset();
                            this.tmpArray = null;
                        }

                        break;
                    case Space:
                        if (this.tmpKey == null)
                        {
                            this.tmpKey = new PdfString(this.buffer.ToArray().AsString());
                            this.Init();
                        }
                        else
                        {
                            if (this.tmpArray == null)
                            {
                                this.buffer.Add(curr);
                            }
                            else
                            {
                                this.tmpValue = this.GetValue(this.buffer.ToArray().AsString());
                                this.tmpArray.Add(this.tmpValue);
                                this.Init();
                            }
                        }

                        break;
                    case LF:
                    case RF:
                        // Ignore line jumps inside dictionary;
                        break;
                    case DicEndMarker:
                        return this.tmpDic;
                    default:
                        this.buffer.Add(curr);
                        break;
                }
            }

            return null;
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            this.Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        private PdfValue GetValue(string value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (int.TryParse(value, out var n))
            {
                return new PdfInt(n);
            }
            else if (double.TryParse(value, out var d))
            {
                return new PdfDouble(d);
            }
            else if (PdfIdentifier.Pattern.IsMatch(value))
            {
                return new PdfIdentifier(value);
            }
            else
            {
                return new PdfString(value);
            }
        }

        private void Init() => this.buffer.Clear();

        private void Reset()
        {
            this.tmpKey = null;
            this.tmpValue = null;
            this.buffer.Clear();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                this.Reset();
                this.buffer = null;
                this.disposedValue = true;
            }
        }
    }
}
