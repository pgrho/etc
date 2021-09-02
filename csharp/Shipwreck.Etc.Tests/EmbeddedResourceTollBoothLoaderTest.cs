using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Shipwreck.Etc
{
    public class EmbeddedResourceTollBoothLoaderTest
    {
        [Fact]
        public async Task LoadAsyncTest()
        {
            var es = await new EmbeddedResourceTollBoothLoader().LoadAsync();
            Assert.Equal(2071, es.Count());
        }
        [Fact]
        public async Task FindTest1()
        {
            var dic = await new EmbeddedResourceTollBoothLoader().LoadTollBoothDictionaryAsync();
            var tolls = dic.Find(03, 255);
            Assert.Equal(2, tolls.Count());
        }

        [Fact]
        public async Task FindTest2()
        {
            var dic = await new EmbeddedResourceTollBoothLoader().LoadTollBoothDictionaryAsync();
            var tolls = dic.Find("03", "255");
            Assert.Equal(2, tolls.Count());
        }
    }
}