using System;
using System.IO;
using System.Linq;

namespace Shipwreck.Etc
{
    public class TsvTollBoothReader : TollBoothReader
    {
        private readonly TextReader _Reader;

        private string[] _Fields;

        public TsvTollBoothReader(Stream stream, bool leaveOpen = false)
            : this(new StreamReader(stream), leaveOpen)
        {
        }

        public TsvTollBoothReader(TextReader reader, bool leaveOpen = false)
        {
            _Reader = reader ?? throw new ArgumentNullException(nameof(reader));
            LeaveOpen = leaveOpen;
        }

        public bool LeaveOpen { get; set; }

        public int RowIndex { get; private set; } = -1;

        public override string ExpresswayName => _Fields?.ElementAtOrDefault(0);
        public override string ExpresswayAlias => _Fields?.ElementAtOrDefault(1);
        public override string ExpresswayOperator => _Fields?.ElementAtOrDefault(2);
        public override string ExpresswayComment => _Fields?.ElementAtOrDefault(3);

        public override string TollBoothName => _Fields?.ElementAtOrDefault(4);
        public override string TollBoothComment => _Fields?.ElementAtOrDefault(5);

        public override string ExpresswayCode => _Fields?.ElementAtOrDefault(6);
        public override string TollBoothCode => _Fields?.ElementAtOrDefault(7);

        public override bool Read()
        {
            for (var l = _Reader.ReadLine(); l != null; l = _Reader.ReadLine())
            {
                RowIndex++;
                if (string.IsNullOrEmpty(l) || l[0] == '#')
                {
                    continue;
                }
                _Fields = l.Split('\t');
                if (RowIndex == 0 && ExpresswayName == "路線名")
                {
                    continue;
                }
                return true;
            }
            _Fields = null;
            return false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && LeaveOpen)
            {
                _Reader.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}