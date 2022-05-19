using NUnit.Framework;
using System.Diagnostics;

namespace TensorCS.Core.Test {
    public class ShapeTests {
        [SetUp]
        public void Setup() {
        }

        [Test]
        public void TestTotal() {
            var shape = new Shape(new long[] { 2, 3, 4 });
            Assert.AreEqual(24, shape.Total);
        }

        [Test]
        public void TestDimensionLengths () {
            var shape = new Shape(new long[] { 2, 3, 4 });
            Assert.AreEqual(new long[] { 12, 4, 1 }, shape.DimensionLengths);
        }
    }
}