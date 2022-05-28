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
