using System;
using System.Collections.Generic;
using System.Linq;

namespace Shipwreck.Etc
{
    public sealed class TollBoothDictionary
    {
        private struct Entry
        {
            public Entry(short expresswayCode, short tollBoothCode, TollBooth entry)
            {
                ExpresswayCode = expresswayCode;
                TollBoothCode = tollBoothCode;
                Entity = entry;
            }

            public readonly short ExpresswayCode;
            public readonly short TollBoothCode;
            public readonly TollBooth Entity;
        }

        private sealed class EntryComparer : IComparer<Entry>
        {
            public static readonly EntryComparer Default = new EntryComparer();

            public int Compare(Entry x, Entry y)
            {
                var r = x.ExpresswayCode - y.ExpresswayCode;
                if (r != 0)
                {
                    return r;
                }
                return x.TollBoothCode - y.TollBoothCode;
            }
        }

        private readonly Entry[] _Array;

        public TollBoothDictionary(IEnumerable<TollBooth> entries)
        {
            _Array = entries.Select(e =>
            {
                short.TryParse(e.ExpresswayCode, out var ec);
                short.TryParse(e.TollBoothCode, out var tc);
                return new Entry(ec, tc, e);
            }).OrderBy(e => e.ExpresswayCode).ThenBy(e => e.TollBoothCode).ToArray();
        }

        public IEnumerable<TollBooth> Find(short expresswayCode, short tollBoothCode)
        {
            var i = Array.BinarySearch(_Array, new Entry(expresswayCode, tollBoothCode, null), EntryComparer.Default);
            while (i > 0
                && _Array[i - 1] is var e
                && e.ExpresswayCode == expresswayCode
                && e.TollBoothCode == tollBoothCode)
            {
                i--;
            }

            while (0 <= i && i < _Array.Length
                && _Array[i++] is var e
                && e.ExpresswayCode == expresswayCode
                && e.TollBoothCode == tollBoothCode)
            {
                yield return e.Entity;
            }
        }

        public IEnumerable<TollBooth> Find(string expresswayCode, string tollBoothCode)
        {
            short.TryParse(expresswayCode, out var ec);
            short.TryParse(tollBoothCode, out var tc);

            return Find(ec, tc)
                    .Where(e => e.ExpresswayCode == expresswayCode && e.TollBoothCode == tollBoothCode);
        }

        public bool TryFind(short expresswayCode, short tollBoothCode, out TollBooth result)
        {
            result = Find(expresswayCode, tollBoothCode).FirstOrDefault();
            return result != null;
        }

        public bool TryFind(string expresswayCode, string tollBoothCode, out TollBooth result)
        {
            result = Find(expresswayCode, tollBoothCode).FirstOrDefault();
            return result != null;
        }
    }
}