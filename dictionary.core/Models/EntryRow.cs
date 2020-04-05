using System.Collections.Generic;

namespace Dictionary.Core.Models
{
    public class EntryRow : EnumerableItem
    {
        public EntryCategory Category { get; set; }
        public IEnumerable<EntryColumn> Columns { get; set; } = new List<EntryColumn>();
    }
}