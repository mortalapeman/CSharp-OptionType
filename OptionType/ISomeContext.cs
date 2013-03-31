using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionType {
    /// <summary>
    /// Contract for contextual handling of Option type computations.
    /// </summary>
    /// <typeparam name="T">Any non value type.</typeparam>
    /// <typeparam name="R">Type to convert to from T.</typeparam>
    public interface ISomeContext<T, R> {

        /// <summary>
        /// Begins the context shift for handling of failed computations.
        /// </summary>
        /// <param name="func">Provides the implementation for failed computations.</param>
        /// <returns>Returns the type to convert to from type T.</returns>
        R None(Func<INoneContext<R>, R> func);

        /// <summary>
        /// Convenience method for returning a defautl value of type R.
        /// </summary>
        /// <param name="defaultValue">Default value to return.</param>
        /// <returns>Returns the value given by the input if the context is None.</returns>
        R None(R defaultValue);
    }
}
