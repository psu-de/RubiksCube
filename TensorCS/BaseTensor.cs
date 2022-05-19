using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TensorCS.Core {
    public abstract class BaseTensor<T> where T : struct {

        public Shape Shape { get; private set; }


        private T[] _values;


        public BaseTensor(Shape shape) {
            this.Shape = shape;

            this._values = new T[this.Shape.Total];
        }

        public BaseTensor(Shape shape, T initialValue) : this(shape) {
            for (long i = 0; i < this.Shape.Total; i++) {
                this._values[i] = initialValue;
            }
        }

        public BaseTensor(Shape shape, T[] values) {
            this.Shape = shape;
            if (values.Length != this.Shape.Total)
                throw new ArgumentException($"Initial values must be the length of Shape.Total (Expected: {shape.Total}, Got: {values.Length})");
            this._values = values;
        }

        #region Operation

        public BaseTensor<T> Add (BaseTensor<T> other) {
            if (!this.Shape.Equals(other.Shape)) {
                throw new ArgumentException("Other Tensor must be of the same shape");
            }

            BaseTensor<T> result = this.Create(this.Shape);
            for (long i = 0; i < this.Shape.Total; i++) {
                result[i] = this.AddT(this[i], other[i]);
            }

            return result;
        }

        public BaseTensor<T> Subtract(BaseTensor<T> other) {
            if (!this.Shape.Equals(other.Shape)) {
                throw new ArgumentException("Other Tensor must be of the same shape");
            }

            BaseTensor<T> result = this.Create(this.Shape);
            for (long i = 0; i < this.Shape.Total; i++) {
                result[i] = this.SubtractT(this[i], other[i]);
            }

            return result;
        }

        public BaseTensor<T> Multiply(BaseTensor<T> other) {
            if (!this.Shape.Equals(other.Shape)) {
                throw new ArgumentException("Other Tensor must be of the same shape");
            }

            BaseTensor<T> result = this.Create(this.Shape);
            for (long i = 0; i < this.Shape.Total; i++) {
                result[i] = this.MultiplyT(this[i], other[i]);
            }

            return result;
        }

        public BaseTensor<T> Divide(BaseTensor<T> other) {
            if (!this.Shape.Equals(other.Shape)) {
                throw new ArgumentException("Other Tensor must be of the same shape");
            }

            BaseTensor<T> result = this.Create(this.Shape);
            for (long i = 0; i < this.Shape.Total; i++) {
                result[i] = this.DivideT(this[i], other[i]);
            }

            return result;
        }

        public BaseTensor<T> Flatten() {
            BaseTensor<T> result = this.Create(new Shape(this.Shape.Total), this._values);
            return result;
        }

        public BaseTensor<T> Reshape(Shape shape) {
            if (this.Shape.Total != shape.Total) {
                throw new ArgumentException($"Shape Total must match (Expected: {this.Shape.Total}, Got: {shape.Total})");
            }

            return this.Create(shape, this._values);
        }

        #endregion

        #region Abstract Methods 

        internal abstract T AddT(T x, T y);
        internal abstract T SubtractT(T x, T y);
        internal abstract T MultiplyT(T x, T y);
        internal abstract T DivideT(T x, T y);

        #endregion

        #region Create

        public BaseTensor<T> Create(Shape shape) {
            Type t = this.GetType();

            BaseTensor<T>? newTensor = Activator.CreateInstance(t, shape) as BaseTensor <T>;
            if (newTensor == null) throw new Exception("Should not happen");
            return newTensor;
        }

        public BaseTensor<T> Create(Shape shape, T initialValue) {
            Type t = this.GetType();

            BaseTensor<T>? newTensor = Activator.CreateInstance(t, shape, initialValue) as BaseTensor<T>;
            if (newTensor == null) throw new Exception("Should not happen");
            return newTensor;
        }

        public BaseTensor<T> Create(Shape shape, T[] values) {
            Type t = this.GetType();

            BaseTensor<T>? newTensor = Activator.CreateInstance(t, shape, values) as BaseTensor<T>;
            if (newTensor == null) throw new Exception("Should not happen");
            return newTensor;
        }

        #endregion

        #region Getters / Setters

        public T[] GetValues () {
            return this._values;
        }

        public T[] GetValues(int start, int length) {
            return this._values.Skip(start).Take(length).ToArray();
        }

        public T GetValue(long index) {
            return _values[index];
        }

        public T GetValue(params int[] indices) {
            long index = this.GetIndex(indices);
            return this.GetValue(index);
        }

        public void SetValue(T value, long index) {
            _values[index] = value;
        }

        public void SetValue(T value, params int[] indices) {
            long index = this.GetIndex(indices);
            this.SetValue(value, index);
        }

        public void SetValues(T[] values, long startIndex) {
            for (int i = 0; i < values.Length; i++) {
                SetValue(values[i], (long)(startIndex + i));
            }
        }

        public long GetIndex(params int[] indices) {
            if (indices.Length != this.Shape.Rank) throw new ArgumentException($"Expected {this.Shape.Rank} indices for Shape ${this.Shape}");

            long index = 0;

            for (int i = 0; i < indices.Length; i++) {
                index += indices[i] * this.Shape.DimensionLengths[i];
            }
            return index;
        }

        public T this[params int[] indices] {
            get { return this.GetValue(indices); }
            set { this.SetValue(value, indices); }
        }

        public T this[long index] {
            get { return this.GetValue(index); }
            set { this.SetValue(value, index); }
        }

        #endregion

        public override string ToString() {


            string buildString(int rank, List<string>? str = null) {
                long dim = this.Shape.GetDimension(rank);
                List<string> newList = new List<string>();

                if (str == null) {
                    for (int i = 0; i < this._values.Length / dim; i++) {
                        newList.Add($"[{string.Join(",", this.GetValues((int)(i * dim), (int)dim))}]");
                    }
                } else {
                    for (int i = 0; i < str.Count / dim; i++) {
                        newList.Add($"[{string.Join(",", str.Skip((int)(i * dim)).Take((int)dim))}]");
                    }
                }
                
                if (rank > 0) {
                    return buildString(rank - 1, newList);
                } else {
                    return newList[0];
                }
            }

            string values = buildString(this.Shape.Rank - 1);


            string str = $"Tensor(shape={this.Shape}, type={this.GetType().Name}, values={values})";
            return str;

        }
    }
}
