using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionType {
    internal class None<T> : Option<T>, IOption<T> {

        internal None() { }

        internal override R Accept<R>(OptionVisitor<T, R> visitor) {
            return visitor.Visit(this);
        }
    }
}
