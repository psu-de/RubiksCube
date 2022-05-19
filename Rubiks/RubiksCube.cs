using TensorCS.Core;

namespace Rubiks {
    public class RubiksCube {

        public const int RUBIK_FACES = 6;

        public IntTensor Cube { get; private set; }

        public RubiksCube() {
            this.Cube = new IntTensor(new Shape(6, 3, 3), 0);
            this.SetCubeFaces();
        }


        private void SetCubeFaces() {
            for (int f = 0; f < RUBIK_FACES; f++) {
                for (int x = 0; x < 3; x++) {
                    for (int y = 0; y < 3; y++) {
                        this.Cube[f, x, y] = f + 1;
                    }
                }
            }
        }

        public int GetFaceIndex(RubiksFace face) {
            return ((int)face - 1) * 3 * 3;
        }

        public int[] GetFace(RubiksFace face) {
            List<int> faceValues = new List<int>();
            for (int x = 0; x < 3; x++) {
                for (int y = 0; y < 3; y++) {
                    faceValues.Add(this.Cube[(int)face - 1, x, y]);
                }
            }
            return faceValues.ToArray();
        }

        private void RotateAxis90(int axis, bool vertical) {
            Dictionary<int, int> swapIndices = new Dictionary<int, int>();

            //TODO: Faces rotieren
            if (!vertical) {
                for (int i = 0; i < 3; i++) {
                    swapIndices.Add(GetFaceIndex(RubiksFace.FRONT) + (axis * 3) + i, GetFaceIndex(RubiksFace.RIGHT) + (axis * 3) + i);
                    swapIndices.Add(GetFaceIndex(RubiksFace.LEFT) + (axis * 3) + i, GetFaceIndex(RubiksFace.FRONT) + (axis * 3) + i);
                    swapIndices.Add(GetFaceIndex(RubiksFace.BACK) + (axis * 3) + i, GetFaceIndex(RubiksFace.LEFT) + (axis * 3) + i);
                    swapIndices.Add(GetFaceIndex(RubiksFace.RIGHT) + (axis * 3) + i, GetFaceIndex(RubiksFace.BACK) + (axis * 3) + i);
                }
            } else {
                for (int i = 0; i < 3; i++) {
                    swapIndices.Add(GetFaceIndex(RubiksFace.FRONT) + axis + (i * 3), GetFaceIndex(RubiksFace.DOWN) + axis + (i * 3));
                    swapIndices.Add(GetFaceIndex(RubiksFace.UP) + axis + (i * 3), GetFaceIndex(RubiksFace.FRONT) + axis + (i * 3));
                    swapIndices.Add(GetFaceIndex(RubiksFace.BACK) + axis + (i * 3), GetFaceIndex(RubiksFace.UP) + axis + (i * 3));
                    swapIndices.Add(GetFaceIndex(RubiksFace.DOWN) + axis + (i * 3), GetFaceIndex(RubiksFace.BACK) + axis + (i * 3));
                }
            }

            Dictionary<int, int> cache = new Dictionary<int, int>();
            foreach (var kvp in swapIndices) {
                if (!cache.ContainsKey(kvp.Key)) {
                    cache.Add(kvp.Key, Cube.GetValue((long)kvp.Key));
                }
                if (!cache.ContainsKey(kvp.Value)) {
                    cache.Add(kvp.Value, Cube.GetValue((long)kvp.Value));
                }

                Cube.SetValue(cache[kvp.Value], (long)kvp.Key);
            }
        }

        public void MoveU() => RotateAxis90(0, false);
        public void MoveD() => RotateAxis90(2, false);


        public void MoveR() => RotateAxis90(2, true);

        public void Move(RubiksMove move) {
            switch (move) {
                case RubiksMove.U: MoveU(); break;
            }
        }

        public bool IsSolved() {
            bool solved = true;

            for (int f = 0; f < RUBIK_FACES; f++) {
                var face = (RubiksFace)f + 1;
                var values = GetFace(face);
                if (values.Any(x => x != values[0])) {
                    solved = false;
                    break;
                }
            }

            return solved;
        }
    }
}