using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionType {
    internal class OptionVisitorBuilder<T, R> {
        Func<T, R> someTransform;
        Func<R> noneResult;

        public OptionVisitorBuilder<T, R> Some(Func<T, R> func) {
            this.someTransform = func;
            return this;
        }

        public OptionVisitorBuilder<T, R> None(Func<INoneContext<R>, R> func) {
            noneResult = () => func(new NoneContextHelper<R>());
            return this;
        }

        public OptionVisitor<T, R> Build() {
            return new OptionVisitor<T, R>(someTransform, noneResult);
        }

        private class NoneContextHelper<TResult> : INoneContext<TResult> {
            public TResult Throw(Exception e) {
                throw e;
            }

            public TResult Default() {
                return default(TResult);
            }

            public TResult Default(TResult value) {
                return value;
            }

            public TResult Do(Func<TResult> func) {
                return func();
            }
        }
    }
}
