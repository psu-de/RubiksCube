using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TensorCS.Core;

namespace Rubiks.Moves {
    public class Move {

        private static Move U = new Move(new int[] { 9, 10, 11, 3, 4, 5, 6, 7, 8, 18, 19, 20, 12, 13, 14, 15, 16, 17, 27, 28, 29, 21, 22, 23, 24, 25, 26, 0, 1, 2, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 51, 48, 45, 52, 49, 46, 53, 50, 47 });
        private static Move X = new Move(new int[] { 36, 37, 38, 39, 40, 41, 42, 43, 44, 15, 12, 9, 16, 13, 10, 17, 14, 11, 53, 52, 51, 50, 49, 48, 47, 46, 45, 29, 32, 35, 28, 31, 34, 27, 30, 33, 26, 25, 24, 23, 22, 21, 20, 19, 18, 0, 1, 2, 3, 4, 5, 6, 7, 8 });
        private static Move Y = new Move(new int[] { 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 0, 1, 2, 3, 4, 5, 6, 7, 8, 38, 41, 44, 37, 40, 43, 36, 39, 42, 51, 48, 45, 52, 49, 46, 53, 50, 47 });
        private static Move Z = new Move(new Move[] { X, Y, X, X, X });
        private static Move D = new Move(new Move[] { X, X, U, X, X });
        private static Move L = new Move(new Move[] { Z, U, Z, Z, Z });
        private static Move R = new Move(new Move[] { Y, Y, L, Y, Y });
        private static Move F = new Move(new Move[] { Y, Y, Y, R, Y });
        private static Move B = new Move(new Move[] { Y, Y, F, Y, Y });
        private static Move M = new Move(new Move[] { L, L, L, R, X, X, X });
        private static Move E = new Move(new Move[] { Z, M, Z, Z, Z });
        private static Move S = new Move(new Move[] { Y, M, Y, Y, Y });
        private static Move Fw = new Move(new Move[] { F, S });
        private static Move Bw = new Move(new Move[] { Z, Z, Z, F });
        private static Move Rw = new Move(new Move[] { M, M, M, R });
        private static Move Lw = new Move(new Move[] { M, L });
        private static Move Uw = new Move(new Move[] { D, Y });
        private static Move Dw = new Move(new Move[] { Y, Y, Y, U });

        // Alle moves umgekehrt
        private static Move _U = new Move(new Move[] { U, U, U });
        private static Move _X = new Move(new Move[] { X, X, X });
        private static Move _Y = new Move(new Move[] { Y, Y, Y });
        private static Move _Z = new Move(new Move[] { Z, Z, Z });
        private static Move _D = new Move(new Move[] { D, D, D });
        private static Move _L = new Move(new Move[] { L, L, L });
        private static Move _R = new Move(new Move[] { R, R, R });
        private static Move _F = new Move(new Move[] { F, F, F });
        private static Move _B = new Move(new Move[] { B, B, B });
        private static Move _M = new Move(new Move[] { M, M, M });
        private static Move _E = new Move(new Move[] { E, E, E });
        private static Move _S = new Move(new Move[] { S, S, S });
        private static Move _Fw = new Move(new Move[] { Fw, Fw, Fw });
        private static Move _Bw = new Move(new Move[] { Bw, Bw, Bw });
        private static Move _Rw = new Move(new Move[] { Rw, Rw, Rw });
        private static Move _Lw = new Move(new Move[] { Lw, Lw, Lw });
        private static Move _Uw = new Move(new Move[] { Uw, Uw, Uw });
        private static Move _Dw = new Move(new Move[] { Dw, Dw, Dw });

        /// <summary>
        /// Gibt den entsprechenden <see cref="Move"/> für ein <see cref="RubiksMove"/> zurück
        /// </summary>
        /// <param name="move"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException">Falls <paramref name="move"/> nicht implementiert ist</exception>
        public static Move GetMove(RubiksMove move) => move switch {
            RubiksMove.U => U,
            RubiksMove.D => D,
            RubiksMove.R => R,
            RubiksMove.L => L,
            RubiksMove.F => F,
            RubiksMove.B => B,
            RubiksMove.M => M,
            RubiksMove.E => E,
            RubiksMove.S => S,
            RubiksMove.X => X,
            RubiksMove.Y => Y,
            RubiksMove.Z => Z,
            RubiksMove.Rw => Rw,
            RubiksMove.Lw => Lw,
            RubiksMove.Uw => Uw,
            RubiksMove.Dw => Dw,
            RubiksMove.Fw => Fw,
            RubiksMove.Bw => Bw,
            RubiksMove._U => _U,
            RubiksMove._D => _D,
            RubiksMove._R => _R,
            RubiksMove._L => _L,
            RubiksMove._F => _F,
            RubiksMove._B => _B,
            RubiksMove._M => _M,
            RubiksMove._E => _E,
            RubiksMove._S => _S,
            RubiksMove._X => _X,
            RubiksMove._Y => _Y,
            RubiksMove._Z => _Z,
            RubiksMove._Rw => _Rw,
            RubiksMove._Lw => _Lw,
            RubiksMove._Uw => _Uw,
            RubiksMove._Dw => _Dw,
            RubiksMove._Fw => _Fw,
            RubiksMove._Bw => _Bw,
            _ => throw new NotImplementedException()
        };

