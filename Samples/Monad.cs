using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Functional.Util;

namespace Functional {
    static class MonadTest {
        public static void TestSeq() {

        }

        public static void TestOption() {

        }

        public static async Task TestAsync() {
            var downloader = new WebPageDownloader();
            Func<string, Task<string>> fetch10 =
                url => downloader.FetchLinesAsync(url, 10);

            var res1 = await Async.Bind(
                fetch10("http://microsoft.github.io"),
                x => Async.Bind(
                    fetch10("http://fsharp.org"),
                    y => Async.Bind(
                        fetch10("http://funscript.info"),
                        z => Async.Return(x + y + z)
                    )
                )
            );

            var res2 = await (
                from x in fetch10("http://microsoft.github.io")
                from y in fetch10("http://fsharp.org")
                from z in fetch10("http://funscript.info")
                select x + y + z
            );

            Console.WriteLine("Chars fetched with verbose syntax: {0}", res1.Length);
            Console.WriteLine("Chars fetched with LINQ syntax: {0}", res2.Length);
        }
    }
}
