using System.Collections.Generic;

namespace Dictionary.Core
{
    public class EntryHeader
    {
        public string Lemma { get; set; }
        public EntryCategory Pos { get; set; }
        public IEnumerable<string> Meanings { get; set; }
        public IEnumerable<EntryCategory> Labels { get; set; }
    }
}