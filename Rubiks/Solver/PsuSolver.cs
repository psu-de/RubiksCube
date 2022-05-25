using Rubiks.Moves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubiks.Solver {
    public class PsuSolver : Solver {

        public PsuSolver(RubiksCube cube) : base(cube) {
        }

        //private int GetEdgeNeighborIndex(int index) {
        //    return index switch {
        //        1 => 52,
        //        3 => 32,
        //        5 => 12,
        //        7 => 37,
        //        10 => 50,
        //        12 => 5,
        //        14 => 21,
        //        16 => 41,
        //        19 => 46,
        //        21 => 14,
        //        23 => 30,
        //        25 => 43,
        //        28 => 48,
        //        30 => 23,
        //        32 => 3,
        //        34 => 39,
        //        37 => 7,
        //        39 => 34,
        //        41 => 16,
        //        43 => 25,
        //        46 => 19,
        //        48 => 28,
        //        50 => 10,
        //        52 => 1,
        //        _ => throw new ArgumentException()
        //    };
        //}

        //private (int, int) GetCornerNeighborIndexes(int index) {

        //    int[][] corners = new int[][] {
        //        new int[]{ 0, 29, 51 },
        //        new int[]{ 2, 53, 9  },
        //        new int[]{ 6, 35, 36 },
        //        new int[]{ 8, 15, 38 },
        //        new int[]{ 18, 11, 47 },
        //        new int[]{ 20, 45, 27 },
        //        new int[]{ 24, 17, 44 },
        //        new int[]{ 26, 42, 33 }
        //    };

        //    var corner = corners.FirstOrDefault(x => x.Contains(index));
        //    if (corner == null) throw new ArgumentException("Corner not found");
        //    var cornerIndices = corner.ToList();
        //    cornerIndices.Remove(index);


        //    return (cornerIndices[0], cornerIndices[1]);
        //}

        //private RubiksFace GetFaceOn(int index) {
        //    return (RubiksFace)((index / (3 * 3)) + 1);
        //}


        //public (int, int) FindEdgePiece(RubiksColor color1, RubiksColor color2) {
        //    for (int f = 0; f < 6; f++) {
        //        var faceIndex = Cube.GetFaceIndex((RubiksFace)(f + 1));
        //        for (long i = faceIndex + 1; i < faceIndex + 9; i += 2) {
        //            if (this.Cube.Data[i] == color1) {
        //                int neighbor = GetEdgeNeighborIndex((int)i);
        //                if (this.Cube.Data[(long)neighbor] == color2) {
        //                    return ((int)i, neighbor);
        //                }
        //            }
        //        }
        //    }

        //    throw new KeyNotFoundException();
        //}

        //public (int, int, int) FindCornerPiece(RubiksColor color1, RubiksColor color2, RubiksColor color3) {
        //    for (int f = 0; f < 6; f++) {
        //        var faceIndex = Cube.GetFaceIndex((RubiksFace)(f + 1));
        //        for (int j = 0; j < 9; j += 2) {
        //            if (j == RubiksCube.MiddlePiece) continue;

        //            long i = faceIndex + j;
        //            if (this.Cube.Data[i] == color1) {
        //                (int n1, int n2) = GetCornerNeighborIndexes((int)i);

        //                var c2 = this.Cube.Data[(long)n1];
        //                var c3 = this.Cube.Data[(long)n2];

        //                if (c2 == color2 && c3 == color3) {
        //                    return ((int)i, n1, n2);
        //                } else if (c2 == color3 && c3 == color2) {
        //                    return ((int)i, n2, n1);
        //                }
        //            }
        //        }
        //    }
        //    throw new KeyNotFoundException();
        //}

        //private IEnumerable<RubiksMove> RotateToDefaultOrientation() {
        //    var colorToFaceMap = GetColorToFaceMap();

        //    var currentGreenFace = colorToFaceMap[RubiksColor.Green]; // Should be FRONT
        //    if (currentGreenFace != RubiksFace.FRONT) {
        //        var moves = currentGreenFace switch {
        //            RubiksFace.UP => Move.Parse("X'"),
        //            RubiksFace.RIGHT => Move.Parse("Y"),
        //            RubiksFace.LEFT => Move.Parse("Y'"),
        //            RubiksFace.DOWN => Move.Parse("X"),
        //            RubiksFace.BACK => Move.Parse("Y2")
        //        };
        //        foreach (var move in moves)
        //            yield return move;
        //    }

        //    colorToFaceMap = GetColorToFaceMap();
        //    var currentWhiteFace = colorToFaceMap[RubiksColor.White]; // Should be UP
        //    if (colorToFaceMap[RubiksColor.White] != RubiksFace.UP) {
        //        var moves = currentWhiteFace switch {
        //            RubiksFace.RIGHT => Move.Parse("Z'"),
        //            RubiksFace.LEFT => Move.Parse("Z"),
        //            RubiksFace.DOWN => Move.Parse("Z2")
        //        };
        //        foreach (var move in moves)
        //            yield return move;
        //    }

        //}

        //private Dictionary<RubiksColor, RubiksFace> GetColorToFaceMap() {
        //    return new Dictionary<RubiksColor, RubiksFace>() {
        //            { this.Cube.Data[(long)Cube.GetFaceIndex(RubiksFace.FRONT) + RubiksCube.MiddlePiece], RubiksFace.FRONT },
        //            { this.Cube.Data[(long)Cube.GetFaceIndex(RubiksFace.RIGHT) + RubiksCube.MiddlePiece], RubiksFace.RIGHT },
        //            { this.Cube.Data[(long)Cube.GetFaceIndex(RubiksFace.BACK)  + RubiksCube.MiddlePiece],  RubiksFace.BACK },
        //            { this.Cube.Data[(long)Cube.GetFaceIndex(RubiksFace.LEFT)  + RubiksCube.MiddlePiece],  RubiksFace.LEFT },
        //            { this.Cube.Data[(long)Cube.GetFaceIndex(RubiksFace.UP)    + RubiksCube.MiddlePiece], RubiksFace.UP },
        //            { this.Cube.Data[(long)Cube.GetFaceIndex(RubiksFace.DOWN)  + RubiksCube.MiddlePiece], RubiksFace.DOWN }
        //        };
        //}

        //private string GetSetupMove(RubiksFace desiredFace) => desiredFace switch {
        //    RubiksFace.FRONT => "",
        //    RubiksFace.LEFT => "y",
        //    RubiksFace.RIGHT => "y'",
        //    RubiksFace.BACK => "y2"
        //};

        //private string MoveDToFace(RubiksFace currentFace, RubiksFace desiredFace) {
        //    var numDTurns = (byte)desiredFace - (byte)currentFace;
        //    var move = (numDTurns > 0) ? ("D" + numDTurns) : ("D" + (-numDTurns) + "'");
        //    return move;
        //}

        //private IEnumerable<RubiksMove> SolveWhiteCross() {

        //    //           ╔══╦══╦══╗                  
        //    //           ║45│X │47║                  
        //    //           ║──┼──┼──║                  
        //    //           ║X │49│X ║                  
        //    //           ║──┼──┼──║                  
        //    //           ║51│X │53║                  
        //    //  ╔══╦══╦══╬══╬══╬══╬══╦══╦══╦══╦══╦══╗
        //    //  ║27│X │29║0 │X │2 ║9 │X │11║18│X │20║  //X = Piece to solve
        //    //  ║──┼──┼──╬──┼──┼──╬──┼──┼──╬──┼──┼──║
        //    //  ║30│31│32║3 │4 |5 ║12│13│14║21│22│23║
        //    //  ║──┼──┼──╬──┼──┼──╬──┼──┼──╬──┼──┼──║
        //    //  ║33│34│35║6 │7 │8 ║15│16│17║24│25│26║
        //    //  ╚══╩══╩══╬══╬══╬══╬══╩══╩══╩══╩══╩══╝
        //    //           ║36│39│38║                  
        //    //           ║──┼──┼──║                  
        //    //           ║39│40│41║                  
        //    //           ║──┼──┼──║                  
        //    //           ║42│43│44║                  
        //    //           ╚══╩══╩══╝
        //    //           
        //    RubiksColor[] edgeColors = { RubiksColor.Green, RubiksColor.Orange, RubiksColor.Blue, RubiksColor.Red };


        //    foreach (var move in RotateToDefaultOrientation()) {
        //        yield return move;
        //    }

        //    var colorToFaceMap = GetColorToFaceMap();
        //    long[] edgesToSolve = {
        //        Cube.GetFaceIndex(colorToFaceMap[RubiksColor.White]) + RubiksCube.BottomEdge,
        //        Cube.GetFaceIndex(colorToFaceMap[RubiksColor.Green]) + RubiksCube.TopEdge,

        //        Cube.GetFaceIndex(colorToFaceMap[RubiksColor.White]) + RubiksCube.LeftEdge,
        //        Cube.GetFaceIndex(colorToFaceMap[RubiksColor.Orange]) + RubiksCube.TopEdge,

        //        Cube.GetFaceIndex(colorToFaceMap[RubiksColor.White]) + RubiksCube.TopEdge,
        //        Cube.GetFaceIndex(colorToFaceMap[RubiksColor.Blue]) + RubiksCube.TopEdge,

        //        Cube.GetFaceIndex(colorToFaceMap[RubiksColor.White]) + RubiksCube.RightEdge,
        //        Cube.GetFaceIndex(colorToFaceMap[RubiksColor.Red]) + RubiksCube.TopEdge
        //    };
        //    string solvedState = new string('W' + string.Join("W", edgeColors.Select(x => (char)x))); //"WGWOWBWR"

        //    // Sometimes multiple iterations are needed
        //    while (solvedState != new string(Enumerable.Range(0, edgesToSolve.Length).Select(i => (char)Cube.Data[edgesToSolve[i]]).ToArray())) {



        //        colorToFaceMap = GetColorToFaceMap();

        //        IEnumerable<RubiksMove> MovePieceToBottom(int index) {
        //            var currentFace = GetFaceOn(index);
        //            var edgeType = index % (3 * 3);

        //            if (currentFace == RubiksFace.DOWN) {
        //                yield break;
        //            }

        //            IEnumerable<RubiksMove> moves;

        //            if (currentFace == RubiksFace.UP) {
        //                moves = edgeType switch {
        //                    RubiksCube.TopEdge => Move.Parse("B2"),
        //                    RubiksCube.LeftEdge => Move.Parse("L2"),
        //                    RubiksCube.RightEdge => Move.Parse("R2"),
        //                    RubiksCube.BottomEdge => Move.Parse("F2")
        //                };
        //            } else {
        //                string setupMove = GetSetupMove(currentFace);

        //                var edgeSolve = edgeType switch {
        //                    RubiksCube.TopEdge => "F R' D R",
        //                    RubiksCube.LeftEdge => "L D L'",
        //                    RubiksCube.RightEdge => "R' D R",
        //                    RubiksCube.BottomEdge => "F L D L' F'"
        //                };

        //                moves = Move.ParseWithSetupMove(setupMove, edgeSolve);
        //            }

        //            foreach (var move in moves)
        //                yield return move;
        //        }

        //        for (int i = 0; i < edgesToSolve.Length; i += 2) {
        //            var desiredIndexWhite = edgesToSolve[i];
        //            var desiredIndexOther = edgesToSolve[i + 1];

        //            (var actualIndexWhite, var actualIndexOther) = FindEdgePiece(RubiksColor.White, edgeColors[i / 2]);

        //            if (actualIndexWhite == desiredIndexWhite) continue;

        //            // Move white side to bottom

        //            foreach (var move in MovePieceToBottom(actualIndexWhite)) {
        //                yield return move;
        //            }

        //            (actualIndexWhite, actualIndexOther) = FindEdgePiece(RubiksColor.White, edgeColors[i / 2]);

        //            var desiredFace = colorToFaceMap[edgeColors[i / 2]];
        //            var numDTurns = (byte)desiredFace - (byte)GetFaceOn(actualIndexOther);

        //            foreach (var move in (numDTurns > 0) ? Move.Parse("D" + numDTurns) : Move.Parse("D" + (-numDTurns) + "'"))
        //                yield return move;

        //            string setupMove = GetSetupMove(desiredFace);

        //            foreach (var move in Move.ParseWithSetupMove(setupMove, "F2"))
        //                yield return move;
        //        }
        //    }

        //    if (solvedState != new string(Enumerable.Range(0, edgesToSolve.Length).Select(i => (char)Cube.Data[edgesToSolve[i]]).ToArray())) {
        //        throw new Exception();
        //    }


        //}

        //private IEnumerable<RubiksMove> SolveWhiteCorners() {

        //    foreach (var move in RotateToDefaultOrientation())
        //        yield return move;

        //    RubiksColor[][] cornerColors = new RubiksColor[][] {
        //        new RubiksColor[] { RubiksColor.Orange, RubiksColor.Blue },
        //        new RubiksColor[] { RubiksColor.Blue, RubiksColor.Red },
        //        new RubiksColor[] { RubiksColor.Red, RubiksColor.Green },
        //        new RubiksColor[] { RubiksColor.Green, RubiksColor.Orange }, };

        //    string solvedState = "W" + string.Join('W', cornerColors.Select(x => string.Join("", x.Select(y => (char)y)))); // "WOBWBRWRGWGO"

        //    long[][] cornersToSolve = new long[][] {
        //        new long[] { Cube.GetFaceIndex(RubiksFace.UP) + RubiksCube.TopLeftCorner, Cube.GetFaceIndex(RubiksFace.LEFT) + RubiksCube.TopLeftCorner, Cube.GetFaceIndex(RubiksFace.BACK) + RubiksCube.TopRightCorner },
        //        new long[] { Cube.GetFaceIndex(RubiksFace.UP) + RubiksCube.TopRightCorner, Cube.GetFaceIndex(RubiksFace.BACK) + RubiksCube.TopLeftCorner, Cube.GetFaceIndex(RubiksFace.RIGHT) + RubiksCube.TopRightCorner },
        //        new long[] { Cube.GetFaceIndex(RubiksFace.UP) + RubiksCube.BottomRightCorner, Cube.GetFaceIndex(RubiksFace.RIGHT) + RubiksCube.TopLeftCorner, Cube.GetFaceIndex(RubiksFace.FRONT) + RubiksCube.TopRightCorner },
        //        new long[] { Cube.GetFaceIndex(RubiksFace.UP) + RubiksCube.BottomLeftCorner, Cube.GetFaceIndex(RubiksFace.FRONT) + RubiksCube.TopLeftCorner, Cube.GetFaceIndex(RubiksFace.LEFT) + RubiksCube.TopRightCorner },
        //    };

        //    if (solvedState != string.Join("", cornersToSolve.Select(x => x.Select(y => string.Join("", (char)this.Cube.Data[y]))))) {

        //        for (int i = 0; i < cornerColors.Length; i++) {
        //            System.Diagnostics.Debug.WriteLine("Solving edge " + i);
        //            (int White, int Color1, int Color2) edge = FindCornerPiece(RubiksColor.White, cornerColors[i][0], cornerColors[i][1]);
        //            System.Diagnostics.Debug.WriteLine($"({edge.White} / {edge.Color1} / {edge.Color2})");


        //            if (cornersToSolve[i][0] == edge.White && cornersToSolve[i][1] == edge.Color1 && cornersToSolve[i][2] == edge.Color2) {
        //                continue;
        //            }

        //            var desiredColor1Face = GetFaceOn((int)cornersToSolve[i][1]);
        //            var desiredColor2Face = GetFaceOn((int)cornersToSolve[i][2]);

        //            // edge auf front/back/left/right cornerpice = cornerBottomLeft || cornerBottomRight moven mit white facing front/back/left/right
        //            var whiteFaceOn = GetFaceOn(edge.White);
        //            var color1FaceOn = GetFaceOn(edge.Color1);
        //            var color2FaceOn = GetFaceOn(edge.Color2);
        //            var cornerType = edge.White % (3 * 3);
        //            string setupMove = "";

        //            switch (whiteFaceOn) {
        //                case RubiksFace.UP:
        //                    setupMove = cornerType switch { // Setup so that cornerType would be bottomCornerLeft
        //                        RubiksCube.TopLeftCorner => GetSetupMove(RubiksFace.LEFT),
        //                        RubiksCube.TopRightCorner => GetSetupMove(RubiksFace.BACK),
        //                        RubiksCube.BottomLeftCorner => "",
        //                        RubiksCube.BottomRightCorner => GetSetupMove(RubiksFace.RIGHT)
        //                    };
        //                    foreach (var move in Move.ParseWithSetupMove(setupMove, "F' D' F"))
        //                        yield return move;

        //                    break;

        //                case RubiksFace.DOWN:

        //                    // move piece to correct position on down side to turn it

        //                    //color2faceon sollte desiredColorface1 sein
        //                    foreach (var m in Move.Parse(MoveDToFace(color2FaceOn, desiredColor1Face)))
        //                        yield return m;

        //                    foreach (var m in Move.Parse("Z2"))
        //                        yield return m;

        //                    edge = FindCornerPiece(RubiksColor.White, cornerColors[i][0], cornerColors[i][1]);
        //                    cornerType = edge.White % (3 * 3); // White side is up now

        //                    setupMove = cornerType switch { // Setup so that cornerType would be bottomCornerLeft
        //                        RubiksCube.TopLeftCorner => GetSetupMove(RubiksFace.LEFT),
        //                        RubiksCube.TopRightCorner => GetSetupMove(RubiksFace.BACK),
        //                        RubiksCube.BottomLeftCorner => "",
        //                        RubiksCube.BottomRightCorner => GetSetupMove(RubiksFace.RIGHT)
        //                    };

        //                    foreach (var move in Move.ParseWithSetupMove(setupMove, "F U' F'")) {
        //                        yield return move;
        //                    }

        //                    foreach (var m in Move.Parse("Z2"))
        //                        yield return m;


        //                    break;

        //                case RubiksFace.FRONT:
        //                case RubiksFace.BACK:
        //                case RubiksFace.LEFT:
        //                case RubiksFace.RIGHT:

        //                    if (!(cornerType == RubiksCube.TopLeftCorner || cornerType == RubiksCube.TopRightCorner)) break;

        //                    setupMove = GetSetupMove(whiteFaceOn);

        //                    setupMove += cornerType switch {
        //                        RubiksCube.TopLeftCorner => "",
        //                        RubiksCube.TopRightCorner => " " + GetSetupMove(RubiksFace.RIGHT)
        //                    };

        //                    foreach (var move in Move.ParseWithSetupMove(setupMove, "L D L'"))
        //                        yield return move;

        //                    break;
        //            }
        //            System.Diagnostics.Debug.WriteLine("Moved piece to yellow side");

        //            // Corner piece is now on the yellow side facing front/left/right/back 
        //            //TODO: Insert piece into correct corner

        //            //find the other color which doesnt face down
        //            edge = FindCornerPiece(RubiksColor.White, cornerColors[i][0], cornerColors[i][1]);
        //            cornerType = edge.White % (3 * 3);
        //            color1FaceOn = GetFaceOn(edge.Color1);

        //            var colorToMove = color1FaceOn == RubiksFace.DOWN ? edge.Color2 : edge.Color1;
        //            var colorFaceOn = GetFaceOn(colorToMove);
        //            var desiredFaceOn = color1FaceOn == RubiksFace.DOWN ? desiredColor2Face : desiredColor1Face;

        //            foreach (var move in Move.Parse(MoveDToFace(colorFaceOn, desiredFaceOn)))
        //                yield return move;

        //            var moveUp = cornerType == RubiksCube.BottomLeftCorner ? "L' F L F'" : "F L' F' L";

        //            foreach (var move in Move.ParseWithSetupMove(GetSetupMove(desiredFaceOn), moveUp))
        //                yield return move;
        //        }
        //    }

        //}


        public override IEnumerable<RubiksMove> Solve() {

            throw new NotImplementedException();

            //foreach (var move in SolveWhiteCross()) {
            //    this.Cube.Move(move);
            //    yield return move;
            //}

            //System.Diagnostics.Debug.WriteLine("White cross solved");

            //foreach (var move in SolveWhiteCorners()) {
            //    System.Diagnostics.Debug.WriteLine(move);
            //    this.Cube.Move(move);
            //    yield return move;
            //}

            //System.Diagnostics.Debug.WriteLine("White corners solved");

        }
    }
}
