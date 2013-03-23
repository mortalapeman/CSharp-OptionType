using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionType {
    /// <summary>
    /// Context for handling of null values.
    /// </summary>
    /// <typeparam name="R">Type to return to statisfy contract.</typeparam>
    public interface INoneContext<R> {
        /// <summary>
        /// Throws the given exception if the previous context determines
        /// the internal value is a null value.
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
        /// that the internal value is null.
        /// </summary>
        /// <param name="value">Default value to return inplace of default(R).</param>
        /// <returns>Type to satisfy contract.</returns>
        R Default(R value);
        /// <summary>
        /// Runs the supplied funtion to run if the internal value is null.
        /// </summary>
        /// <param name="func">Custom funtion to run if internal value is null.</param>
        /// <returns>Type to satisfy contract.</returns>
        R Do(Func<R> func);
    }
}
