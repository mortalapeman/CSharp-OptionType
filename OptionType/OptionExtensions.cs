using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionType {
    public static class OptionExtension {
        public static Option<T> ToOption<T>(this T obj) where T : class {
            return Option.Create(obj);
        }
    }
}
