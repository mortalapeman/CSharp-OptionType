using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionType {
    /// <summary>
    /// Static helper methods for creating Option types.
    /// </summary>
    public static class Option {
        /// <summary>
        /// Creates an option type that represents a successful computation.
        /// </summary>
        /// <typeparam name="T">Return type of the computation.</typeparam>
        /// <param name="obj">Object to return from the compuation.</param>
        /// <returns>An option type for a successful operation.</returns>
        public static IOption<T> Some<T>(T obj) { return new Some<T>(obj); }

        /// <summary>
        /// Creates an option type that represents a failed computation.
        /// </summary>
        /// <typeparam name="T">Return type of the computation.</typeparam>
        /// <param name="obj">Object used to determine the generic type T.</param>
        /// <returns>An option type of a failed operation.</returns>
        public static IOption<T> None<T>(T obj) { return new None<T>(); }

        /// <summary>
        /// Creates an option type that represents a failed computation.
        /// </summary>
        /// <typeparam name="T">Return type of the computation.</typeparam>
        /// <returns>An option type of a failed operation.</returns>
        public static IOption<T> None<T>() { return new None<T>(); }
    }

    internal abstract class Option<T> {

        public ISomeContext<T, R> Some<R>(Func<T, R> func) {
            var builder = new OptionVisitorBuilder<T, R>().Some(func);
            return new SomeContextHelper<T, R>(this, builder);
        }

        internal abstract R Accept<R>(OptionVisitor<T, R> visitor);

        private class SomeContextHelper<TSome, TResult> : ISomeContext<TSome, TResult> {
            OptionVisitorBuilder<TSome, TResult> builder;
            Option<TSome> option;

            internal SomeContextHelper(Option<TSome> option, OptionVisitorBuilder<TSome, TResult> builder) {
                this.builder = builder;
                this.option = option;
            }

            public TResult None(Func<INoneContext<TResult>, TResult> func) {
                return option.Accept(builder.None(func).Build());
            }

            public TResult None(TResult defaultValue) {
                return option.Accept(builder.None(x => x.Default(defaultValue)).Build());
            }
        }
    }
}
