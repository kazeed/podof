using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Podof.Model
{
    public class PdfDictionary : PdfValue, IDictionary<PdfString, PdfValue>
    {
        private readonly Dictionary<PdfString, PdfValue> keyValuePairs = new Dictionary<PdfString, PdfValue>();

        public ICollection<PdfString> Keys => (keyValuePairs as IDictionary<PdfString, PdfValue>).Keys;

        public ICollection<PdfValue> Values => ((IDictionary<PdfString, PdfValue>)this.keyValuePairs).Values;

        public int Count => ((ICollection<KeyValuePair<PdfString, PdfValue>>)this.keyValuePairs).Count;

        public bool IsReadOnly => ((ICollection<KeyValuePair<PdfString, PdfValue>>)this.keyValuePairs).IsReadOnly;

        public PdfValue this[PdfString key] { get => ((IDictionary<PdfString, PdfValue>)this.keyValuePairs)[key]; set => ((IDictionary<PdfString, PdfValue>)this.keyValuePairs)[key] = value; }

        public void Add(PdfString key, PdfValue value) => ((IDictionary<PdfString, PdfValue>)this.keyValuePairs).Add(key, value);

        public void Add(KeyValuePair<PdfString, PdfValue> item) => ((ICollection<KeyValuePair<PdfString, PdfValue>>)this.keyValuePairs).Add(item);

        public void Clear() => ((ICollection<KeyValuePair<PdfString, PdfValue>>)this.keyValuePairs).Clear();

        public bool Contains(KeyValuePair<PdfString, PdfValue> item) => ((ICollection<KeyValuePair<PdfString, PdfValue>>)this.keyValuePairs).Contains(item);

        public bool ContainsKey(PdfString key) => ((IDictionary<PdfString, PdfValue>)this.keyValuePairs).ContainsKey(key);

        public void CopyTo(KeyValuePair<PdfString, PdfValue>[] array, int arrayIndex) => ((ICollection<KeyValuePair<PdfString, PdfValue>>)this.keyValuePairs).CopyTo(array, arrayIndex);

        public IEnumerator<KeyValuePair<PdfString, PdfValue>> GetEnumerator() => ((IEnumerable<KeyValuePair<PdfString, PdfValue>>)this.keyValuePairs).GetEnumerator();

        public bool Remove(PdfString key) => ((IDictionary<PdfString, PdfValue>)this.keyValuePairs).Remove(key);

        public bool Remove(KeyValuePair<PdfString, PdfValue> item) => ((ICollection<KeyValuePair<PdfString, PdfValue>>)this.keyValuePairs).Remove(item);

        public bool TryGetValue(PdfString key, [MaybeNullWhen(false)] out PdfValue value) => ((IDictionary<PdfString, PdfValue>)this.keyValuePairs).TryGetValue(key, out value);

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)this.keyValuePairs).GetEnumerator();

        public override string ToString()
        {
            var contents = string.Join(" ", this.keyValuePairs.Select(kv => $"{kv.Key} {kv.Value}")).Trim();
            return $"<<{contents}>>";
}
    }
}
