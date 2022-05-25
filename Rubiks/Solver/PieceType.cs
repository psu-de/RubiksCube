using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubiks.Solver {
    public enum PieceType : byte {

        TopLeftCorner = 0,
        TopRightCorner = 2,
        BottomLeftCorner = 6,
        BottomRightCorner = 8,

        TopEdge = 1,
        LeftEdge = 3,
        RightEdge = 5,
        BottomEdge = 7,

        MiddlePiece = 4,
    }
}
