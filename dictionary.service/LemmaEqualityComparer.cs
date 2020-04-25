using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Dictionary.Core.Models;

namespace Dictionary.Service
{
    public class LemmaEqualityComparer : IEqualityComparer<Lemma>
    {
        public bool Equals([AllowNull] Lemma x, [AllowNull] Lemma y)
        {
            return
                x.Form.Equals(y.Form) &&
                x.Tag.Equals(y.Tag);
        }

        public int GetHashCode([DisallowNull] Lemma obj)
        {
            return obj.Form.GetHashCode() + obj.Tag.GetHashCode();
        }
    }
}