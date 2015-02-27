using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functional {
    public static partial class Seq {
        // First lift the curried function to the domain of the context
        public static IEnumerable<a> Pure<a>(a input) {
            return new a[] { input };
        }

        // Then apply the function to the first operand. As the result is a partially applied
        // function, we can still apply it to the rest of the operands
        public static IEnumerable<b> Apply<a, b>(IEnumerable<Func<a, b>> liftedFn, IEnumerable<a> input) {
            foreach (var f in liftedFn)
                foreach (var x in input)
                    yield return f(x);
        }
    }

    public static partial class Option {
        public static Option<a> Pure<a>(a input) {
            return new Option<a>.Some(input);
        }

        public static Option<b> Apply<a, b>(Option<Func<a, b>> liftedFn, Option<a> input) {
            return liftedFn.Match(
                some: f => Option.Map(input, f),
                none: () => new Option<b>.None()
            );
        }
    }

    public static partial class Async {
        public static Task<a> Pure<a>(a input) {
            return Task.Run(() => input);
        }

        /// <summary>Caution: Simplified code, no error nor cancellation checks</summary>
        public static Task<b> Apply<a, b>(Task<Func<a, b>> liftedFn, Task<a> input) {
            var tcs = new TaskCompletionSource<b>();
            liftedFn.ContinueWith(f =>
                input.ContinueWith(x =>
                    tcs.SetResult(f.Result(x.Result))
            ));
            return tcs.Task;
        }
    }
}
