using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionType {
    internal class OptionVisitor<T, R> {
        Func<T, R> someTransform;
        Func<R> noneResult;

        public OptionVisitor(Func<T, R> someTransform, Func<R> noneResult) {
            this.someTransform = someTransform;
            this.noneResult = noneResult;
        }

        public R Visit(Some<T> some) {
            return someTransform(some.Value);
        }

        public R Visit(None<T> none) {
            return noneResult();
        }
    }
}
