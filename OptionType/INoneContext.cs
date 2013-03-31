using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionType {
    /// <summary>
    /// Context for handling of failed computations.
    /// </summary>
    /// <typeparam name="R">Type to return to statisfy contract.</typeparam>
    public interface INoneContext<R> {
        /// <summary>
        /// Throws the given exception if the computation failed.
        /// </summary>
        /// <param name="e">Exception to be thrown</param>
        /// <returns>Type to satisfy contract.</returns>
        R Throw(Exception e);
        /// <summary>
        /// Return the value of default(R).
        /// </summary>
        /// <returns>Type to satisfy contract.</returns>
        R Default();
        /// <summary>
        /// Returns the given value if the previous context determines
        /// that the computation failed.
        /// </summary>
        /// <param name="value">Default value to return instead of default(R).</param>
        /// <returns>Type to satisfy contract.</returns>
        R Default(R value);
        /// <summary>
        /// Invokes the given funtion to run if the computation fails.
        /// </summary>
        /// <param name="func">Custom funtion to run if hte computation fails.</param>
        /// <returns>Type to satisfy contract.</returns>
        R Do(Func<R> func);
    }
}
