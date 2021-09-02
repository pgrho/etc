using System;

namespace Shipwreck.Etc
{
    public abstract class TollBoothReader : IDisposable
    {
        protected bool IsDisposed { get; private set; }

        public abstract string ExpresswayName { get; }
        public abstract string ExpresswayAlias { get; }
        public abstract string ExpresswayOperator { get; }
        public abstract string ExpresswayComment { get; }

        public abstract string TollBoothName { get; }
        public abstract string TollBoothComment { get; }

        public abstract string ExpresswayCode { get; }
        public abstract string TollBoothCode { get; }

        protected virtual void Dispose(bool disposing)
            => IsDisposed = true;

        public abstract bool Read();

        public TollBooth ToTollBooth()
            => new TollBooth
            {
                ExpresswayName = ExpresswayName,
                ExpresswayAlias = ExpresswayAlias,
                ExpresswayOperator = ExpresswayOperator,
                ExpresswayComment = ExpresswayComment,

                TollBoothName = TollBoothName,
                TollBoothComment = TollBoothComment,
                ExpresswayCode = ExpresswayCode,
                TollBoothCode = TollBoothCode
            };

        public void Dispose()
        {
            // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}