using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functional {
    public static partial class Seq {
        public static IEnumerable<b> Map<a, b>(IEnumerable<a> input, Func<a, b> f) {
            return input.Select(f);
        }
    }

    public static partial class Option {
        public static Option<b> Map<a, b>(Option<a> input, Func<a, b> f) {
            return input.Match(
                some:  v => (Option<b>)new Option<b>.Some(f(v)),
                none: () => (Option<b>)new Option<b>.None()
            );
        }
    }

    public static partial class Async {
        public static Task<b> Map<a, b>(Task<a> input, Func<a, b> f) {
            return input.ContinueWith(t => f(t.Result));
        }
    }
}
