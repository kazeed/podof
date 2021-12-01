using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Podof.Model
{
    public class PdfArray : PdfValue, IList<PdfValue>
    {
        private readonly List<PdfValue> values = new List<PdfValue>();

        public PdfArray(IEnumerable<PdfValue> values) => this.values = values.ToList();

        public PdfArray()
        {
        }

        public List<PdfValue> Value => this.values;

        public int Count => ((ICollection<PdfValue>)this.values).Count;

        public bool IsReadOnly => ((ICollection<PdfValue>)this.values).IsReadOnly;

        public PdfValue this[int index] { get => ((IList<PdfValue>)this.values)[index]; set => ((IList<PdfValue>)this.values)[index] = value; }

        public override string ToString() => $"[{string.Join(' ', this.Value.Select(i => i.ToString()))}]";

        public int IndexOf(PdfValue item) => ((IList<PdfValue>)this.values).IndexOf(item);

        public void Insert(int index, PdfValue item) => ((IList<PdfValue>)this.values).Insert(index, item);

        public void RemoveAt(int index) => ((IList<PdfValue>)this.values).RemoveAt(index);

        public void Add(PdfValue item) => ((ICollection<PdfValue>)this.values).Add(item);

        public void Clear() => ((ICollection<PdfValue>)this.values).Clear();

        public bool Contains(PdfValue item) => ((ICollection<PdfValue>)this.values).Contains(item);

        public void CopyTo(PdfValue[] array, int arrayIndex) => ((ICollection<PdfValue>)this.values).CopyTo(array, arrayIndex);

        public bool Remove(PdfValue item) => ((ICollection<PdfValue>)this.values).Remove(item);

        public IEnumerator<PdfValue> GetEnumerator() => ((IEnumerable<PdfValue>)this.values).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)this.values).GetEnumerator();
    }
}
