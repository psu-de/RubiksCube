using Rubiks.Moves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubiks.Solver {
    public class OldHoffmanSolver : Solver {


        private const string YPerm = "(R U' R' U') (R U R' F') (R U R' U') R' F R";


        public OldHoffmanSolver(RubiksCube cube) : base(cube) {
        }




        private IEnumerable<RubiksMove> SolveCorners() {
            throw new NotImplementedException();


            int bufferSlot = GetFaceIndex(RubiksFace.UP) + (int)PieceType.TopLeftCorner;
            var bufferCorner = GetCorner(bufferSlot);
            int currentSlot = bufferSlot;

            List<int> solveOrder = new List<int>();
            List<int[]> cornersToSolve = Corners.ToList();
            cornersToSolve.Remove(new int[] { bufferCorner.X, bufferCorner.Y, bufferCorner.Z });

            while (cornersToSolve.Count > 0) {
                var corner = GetCorner(currentSlot);

                if (corner == bufferCorner) {
                    // Take any other unsolved corner and continue
                    solveOrder.Add(cornersToSolve.First()[0]);
                    currentSlot = cornersToSolve.First()[0];
                    continue;
                }

                var cornerColors = GetCornerColors(corner);
                var colorOnTop = this.Cube.Data[currentSlot];

                var desiredCorner = GetDesiredCorner(cornerColors);
                
                // TODO: Was passiert wenn eine Corner in der richtigen position ist aber verdreht?

                int index;
                if (GetFaceOn(desiredCorner.X) == GetFaceOfColor(colorOnTop)) {
                    index = desiredCorner.X;
                } else if (GetFaceOn(desiredCorner.Y) == GetFaceOfColor(colorOnTop)) {
                    index = desiredCorner.Y;
                } else if (GetFaceOn(desiredCorner.Z) == GetFaceOfColor(colorOnTop)) {
                    index = desiredCorner.Z;
                } else throw new Exception();

                cornersToSolve.Remove(cornersToSolve.First(x => x.Contains(index)));
                solveOrder.Add(index);
                currentSlot = index;
            }
        }

        public override IEnumerable<RubiksMove> Solve() {
            foreach (var move in SolveCorners())
                yield return move;
            throw new NotImplementedException();
        }
    }
}
