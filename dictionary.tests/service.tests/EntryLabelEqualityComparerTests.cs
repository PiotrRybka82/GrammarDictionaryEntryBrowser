using System;
using Xunit;
using Dictionary.Service.FormProcessors;
using Dictionary.Core.Models;

namespace Dictionary.Service.Tests
{
    public class EntryLabelEqualityComparerTests
    {
        EntryLabelEqualityComparer _comparer = new EntryLabelEqualityComparer();

        Entry.Label lab1 = new Entry.Label
        {
            Id = 0,
            Description = "abc",
            Name = "nazwa",
            ValueAbbr = "x",
            ValueFull = "Xxx"
        };

        Entry.Label lab2 = new Entry.Label
        {
            Id = 0,
            Description = "abc",
            Name = "nazwa",
            ValueAbbr = "x",
            ValueFull = "Xxx"
        };

        Entry.Label lab3 = new Entry.Label
        {
            Id = 0,
            Description = "abc",
            Name = "nazwa",
            ValueAbbr = "y",
            ValueFull = "Yyy"
        };

        [Fact]
        public void equals_resIsTrue()
        {
            var res = _comparer.Equals(lab1, lab2);

            Assert.True(res);

        }

        [Fact]
        public void equals_resIsFalse()
        {
            var res = _comparer.Equals(lab1, lab3);

            Assert.False(res);

        }


    }
}
