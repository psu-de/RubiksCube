using Rubiks;
using Spectre.Console;

var cube = new RubiksCube();

Console.WriteLine(cube.IsSolved());

DisplayRubiks(cube);
cube.MoveD();
DisplayRubiks(cube);
cube.MoveR();
DisplayRubiks(cube);
Console.ReadLine();



void DisplayRubiks(RubiksCube cube) {

    Canvas canvas = new Canvas(3 * 3 + 1, 4 * 3 + 1);

    Spectre.Console.Color GetColor(int val) {
        switch (val) {
            case 1: return Color.White;
            case 2: return Color.Red;
            case 3: return Color.Yellow1;
            case 4: return Color.Orange1;
            case 5: return Color.Blue;
            case 6: return Color.Green;

            default: throw new NotSupportedException();
        }
    }

    void SetFacePixels(RubiksFace face, int offsetX, int offsetY) {
        var values = cube.GetFace(face);
        for (int py = 0; py < 3; py++) {
            for (int px = 0; px < 3; px++) {
                canvas.SetPixel(offsetX + px, offsetY + py, GetColor(values[py * 3 + px]));
            }
        }
    }

    SetFacePixels(RubiksFace.UP, 3, 0);
    SetFacePixels(RubiksFace.LEFT, 0, 3);
    SetFacePixels(RubiksFace.FRONT, 3, 3);
    SetFacePixels(RubiksFace.RIGHT, 6, 3);
    SetFacePixels(RubiksFace.DOWN, 3, 6);
    SetFacePixels(RubiksFace.BACK, 3, 9);

    AnsiConsole.Write(canvas);
}