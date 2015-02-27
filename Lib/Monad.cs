using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functional {
    public static partial class Seq {
        public static IEnumerable<a> Return<a>(a input) {
            return Pure(input);
        }

        public static IEnumerable<b> Bind<a, b>(IEnumerable<a> input, Func<a, IEnumerable<b>> f) {
            return input.SelectMany<a, b>(f);
        }
    }

    public static partial class Option {
        public static Option<a> Return<a>(a input) {
            return Pure(input);
        }

        public static Option<b> Bind<a, b>(Option<a> input, Func<a, Option<b>> f) {
            return input.Match(
                some: v => f(v),
                none: () => new Option<b>.None()
            );
        }
    }

    public static partial class Async {
        public static Task<a> Return<a>(a input) {
            return Pure(input);
        }

        /// <summary>Caution: Simplified code, no error nor cancellation checks</summary>
        public static Task<b> Bind<a, b>(Task<a> input, Func<a, Task<b>> f) {
            var tcs = new TaskCompletionSource<b>();
            input.ContinueWith(x =>
                f(x.Result).ContinueWith(y =>
                    tcs.SetResult(y.Result)));
            return tcs.Task;
        }

        public static Task<c> SelectMany<a,b,c>(
            this Task<a> input, Func<a, Task<b>> f, Func<a, b, c> projection) {

            return Bind(input, outer =>
                Bind(f(outer), inner =>
                    Return(projection(outer, inner))));
        }
    }
}
