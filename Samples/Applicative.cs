using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Functional.Util;

namespace Functional {
    static class ApplicativeTest {
        public static void TestSeq() {
            Func<int, Func<int, int>> add = x => y => x + y;
            var lifted1 = Seq.Pure(add);
            var lifted2 = Seq.Apply(lifted1, new int[] { 1, 2 });
            var res = Seq.Apply(lifted2, new int[] { 3, 4 });

            foreach (var x in res)
                Console.Write("{0}, ", x);
            Console.WriteLine();

        }       
                
        public static void TestOption() {
                
        }
                
        public static async Task TestAsync() {
            var downloader = new WebPageDownloader();
            Func<string, Task<string>> fetch10 =
                url => downloader.FetchLinesAsync(url, 10);

            Func<string, Func<string, Func<string, string>>> curry =
                x => y => z => x + y + z;

            var res1 = await
                Async.Apply(
                    Async.Apply(
                        Async.Apply(
                            Async.Pure(curry),
                            fetch10("http://microsoft.github.io")),
                        fetch10("http://fsharp.org")),
                    fetch10("http://funscript.info")
                );

            var res2 = await Async.Lift3(
                (x,y,z) => x+y+z,
                fetch10("http://microsoft.github.io"),
                fetch10("http://fsharp.org"),
                fetch10("http://funscript.info")
            );

            Console.WriteLine("Chars fetched with verbose syntax: {0}", res1.Length);
            Console.WriteLine("Chars fetched with Lift3: {0}", res2.Length);
        }
    }
}
