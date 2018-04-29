using Flurl.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SubstringExtension;
using System.Linq;
using System.Net;

namespace xkcd.tests
{
    [TestClass]
    public class xkcdDownloadTests
    {
        [TestMethod]
        public void Main()
        {
            for (int i = 1; i < 100; i++)
            {
                var link = getPictureLink(i);
                save(i, link);
            }
        }

        private string getPictureLink(int num)
        {
            var source = $"https://xkcd.com/{num}/".GetStringAsync().Result;
            var imageLink = source.Substring("imgs.xkcd.com/", "\"");
            var link = $"http://imgs.xkcd.com/{imageLink}";
            return link;
        }

        private void save(int i, string link)
        {
            var name = getName(link);
            using (var client = new WebClient())
            {
                client.DownloadFile(link, name);
            }
        }

        private string getName(string link)
        {
            return link.Split('/').Last();
        }

    }
}
