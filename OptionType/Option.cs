using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionType {
    /// <summary>
    /// Static helper class for creation Option type instances.
    /// </summary>
    public static class Option {
        /// <summary>
        /// Static helper method for creation of Option type instances.
        /// </summary>
        /// <typeparam name="T">Any non value type.</typeparam>
        /// <param name="obj">Object to wrap with Option type.</param>
        /// <returns>An Option type wrapper around the supplied obj.</returns>
        public static Option<T> Create<T>(T obj) where T : class {
            return new Option<T>(obj);
        }
    }

    /// <summary>
    /// Class that requires explicit handling of null values before returning
    /// its contained value.
    /// </summary>
    /// <typeparam name="T">Any non value type.</typeparam>
    public class Option<T> where T : class {
        private T value;

        /// <summary>
        /// Contructor for Option type.
        /// </summary>
        /// <param name="value">Value to wrap with Option type.</param>
        public Option(T value) {
            this.value = value;
        }

        /// <summary>
        /// Begins the context shift for handling of non null values.
        /// </summary>
        /// <typeparam name="R">Return type of the converter funtion.</typeparam>
        /// <param name="converter">Function to convert the internal value to type R.</param>
        /// <returns>Returns the next context used to deal with null values.</returns>
        public ISomeContext<T, R> Some<R>(Func<T, R> converter) {
            return new SomeContext<T, R>(value, converter);
        }

        private class NoneContext<R> : INoneContext<R>{
            private R value;
            private bool isSome;

            public NoneContext(R value, bool isSome) {
                this.value = value;
                this.isSome = isSome;
            }

            public R Throw(Exception e) {
                if (isSome)
                    return value;
                throw e;
            }

            public R Default() {
                if (isSome)
                    return value;
                return default(R);
            }

            public R Default(R value) {
                if (isSome)
                    return this.value;
                return value;
            }

            public R Do(Func<R> func) {
                if (isSome)
                    return value;
                return func();
            }
        }

        private class SomeContext<T, R> : ISomeContext<T, R> where T : class {
            private T value;
            private Func<T, R> convert;

            public SomeContext(T value, Func<T, R> convert) {
                this.value = value;
                this.convert = convert;
            }

            public R None(Func<INoneContext<R>, R> func) {
                if (IsSome())
                    return func(new NoneContext<R>(convert(value), true));
                return func(new NoneContext<R>(default(R), false));
            }

            public R None(R defaultValue) {
                return this.None(x => x.Default(defaultValue));
            }

            private bool IsSome() {
                return !EqualityComparer<T>.Default.Equals(default(T), value);
            }
        }
    }
}
