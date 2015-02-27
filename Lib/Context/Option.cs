using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functional
{
    public abstract class Option<T> {
        readonly T Value;
        Option() { }
        Option(T value) { Value = value; }

        public sealed class Some : Option<T> {
            public Some(T value) : base(value) { }
        }

        public sealed class None : Option<T> {
            public None() : base() { }
        }

        public TOutput Match<TOutput>(Func<T, TOutput> some, Func<TOutput> none) {
            return this is Some ? some(this.Value) : none();
        }
    }
}
