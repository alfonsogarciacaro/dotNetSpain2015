using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functional {
    class Program {
        static void Main(string[] args) {
            ApplicativeTest.TestSeq();
            ApplicativeTest.TestOption();
            ApplicativeTest.TestAsync().Wait();

            MonadTest.TestSeq();
            MonadTest.TestOption();
            MonadTest.TestAsync().Wait();

            Console.ReadLine();
        }
    }
}
