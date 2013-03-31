using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionType {
    internal class Some<T> : Option<T>, IOption<T> {
        internal T Value { get; private set; }

        internal Some(T obj) { this.Value = obj; }

        internal override R Accept<R>(OptionVisitor<T, R> visitor) {
            return visitor.Visit(this);
        }
    }
}
