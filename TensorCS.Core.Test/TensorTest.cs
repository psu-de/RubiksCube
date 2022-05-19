using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TensorCS.Core.Test {
    public class TensorTest {

        [Test]
        public void TestCreate () {
            FloatTensor tensor1 = new FloatTensor(new Shape(2, 3, 4), 5);
            FloatTensor tensor2 = new FloatTensor(new Shape(2, 3, 4), 0);


            FloatTensor tensor3 = (FloatTensor)tensor1.Multiply(tensor2);

            TestContext.Out.WriteLine(string.Join(", ", tensor3));
        }

    }
}
