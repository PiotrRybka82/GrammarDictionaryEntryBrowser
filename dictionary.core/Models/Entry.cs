using System.Collections.Generic;

namespace Dictionary.Core.Models
{
    public class Entry : EnumerableItem
    {
        public string Paradigm { get; set; }
        public string Lemma { get; set; }
        public Entry.Label Pos { get; set; } = new Label();
        public IEnumerable<Entry.Label> Labels { get; set; } = new List<Entry.Label>();
        public IEnumerable<string> Meanings { get; set; } = new List<string>();
        public IEnumerable<Entry.Table> Tables { get; set; } = new List<Entry.Table>();
        public IEnumerable<Entry.Related> Relateds { get; set; } = new List<Entry.Related>();

        public class Label : EnumerableItem
        {
            public string Name { get; set; }
            public string ValueFull { get; set; }
            public string ValueAbbr { get; set; }
            public string Description { get; set; }
        }

        public class Table : EnumerableItem
        {
            public IEnumerable<Entry.Label> Titles { get; set; } = new List<Entry.Label>();
            public IEnumerable<Entry.Label> ColumnHeaders { get; set; } = new List<Entry.Label>();
            public IEnumerable<Entry.Row> Rows { get; set; } = new List<Entry.Row>();
        }

        public class Row : EnumerableItem
        {
            public Entry.Label RowCategory { get; set; } = new Label();
            public IEnumerable<Entry.Column> Columns { get; set; } = new List<Entry.Column>();
        }

        public class Column : EnumerableItem
        {
            public IEnumerable<Entry.Form> Forms { get; set; } = new List<Entry.Form>();
        }

        public class Form : EnumerableItem
        {
            public string Word { get; set; }
            public IEnumerable<Entry.Label> Categories { get; set; } = new List<Entry.Label>();
        }

        public class Related : EnumerableItem
        {
            public string Word { get; set; }
            public IEnumerable<Entry.Label> Categories { get; set; } = new List<Entry.Label>();
            public string Url { get; set; }
        }
    }
}