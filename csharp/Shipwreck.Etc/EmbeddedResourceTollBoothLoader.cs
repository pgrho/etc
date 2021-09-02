using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Shipwreck.Etc
{
    public sealed class EmbeddedResourceTollBoothLoader : TollBoothLoader
    {
        public override Task<IEnumerable<TollBooth>> LoadAsync()
        {
            var list = new List<TollBooth>(2100);
            var t = GetType();
            using (var s = t.Assembly.GetManifestResourceStream(t, "tollbooth.tsv"))
            using (var r = new StreamReader(s))
            using (var reader = new TsvTollBoothReader(r))
            {
                while (reader.Read())
                {
                    list.Add(reader.ToTollBooth());
                }
            }
            return Task.FromResult<IEnumerable<TollBooth>>(list);
        }
    }
}