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

        public int[][] Corners = {
                new int[]{ 0, 29, 51 },
                new int[]{ 2, 53, 9  },
                new int[]{ 6, 35, 36 },
                new int[]{ 8, 15, 38 },
                new int[]{ 18, 11, 47 },
                new int[]{ 20, 45, 27 },
                new int[]{ 24, 17, 44 },
                new int[]{ 26, 42, 33 }
        };

        public (int X, int Y, int Z) GetCorner(int index) {

            var corner = Corners.FirstOrDefault(x => x.Contains(index));
            if (corner == null) throw new ArgumentException("Corner not found");


            return (corner[0], corner[1], corner[2]);
        }

        public (int X, int Y) GetEdge(int index) {
            return (index, index switch {
                1 => 52,
                3 => 32,
                5 => 12,
                7 => 37,
                10 => 50,
                12 => 5,
                14 => 21,
                16 => 41,
                19 => 46,
                21 => 14,
                23 => 30,
                25 => 43,
                28 => 48,
                30 => 23,
                32 => 3,
                34 => 39,
                37 => 7,
                39 => 34,
                41 => 16,
                43 => 25,
                46 => 19,
                48 => 28,
                50 => 10,
                52 => 1,
                _ => throw new ArgumentException()
            });

            #endregion
        }

        public RubiksFace GetFaceOn(int index) {
            return (RubiksFace)((index / (3 * 3)) + 1);
        }

        public int GetFaceIndex(RubiksFace face) {
            return (((int)face) - 1) * 3 * 3;
        }

        public RubiksFace GetFaceOfColor(RubiksColor color) {
            foreach (var face in Enum.GetValues<RubiksFace>()) {
                var middlePiece = GetFaceIndex(face) + (byte)PieceType.MiddlePiece;
                if (this.Cube.Data[(long)middlePiece] == color)
                    return face;
            }
            throw new NotSupportedException();
        }

        public PieceType GetPieceType(int index) {
            return (PieceType)(index % (3 * 3));
        }

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
                var cornerList = corner.ToList();

                var indiecesX = possibleX.Select(x => cornerList.IndexOf(x)).Where(x => x != -1).ToList();
                if (indiecesX.Count == 0) continue;
                var indiecesY = possibleY.Select(x => cornerList.IndexOf(x)).Where(x => x != -1).ToList(); 
                if (indiecesY.Count == 0) continue;
                var indiecesZ = possibleZ.Select(x => cornerList.IndexOf(x)).Where(x => x != -1).ToList();
                if (indiecesZ.Count == 0) continue;

                var indexX = indiecesX.First();
                var indexY = indiecesY.First();
                var indexZ = indiecesZ.First();

                return (corner[indexX], corner[indexY], corner[indexZ]);
            }
            throw new ArgumentException("Nothing found!");

        }
    }
}
