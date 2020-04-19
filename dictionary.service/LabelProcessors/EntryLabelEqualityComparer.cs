using Dictionary.Core.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Dictionary.Service.FormProcessors
{
    public class EntryLabelEqualityComparer : IEqualityComparer<Entry.Label>
    {
        public bool Equals([AllowNull] Entry.Label x, [AllowNull] Entry.Label y)
        {
            return
                x.Description.Equals(y.Description) &&
                x.Name.Equals(y.Name) &&
                x.ValueAbbr.Equals(y.ValueAbbr) &&
                x.ValueFull.Equals(y.ValueFull);
        }

        public int GetHashCode([DisallowNull] Entry.Label obj)
        {
            return
                obj.Description.GetHashCode() +
                obj.Name.GetHashCode() +
                obj.ValueAbbr.GetHashCode() +
                obj.ValueFull.GetHashCode();
        }
    }
}