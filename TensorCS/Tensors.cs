using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TensorCS.Core {


    public class IntTensor : BaseTensor<int> {
        public IntTensor(Shape shape) : base(shape) { }
        public IntTensor(Shape shape, int initialValue) : base(shape, initialValue) { }
        public IntTensor(Shape shape, int[] values) : base(shape, values) { }


        internal override int AddT(int x, int y) => x + y;

        internal override int DivideT(int x, int y) => x / y;

        internal override int MultiplyT(int x, int y) => x * y;

        internal override int SubtractT(int x, int y) => x - y;
    }

    public class FloatTensor : BaseTensor<float> {
        public FloatTensor(Shape shape) : base(shape) { }
        public FloatTensor(Shape shape, float initialValue) : base(shape, initialValue) { }
        public FloatTensor(Shape shape, float[] values) : base(shape, values) { }


        internal override float AddT(float x, float y) => x + y;

        internal override float DivideT(float x, float y) => x / y;

        internal override float MultiplyT(float x, float y) => x * y;

        internal override float SubtractT(float x, float y) => x - y;
    }

    public class DoubleTensor : BaseTensor<double> {
        public DoubleTensor(Shape shape) : base(shape) { }
        public DoubleTensor(Shape shape, double initialValue) : base(shape, initialValue) { }
        public DoubleTensor(Shape shape, double[] values) : base(shape, values) { }


        internal override double AddT(double x, double y) => x + y;

        internal override double DivideT(double x, double y) => x / y;

        internal override double MultiplyT(double x, double y) => x * y;

        internal override double SubtractT(double x, double y) => x - y;
    }

    public class ByteTensor : BaseTensor<byte> {
        public ByteTensor(Shape shape) : base(shape) { }
        public ByteTensor(Shape shape, byte initialValue) : base(shape, initialValue) { }
        public ByteTensor(Shape shape, byte[] values) : base(shape, values) { }


        internal override byte AddT(byte x, byte y) => (byte)(x + y);

        internal override byte DivideT(byte x, byte y) => (byte)(x / y);

        internal override byte MultiplyT(byte x, byte y) => (byte)(x * y);

        internal override byte SubtractT(byte x, byte y) => (byte)(x - y);
    }
}
