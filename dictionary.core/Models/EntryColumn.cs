using System.Collections.Generic;

namespace Dictionary.Core.Models
{
    public class EntryColumn : EnumerableItem
    {
        public IEnumerable<Form> Forms { get; set; } = new List<Form>();
    }
}