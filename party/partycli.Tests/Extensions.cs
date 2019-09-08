using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace partycli.Tests
{
    public static class Extensions
    {
        public static void AssertWith<TExpected, TActual>(this IEnumerable<TActual> actual, IEnumerable<TExpected> expected, Action<TExpected, TActual> inspector)
        {
            Assert.Collection(actual, expected.Select(e => (Action<TActual>)(a => inspector(e, a))).ToArray());
        }
    }
}
