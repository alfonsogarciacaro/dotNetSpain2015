using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functional {
    public static partial class Seq {
        public static IEnumerable<e> Lift2<a, b, e>(Func<a, b, e> f, IEnumerable<a> x, IEnumerable<b> y) {
            Func<a, Func<b, e>> curry = _x => _y => f(_x, _y);
            var lifted1 = Pure(curry);
            var lifted2 = Apply(lifted1, x);
            return Apply(lifted2, y);
        }

        public static IEnumerable<e> Lift3<a, b, c, e>(Func<a, b, c, e> f,
            IEnumerable<a> x, IEnumerable<b> y, IEnumerable<c> z) {
            Func<a, Func<b, Func<c, e>>> curry =
                _x => _y => _z => f(_x, _y, _z);
            var lifted1 = Pure(curry);
            var lifted2 = Apply(lifted1, x);
            var lifted3 = Apply(lifted2, y);
            return Apply(lifted3, z);
        }
        
        public static IEnumerable<e> Lift4<a, b, c, d, e>(Func<a, b, c, d, e> f,
            IEnumerable<a> x, IEnumerable<b> y, IEnumerable<c> z, IEnumerable<d> u) {
            Func<a,Func<b,Func<c,Func<d,e>>>> curry =
                _x => _y => _z => _u => f(_x,_y,_z,_u);
            var lifted1 = Pure(curry);
            var lifted2 = Apply(lifted1, x);
            var lifted3 = Apply(lifted2, y);
            var lifted4 = Apply(lifted3, z);
            return Apply(lifted4, u);
        }
    }

    public static partial class Option {
        public static Option<e> Lift2<a, b, e>(Func<a, b, e> f, Option<a> x, Option<b> y) {
            Func<a, Func<b, e>> curry = _x => _y => f(_x, _y);
            var lifted1 = Pure(curry);
            var lifted2 = Apply(lifted1, x);
            return Apply(lifted2, y);
        }

        public static Option<e> Lift3<a, b, c, e>(Func<a, b, c, e> f,
            Option<a> x, Option<b> y, Option<c> z) {
            Func<a, Func<b, Func<c, e>>> curry =
                _x => _y => _z => f(_x, _y, _z);
            var lifted1 = Pure(curry);
            var lifted2 = Apply(lifted1, x);
            var lifted3 = Apply(lifted2, y);
            return Apply(lifted3, z);
        }

        public static Option<e> Lift4<a, b, c, d, e>(Func<a, b, c, d, e> f,
            Option<a> x, Option<b> y, Option<c> z, Option<d> u) {
            Func<a, Func<b, Func<c, Func<d, e>>>> curry =
                _x => _y => _z => _u => f(_x, _y, _z, _u);
            var lifted1 = Pure(curry);
            var lifted2 = Apply(lifted1, x);
            var lifted3 = Apply(lifted2, y);
            var lifted4 = Apply(lifted3, z);
            return Apply(lifted4, u);
        }
    }

    public static partial class Async {
        public static Task<e> Lift2<a, b, e>(Func<a, b, e> f, Task<a> x, Task<b> y) {
            Func<a, Func<b, e>> curry = _x => _y => f(_x, _y);
            var lifted1 = Pure(curry);
            var lifted2 = Apply(lifted1, x);
            return Apply(lifted2, y);
        }

        public static Task<e> Lift3<a, b, c, e>(Func<a, b, c, e> f,
            Task<a> x, Task<b> y, Task<c> z) {
            Func<a, Func<b, Func<c, e>>> curry =
                _x => _y => _z => f(_x, _y, _z);
            var lifted1 = Pure(curry);
            var lifted2 = Apply(lifted1, x);
            var lifted3 = Apply(lifted2, y);
            return Apply(lifted3, z);
        }

        public static Task<e> Lift4<a, b, c, d, e>(Func<a, b, c, d, e> f,
            Task<a> x, Task<b> y, Task<c> z, Task<d> u) {
            Func<a, Func<b, Func<c, Func<d, e>>>> curry =
                _x => _y => _z => _u => f(_x, _y, _z, _u);
            var lifted1 = Pure(curry);
            var lifted2 = Apply(lifted1, x);
            var lifted3 = Apply(lifted2, y);
            var lifted4 = Apply(lifted3, z);
            return Apply(lifted4, u);
        }
    }
}
