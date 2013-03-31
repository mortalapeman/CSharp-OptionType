using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionType {
    public static class OptionExtension {
        public static IOption<T> ToOption<T>(this T obj) where T : class {
            return obj.ToOption(x => !object.ReferenceEquals(x, default(T)));
        }

        public static IOption<T> ToOption<T>(this T obj, Func<T, bool> isSome) {
            if (isSome(obj)) {
                return Option.Some(obj);
            } else {
                return Option.None<T>();
            }
        }
    }
}
