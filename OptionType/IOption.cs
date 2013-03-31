using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionType {
    /// <summary>
    /// Type that represents either a successful or failed computation.
    /// </summary>
    /// <typeparam name="T">Return value of the computation.</typeparam>
    public interface IOption<T> {
        ISomeContext<T, R> Some<R>(Func<T, R> fun);
    }
}