        /// <summary>
        /// Gibt den invertierten <see cref="RubiksMove"/> zurück
        /// </summary>
        /// <param name="move"></param>
        /// <returns></returns>
        public static RubiksMove Invert(RubiksMove move) {
            int val = (int)move;

            if ((val & 0x20) == 0x20) return (RubiksMove)(val - 0x20);
            else return (RubiksMove)(val + 0x20);
        }

        /// <summary>
        /// Gibt einen string für <paramref name="move"/> zurück
        /// </summary>
        /// <param name="move"></param>
        /// <returns></returns>
        public static string ToString(RubiksMove move) {
            var val = (int)move;
            bool inverted = false;
            if ((val & 0x20) == 0x20) {
                move = Invert(move);
                inverted = true;
            }

            var c = move switch {
                RubiksMove.U => "U",
                RubiksMove.D => "D",
                RubiksMove.R => "R",
                RubiksMove.L => "L",
                RubiksMove.F => "F",
                RubiksMove.B => "B",
                RubiksMove.M => "M",
                RubiksMove.E => "E",
                RubiksMove.S => "S",

                RubiksMove.Uw => "Uw",
                RubiksMove.Dw => "Dw",
                RubiksMove.Rw => "Rw",
                RubiksMove.Lw => "Lw",
                RubiksMove.Fw => "Fw",
                RubiksMove.Bw => "Bw",

                RubiksMove.X => "X",
                RubiksMove.Y => "Y",
                RubiksMove.Z => "Z",

                RubiksMove._X => "x",
                RubiksMove._Y => "y",
                RubiksMove._Z => "z",
            };
            return inverted ? c + "'" : c;
        }

        /// <summary>
        /// Gibt eine Zugsequenz als String für <paramref name="moves"/> zurück
        /// </summary>
        /// <param name="moves"></param>
        /// <returns></returns>
        public static string ToReadableString(RubiksMove[] moves) {

            return String.Join(" ", moves.Select(x => ToString(x)));
            

        }

        private static Token ReadNextToken(ref string input) {
            string token = "";
            int bracketCount = 0;
            Stack<char> chars = new Stack<char>(input.Reverse());


            while (chars.Count > 0) {
                char c = chars.Pop();

                switch (c) {
                    case '(':
                        bracketCount++;
                        break;
                    case ')':
                        bracketCount--;
                        break;
                }

                if (bracketCount < 0) throw new Exception("Invalid move sequence!");

                token += c;
                if (c == ' ') {
                    if (bracketCount == 0) {
                        break;
                    }
                }

            }

            input = input.Substring(token.Length);
            return Token.Parse(token.Trim());
        }

        /// <summary>
        /// Parsed eine Zugsequenz mit einem <paramref name="setup"/> move
        /// </summary>
        /// <param name="setup">Eine Zugsequenz die vor der <paramref name="sequence"/> ausgeführt wird und danach wieder rückgängig gemacht wird</param>
        /// <param name="sequence">Zugsequenz zum ausführen</param>
        /// <returns>Zugsequenz als <see cref="RubiksMove[]"/> mit setup move</returns>
        public static RubiksMove[] ParseWithSetupMove(string setup, string sequence) {
            var setupMoves = Parse(setup);
            var undoSetupMoves = setupMoves.Select(x => Invert(x)).Reverse();

            var moves = Parse(sequence);
            return new List<RubiksMove>(setupMoves).Concat(moves).Concat(undoSetupMoves).ToArray();
        }

        /// <summary>
        /// Parsed eine Zugsequenz
        /// </summary>
        /// <param name="sequence"></param>
        /// <returns>Zugsequenz als <see cref="RubiksMove[]"/></returns>
        public static RubiksMove[] Parse(string sequence) {

            List<RubiksMove> moves = new List<RubiksMove>();

            while (sequence.Length > 0) {
                var token = ReadNextToken(ref sequence);
                moves.AddRange(token.GetMoves());                
            }

            return moves.ToArray();
        }

        private int[] Permutation;

        /// <summary>
        /// Erstellt einen neuen Move über ein permutation array
        /// </summary>
        /// <param name="permutation"></param>
        public Move(int[] permutation) {
            this.Permutation = permutation;
        }


        /// <summary>
        /// Erstellt einen neuen Move über andere Moves und berechnet die pemutation array
        /// </summary>
        /// <param name="otherMoves"></param>
        public Move(Move[] otherMoves) {
            // Jeden move als permutation speichern, ~12x schneller
            var shape = new Shape(6, RubiksCube.FaceLength, RubiksCube.FaceLength);
            BaseTensor<int> tensor = new IntTensor(shape, Enumerable.Range(0, (int)shape.Total).ToArray());

            foreach (var move in otherMoves) {
                var clone = tensor.Clone();
                for (long i = 0; i < move.Permutation.Length; i++) {
                    clone.SetValue(tensor[(long)move.Permutation[i]], i);
                }
                tensor = clone;
            }

            this.Permutation = tensor.GetValues();
        }


        /// <summary>
        /// Wendet den <see cref="Move"/> auf den <paramref name="cube"/> an
        /// </summary>
        /// <param name="cube"></param>
        public void Apply(RubiksCube cube) {

            var oldCube = cube.Data;
            var newCube = cube.Data.Clone();
            for (long i = 0; i < Permutation.Length; i++) {
                newCube.SetValue(oldCube[(long)this.Permutation[i]], i);
            }
            cube.Data = newCube;
        }
    }
}
