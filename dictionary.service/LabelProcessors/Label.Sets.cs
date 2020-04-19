using System.Collections.Generic;
using Dictionary.Core.Models;

namespace Dictionary.Service.FormProcessors
{
    internal static class LabelSets
    {
        public static IEnumerable<Entry.Label> SgPl => new[] { LabelPrototypes.Number.Singular, LabelPrototypes.Number.Plural.ReIndex(1) };
        public static IEnumerable<Entry.Label> IndPres => new[] { LabelPrototypes.Mood.Indicative, LabelPrototypes.Tense.Present.ReIndex(1) };
        public static IEnumerable<Entry.Label> IndPast => new[] { LabelPrototypes.Mood.Indicative, LabelPrototypes.Tense.Past.ReIndex(1) };
        public static IEnumerable<Entry.Label> IndFut => new[] { LabelPrototypes.Mood.Indicative, LabelPrototypes.Tense.Future.ReIndex(1) };

    }
}