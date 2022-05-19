using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TensorCS.Core {
    public class Shape {

        public long[] Dimensions { get; private set; }
        public long Total => this.DimensionLengths[0] * this.GetDimension(0);
        public int Rank => this.Dimensions.Length;


        public long[] DimensionLengths { get; private set; }

        public Shape(params long[] dimensions) {

            if (dimensions.Length == 0) throw new ArgumentException("Need at least one dimension");

            this.Dimensions = dimensions;
            this.DimensionLengths = new long[this.Rank];
            this.createDimensionLengths();
        }

        private void createDimensionLengths() {

            this.DimensionLengths[this.Rank - 1] = 1;

            if (this.Rank > 1) {

                long length = 1;
                for (int i = this.Dimensions.Length - 2; i >= 0; i--) {
                    length *= this.Dimensions[i + 1];
                    this.DimensionLengths[i] = length;
                }
            }
        }

        public long GetDimension (int index) {
            return this.Dimensions[index];
        }

        public long this[int index] {
            get { return this.Dimensions[index]; }
            set { throw new NotSupportedException(); }
        }

        public override bool Equals(object? obj) {
            if (obj == null) return false;
            if (!(obj is Shape)) return false;

            Shape other = (Shape)obj;
            return other.Dimensions.SequenceEqual(this.Dimensions);
        }

        public override string ToString() {
            return $"({string.Join(',', this.Dimensions)})";
        }

    }
}
