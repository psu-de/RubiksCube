using Rubiks.Moves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubiks.Solver {
    public abstract class Solver {

        public RubiksCube Cube { get; protected set; }

        public Solver(RubiksCube cube) {
            this.Cube = cube;
        }

        public abstract IEnumerable<RubiksMove> Solve();


        #region Helper Methods

        public (int X, int Y, int Z)[] Corners = {
                ( 0, 29, 51 ),
                ( 2, 53, 9  ),
                ( 6, 35, 36 ),
                ( 8, 15, 38 ),
                ( 18, 11, 47 ),
                ( 20, 45, 27 ),
                ( 24, 17, 44 ),
                ( 26, 42, 33)
        };

        public (int X, int Y, int Z) GetCorner(int index) {

            (int X, int Y, int Z)? corner = Corners.FirstOrDefault(x => x.X == index || x.Y == index || x.Z == index);
            
            if (!corner.HasValue) throw new ArgumentException("Corner not found");


            return corner.Value;
        }

        public (int X, int Y)[] Edges = {
            (1, 52),
            (3, 32),
            (5, 12),
            (7, 37),
            (10, 50),
            (14, 21),
            (16, 41),
            (19, 46),
            (23, 30),
            (25, 43),
            (28, 48),
            (34, 39),
        };

        public (int X, int Y) GetEdge(int index) {
            return Edges.Where(x => x.X == index || x.Y == index).First();

            #endregion
        }

        public (int X, int Y) GetDesiredEdge((RubiksColor X, RubiksColor Y) cornerColors) {

            var faceX = GetFaceOfColor(cornerColors.X);
            var faceY = GetFaceOfColor(cornerColors.Y);

            var edgeTypes = new int[] { (int)PieceType.TopEdge, (int)PieceType.RightEdge, (int)PieceType.LeftEdge, (int)PieceType.BottomEdge };

            var possibleX = edgeTypes.Select(x => GetFaceIndex(faceX) + x);
            var possibleY = edgeTypes.Select(x => GetFaceIndex(faceY) + x);

            // Find corner sharing one index in all faces

            foreach (var edge in this.Edges) {
                var edgeList = new List<int>() { edge.X, edge.Y };

                var indiecesX = possibleX.Select(x => edgeList.IndexOf(x)).Where(x => x != -1).ToList();
                if (indiecesX.Count == 0) continue;
                var indiecesY = possibleY.Select(x => edgeList.IndexOf(x)).Where(x => x != -1).ToList();
                if (indiecesY.Count == 0) continue;

                var indexX = indiecesX.First();
                var indexY = indiecesY.First();

                return (edgeList[indexX], edgeList[indexY]);
            }
            throw new ArgumentException("Nothing found!");

        }

        public (RubiksColor X, RubiksColor Y) GetEdgeColors((int X, int Y) edge) => (this.Cube.Data[(long)edge.X], this.Cube.Data[(long)edge.Y]);

        public RubiksFace GetFaceOn(int index) => (RubiksFace)((index / (3 * 3)) + 1);

        public int GetFaceIndex(RubiksFace face) => (((int)face) - 1) * 3 * 3;

        public RubiksFace GetFaceOfColor(RubiksColor color) {
            foreach (var face in Enum.GetValues<RubiksFace>()) {
                var middlePiece = GetFaceIndex(face) + (byte)PieceType.MiddlePiece;
                if (this.Cube.Data[(long)middlePiece] == color)
                    return face;
            }
            throw new NotSupportedException();
        }

        public RubiksColor GetColorOfFace(RubiksFace face) => this.Cube.Data[GetFaceIndex(face) + (long)PieceType.MiddlePiece];

        public PieceType GetPieceType(int index) => (PieceType)(index % (3 * 3));

        public (RubiksColor X, RubiksColor Y, RubiksColor Z) GetCornerColors((int X, int Y, int Z) corner) {
            return (this.Cube.Data[(long)corner.X], this.Cube.Data[(long)corner.Y], this.Cube.Data[(long)corner.Z]);
        }

        public (int X, int Y, int Z) GetDesiredCorner((RubiksColor X, RubiksColor Y, RubiksColor Z) cornerColors) {

            var faceX = GetFaceOfColor(cornerColors.X);
            var faceY = GetFaceOfColor(cornerColors.Y);
            var faceZ = GetFaceOfColor(cornerColors.Z);

            var cornerTypes = new int[] { (int)PieceType.TopLeftCorner, (int)PieceType.TopRightCorner, (int)PieceType.BottomLeftCorner, (int)PieceType.BottomRightCorner };

            var possibleX = cornerTypes.Select(x => GetFaceIndex(faceX) + x);
            var possibleY = cornerTypes.Select(x => GetFaceIndex(faceY) + x);
            var possibleZ = cornerTypes.Select(x => GetFaceIndex(faceZ) + x);

            // Find corner sharing one index in all faces
            
            foreach (var corner in this.Corners) {
                var cornerList = new List<int>() { corner.X, corner.Y, corner.Z };

                var indiecesX = possibleX.Select(x => cornerList.IndexOf(x)).Where(x => x != -1).ToList();
                if (indiecesX.Count == 0) continue;
                var indiecesY = possibleY.Select(x => cornerList.IndexOf(x)).Where(x => x != -1).ToList(); 
                if (indiecesY.Count == 0) continue;
                var indiecesZ = possibleZ.Select(x => cornerList.IndexOf(x)).Where(x => x != -1).ToList();
                if (indiecesZ.Count == 0) continue;

                var indexX = indiecesX.First();
                var indexY = indiecesY.First();
                var indexZ = indiecesZ.First();

                return (cornerList[indexX], cornerList[indexY], cornerList[indexZ]);
            }
            throw new ArgumentException("Nothing found!");

        }


        public const RubiksColor DefaultFrontColor = RubiksColor.Green;
        public const RubiksColor DefaultUpColor = RubiksColor.White;

        public IEnumerable<RubiksMove> RotateToFace(RubiksFace faceFront, RubiksFace faceTop) {

            return RotateToColor(GetColorOfFace(faceFront), GetColorOfFace(faceTop));

        }

        public IEnumerable<RubiksMove> RotateToColor(RubiksColor colorFront, RubiksColor colorTop) {

            RubiksFace faceFront = GetFaceOfColor(colorFront);


            switch (faceFront) {
                case RubiksFace.UP:
                    yield return RubiksMove._X;
                    break;
                case RubiksFace.DOWN:
                    yield return RubiksMove.X;
                    break;
                case RubiksFace.LEFT:
                    yield return RubiksMove._Y;
                    break;
                case RubiksFace.RIGHT:
                    yield return RubiksMove.Y;
                    break;
                case RubiksFace.BACK:
                    yield return RubiksMove.Y;
                    yield return RubiksMove.Y;
                    break;
            }

            RubiksFace faceTop = GetFaceOfColor(colorTop);
            switch (faceTop) {

                case RubiksFace.DOWN:
                    yield return RubiksMove.Z;
                    yield return RubiksMove.Z;
                    break;
                case RubiksFace.LEFT:
                    yield return RubiksMove.Z;
                    break;
                case RubiksFace.RIGHT:
                    yield return RubiksMove._Z;
                    break;
                case RubiksFace.BACK:
                    throw new ArgumentException($"Top Face {faceTop} not possible with front face {faceFront}");
                    break;
            }
        }

        public IEnumerable<RubiksMove> RotateToDefaultOrientation() => RotateToColor(DefaultFrontColor, DefaultUpColor);
    }
}
