using System.Collections.Generic;

namespace Dictionary.Core.Models
{
    public class Entry : EnumerableItem
    {
        public string Paradigm { get; set; }
        public EntryHeader Header { get; set; } = new EntryHeader();
        public IEnumerable<EntryTable> Tables { get; set; } = new List<EntryTable>();
        public IEnumerable<Lemma> Related { get; set; } = new List<Lemma>();
    }
}