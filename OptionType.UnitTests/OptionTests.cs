using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OptionType.UnitTests {
    [TestClass]
    public class OptionTests {
        [TestMethod]
        public void TestOption() {
            // Arrange
            var option = new OptionTests()
                .ToOption();

            // Act
            var result = option
                .Some(x => x)
                .None(x => x.Default());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OptionTests));
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void TestOption_WithException() {
            // Arrange
            var option = default(OptionTests)
                .ToOption();

            // Act
            var result = option
                .Some(x => x)
                .None(x => x.Throw(new NotImplementedException()));

            // Assert
            // That an exception is thrown.
        }

        private class Example {
            public int Value { get; set; }
        }

        [TestMethod]
        public void TestExampleUsage() {
            // Act
            var result = new Example().ToOption()
                .Some(x => x)
                .None(x => x.Throw(new ArgumentNullException("Is none.")));

            var linqReult = new Example[] { }
                .Where(x => x is Example)
                .FirstOrDefault().ToOption()
                .Some(x => x)
                .None(x => x.Default(new Example()));

            var propertyAccess = linqReult.Value;

            // Assert
            // That no exceptions are thrown.
        }
    }
}
