using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shipwreck.Etc
{
    public abstract class TollBoothLoader
    {
        public abstract Task<IEnumerable<TollBooth>> LoadAsync();

        public async Task<TollBoothDictionary> LoadTollBoothDictionaryAsync()
        {
            var tbs = await LoadAsync().ConfigureAwait(false);
            return new TollBoothDictionary(tbs);
        }
    }
}