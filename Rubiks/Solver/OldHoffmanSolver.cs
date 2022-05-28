using Rubiks.Moves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubiks.Solver {
    public class OldHoffmanSolver : Solver {


        private const string CornerSwap = "R U' R' U' R U R' F' R U R' U' R' F R";
        private const string EdgeSwap = "R U R' U' R' F R2 U' R' U' R U R' F'";
        private const string Parity = "R U' R' U' R U R D R' U' R D' R' U2 R' U'";


        public OldHoffmanSolver(RubiksCube cube) : base(cube) {

        }

        private int[] GetEdgeSolveOrder() {
            int bufferSlot = GetFaceIndex(RubiksFace.UP) + (int)PieceType.RightEdge;
            var bufferEdge = GetEdge(bufferSlot);

            var currentSlot = bufferSlot;
            var edgesToSolve = this.Edges.ToList();
            edgesToSolve.Remove(bufferEdge);
            edgesToSolve = edgesToSolve.Where(x => x != GetDesiredEdge(GetEdgeColors(x))).ToList();

            bool newCycle = false;

            List<int> solveOrder = new List<int>();

            while (true) {
                var currentEdge = GetEdge(currentSlot);
                var desiredEdge = GetDesiredEdge((this.Cube.Data[(long)currentSlot], this.Cube.Data[(long)(currentEdge.X == currentSlot ? currentEdge.Y : currentEdge.X)]));

                if (GetEdge(desiredEdge.X) == bufferEdge) {
                    newCycle = true;
                    break;
                }

                solveOrder.Add(desiredEdge.X);
                edgesToSolve.Remove(GetEdge(desiredEdge.X));
                currentSlot = desiredEdge.X;

                if (edgesToSolve.Count == 0)
                    break;
            }

            while (newCycle) {
                newCycle = false;
                var newCycleEdge = edgesToSolve.First();
                solveOrder.Add(newCycleEdge.X);
                currentSlot = newCycleEdge.X;


                while (true) {
                    var currentEdge = GetEdge(currentSlot);
                    var desiredEdge = GetDesiredEdge((this.Cube.Data[(long)currentSlot], this.Cube.Data[(long)(currentEdge.X == currentSlot ? currentEdge.Y : currentEdge.X)]));

                    if (GetEdge(desiredEdge.X) == bufferEdge) {
                        newCycle = true;
                        break;
                    }

                    if (GetEdge(desiredEdge.X) == newCycleEdge) {
                        solveOrder.Add(desiredEdge.X);
                        edgesToSolve.Remove(GetEdge(desiredEdge.X));
                        newCycle = edgesToSolve.Count > 0;
                        break;
                    }


                    solveOrder.Add(desiredEdge.X);
                    edgesToSolve.Remove(GetEdge(desiredEdge.X));
                    currentSlot = desiredEdge.X;

                    if (edgesToSolve.Count == 0)
                        break;
                }
            }

            return solveOrder.ToArray();
        }

        private int[] GetCornerSolveOrder() {
            int bufferSlot = GetFaceIndex(RubiksFace.UP) + (int)PieceType.TopLeftCorner;
            var bufferCorner = GetCorner(bufferSlot);

            var currentSlot = bufferSlot;
            var cornersToSolve = this.Corners.ToList();
            cornersToSolve.Remove(bufferCorner);
            cornersToSolve = cornersToSolve.Where(x => x != GetDesiredCorner(GetCornerColors(x))).ToList();

            bool newCycle = false;

            List<int> solveOrder = new List<int>();

            while (true) {
                var currentCorner = GetCorner(currentSlot);
                var desiredCorner = GetDesiredCorner((this.Cube.Data[(long)currentSlot],
                    this.Cube.Data[(long)(currentCorner.X == currentSlot ? currentCorner.Y : currentCorner.X)],
                    this.Cube.Data[(long)(currentCorner.Z == currentSlot ? currentCorner.Y : currentCorner.Z)]));

                if (GetCorner(desiredCorner.X) == bufferCorner) {
                    newCycle = true;
                    break;
                }

                solveOrder.Add(desiredCorner.X);
                cornersToSolve.Remove(GetCorner(desiredCorner.X));
                currentSlot = desiredCorner.X;

                if (cornersToSolve.Count == 0)
                    break;
            }

            while (newCycle) {
                newCycle = false;
                var newCycleCorner = cornersToSolve.First();
                solveOrder.Add(newCycleCorner.X);
                currentSlot = newCycleCorner.X;


                while (true) {
                    var currentCorner = GetCorner(currentSlot);
                    var desiredCorner = GetDesiredCorner((this.Cube.Data[(long)currentSlot],
                        this.Cube.Data[(long)(currentCorner.X == currentSlot ? currentCorner.Y : currentCorner.X)],
                        this.Cube.Data[(long)(currentCorner.Z == currentSlot ? currentCorner.Y : currentCorner.Z)]));

                    if (GetCorner(desiredCorner.X) == bufferCorner) {
                        newCycle = true;
                        break;
                    }

                    if (GetCorner(desiredCorner.X) == newCycleCorner) {
                        solveOrder.Add(desiredCorner.X);
                        cornersToSolve.Remove(GetCorner(desiredCorner.X));
                        newCycle = cornersToSolve.Count > 0;
                        break;
                    }


                    solveOrder.Add(desiredCorner.X);
                    cornersToSolve.Remove(GetCorner(desiredCorner.X));
                    currentSlot = desiredCorner.X;

                    if (cornersToSolve.Count == 0)
                        break;
                }
            }

            return solveOrder.ToArray();
        }

        private IEnumerable<RubiksMove> SolveCorners(int[] solveOrder) {

            foreach (var cornerIndex in solveOrder) {
                var setupMove = cornerIndex switch {
                    45 => throw new NotSupportedException(),
                    47 => "R2",
                    53 => "F2 D",
                    51 => "F2",
                    27 => throw new NotSupportedException(),
                    29 => "F' D",
                    35 => "F'",
                    33 => "D' R",
                    0 => "F R'",
                    2 => "R'",
                    8 => "F' R'",
                    6 => "F2 R'",
                    9 => "F",
                    11 => "R' F",
                    17 => "R2 F",
                    15 => "R F",
                    18 => "R D'",
                    20 => throw new NotSupportedException(),
                    26 => "D F'",
                    24 => "R",
                    36 => "D",
                    38 => "",
                    44 => "D'",
                    42 => "D2",

                    _ => throw new ArgumentException()
                };

                var moves = Move.ParseWithSetupMove(setupMove, CornerSwap);
                foreach (var move in moves)
                    yield return move;
            }
        }



        private IEnumerable<RubiksMove> SolveEdges(int[] solveOrder) {

            foreach (var edgeIndex in solveOrder) {
                var setupMove = edgeIndex switch {
                    46 => "l2 D' l2",
                    52 => "l2 D l2",
                    48 => "",
                    28 => "L d' L",
                    32 => "d' L",
                    34 => "L' d' L",
                    30 => "d L'",
                    1 => "l D' L2",
                    5 => "d2 L",
                    7 => "l D L2",
                    3 => "L'",
                    14 => "d L",
                    16 => "D' l D L2",
                    12 => "d' L'",
                    19 => "l' D L2",
                    23 => "L",
                    25 => "l' D' L2",
                    21 => "d2 L'",
                    37 => "D' L2",
                    41 => "D2 L2",
                    43 => "D L2",
                    39 => "L2",
                    _ => throw new ArgumentException()
                };

                var moves = Move.ParseWithSetupMove(setupMove, EdgeSwap);
                foreach (var move in moves)
                    yield return move;
            }
        }

        public override IEnumerable<RubiksMove> Solve() {


            foreach (var move in RotateToDefaultOrientation()) {
                this.Cube.Move(move);
                yield return move;
            }

            var edgeSolveOrder = GetEdgeSolveOrder();

            foreach (var move in SolveEdges(edgeSolveOrder)) {
                this.Cube.Move(move);
                yield return move;
            }

            if (edgeSolveOrder.Length % 2 == 1) {
                foreach (var move in Move.Parse(Parity))
                    yield return move;
            }
            
            while(!Cube.IsSolved()) { // Manchmal sind irgendwie mehrere Iterationen für die Ecksteine nötig, scheint dann aber zuverlässig zu funktionieren

                var cornerSolveOrder = GetCornerSolveOrder();
                foreach (var move in SolveCorners(cornerSolveOrder)) {
                    this.Cube.Move(move);
                    yield return move;
                }
            }


        }
    }
}
