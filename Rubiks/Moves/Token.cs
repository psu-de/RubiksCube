using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubiks.Moves {
    internal class Token {

        public static Token Empty = new Token(0, new RubiksMove[0]);
        public int Count { get; set; }
        private RubiksMove[] Moves { get; set; }

        private Token(int count, RubiksMove[] moves) {
            Moves = moves;
            Count = count;
        }

        public RubiksMove[] GetMoves() {
            var l = new List<RubiksMove>();
            for (int i = 0; i < Count; i++)
                l.AddRange(Moves);
            return l.ToArray();
        }

        public override string ToString() {
            return $"Token(Moves='{string.Join(", ", Moves)}', Count={Count})";
        }


        public static Token Parse(string input) {
            int count = 1;

            if (string.IsNullOrEmpty(input))
                return Token.Empty;

            if (input.StartsWith('(')) {
                
                input = input.Substring(1, input.Length - 1);
                int indexClosingBracket = -1;
                // figure out count
                int bracketCount = 0;
                bool firstNumberParse = true;
                for (int i = 0; i < input.Length; i++) {
                    switch (input[i]) {
                        case '(': bracketCount++; break;
                        case ')': 
                            bracketCount--; 
                            if (bracketCount == -1) indexClosingBracket = i;
                            break;
                    }

                    if (bracketCount < -1) throw new Exception();

                    if (indexClosingBracket > 0 && input[i] != ')') {
                        //token finished

                        if (!char.IsDigit(input[i])) {
                            throw new Exception($"Expected number, got '{input[i]}'. Token={input}");
                        }
                        if (firstNumberParse) {
                            firstNumberParse = false;
                            count = 0;
                        }

                        var l = input[i] - '0';
                        count *= 10;
                        count += l;
                    }
                }

                if (indexClosingBracket == -1) {
                    throw new Exception($"Expected closing bracket. Token='{input}'");
                }

                string innerSequence = input.Substring(0, indexClosingBracket);
                var innerMoves = Move.Parse(innerSequence);     

                return new Token(count, innerMoves);
            }
            else {
                bool invert = false;

                if (input.Last() == '\'') {
                    // Invert move
                    invert = true;
                    input = input.Substring(0, input.Length - 1);
                }

                var idx = input.LastIndexOfAny(new[] { 'U', 'D', 'R', 'L', 'F', 'B', 'M', 'E', 'S', 'w', 'x', 'y', 'z', 'u', 'd', 'r', 'l', 'f', 'b', 'Y', 'X', 'Z' }) + 1;
                var moveStr = input.Substring(0, idx);

                RubiksMove move = moveStr switch {
                    "U" => RubiksMove.U,
                    "D" => RubiksMove.D,
                    "R" => RubiksMove.R,
                    "L" => RubiksMove.L,
                    "F" => RubiksMove.F,
                    "B" => RubiksMove.B,
                    "M" => RubiksMove.M,
                    "E" => RubiksMove.E,
                    "S" => RubiksMove.S,

                    "u" => RubiksMove.Uw,
                    "d" => RubiksMove.Dw,
                    "r" => RubiksMove.Rw,
                    "l" => RubiksMove.Lw,
                    "f" => RubiksMove.Fw,
                    "b" => RubiksMove.Bw,

                    "Rw" => RubiksMove.Rw,
                    "Lw" => RubiksMove.Lw,
                    "Uw" => RubiksMove.Uw,
                    "Dw" => RubiksMove.Dw,
                    "Fw" => RubiksMove.Fw,
                    "Bw" => RubiksMove.Bw,

                    "X" => RubiksMove.X,
                    "Y" => RubiksMove.Y,
                    "Z" => RubiksMove.Z,

                    "x" => RubiksMove._X,
                    "y" => RubiksMove._Y,
                    "z" => RubiksMove._Z,
                    _ => throw new NotSupportedException($"Invalid token: '{input}' (inverted={invert})")
                };

                string rest = input.Substring(idx);
                if (!string.IsNullOrEmpty(rest)) {
                    count = int.Parse(rest);
                }

                if (rest.Any(c => !char.IsDigit(c))) {
                    throw new NotSupportedException($"Expected number, got: '{rest}'. Token='{input}'");
                }

                if (invert) move = Move.Invert(move);

                return new Token(count, new[] { move });
            }
        }

    }
}
