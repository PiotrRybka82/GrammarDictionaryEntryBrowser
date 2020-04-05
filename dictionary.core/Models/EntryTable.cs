using System.Collections.Generic;

namespace Dictionary.Core.Models
{
    public class EntryTable : EnumerableItem
    {
        public IEnumerable<EntryCategory> Titles { get; set; }
        public IEnumerable<EntryCategory> Headers { get; set; }
        public IEnumerable<EntryRow> Rows { get; set; }
    }
}