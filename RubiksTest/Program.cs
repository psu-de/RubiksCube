using Rubiks;
using Spectre.Console;


var cube = new RubiksCube();
Canvas display = new Canvas(3 * 3 + 1, 4 * 3 + 1);

AnsiConsole.MarkupLine("Informatik Projekt");
AnsiConsole.MarkupLine("Mögliche moves: " + String.Join(", ", Enum.GetNames(typeof(RubiksMove)).Select(x => !x.Contains('_') ? x : x.Replace("_", "") + "'")));
AnsiConsole.MarkupLine("Bei jedem Input können beliebig viele Moves angegeben werden (durch Leerzeichen getrennt) oder: ");
AnsiConsole.MarkupLine(" - [purple]scramble[/] um den Zauberwürfel zufällig zu verdrehen");
AnsiConsole.MarkupLine(" - [red]exit[/] um das Programm zu verlassen");
AnsiConsole.WriteLine();
AnsiConsole.WriteLine();
AnsiConsole.WriteLine();

DisplayRubiks(cube);
AnsiConsole.Write(display);

while (true) {

    var seq = AnsiConsole.Ask<string>("Move Sequenz: ");

    if (seq == "exit") {
        Environment.Exit(0);
    }


    AnsiConsole.Live(display).Start(ctx => {

        if (seq == "scramble") {
            foreach (var move in cube.Scramble()) {
                DisplayRubiks(cube);
                ctx.Refresh();
                Task.Delay(350).Wait();
            }
        } else {
            foreach (var move in cube.Move(seq)) {
                DisplayRubiks(cube);
                ctx.Refresh();
                Task.Delay(350).Wait();
            }
        }

    });

    AnsiConsole.WriteLine("Gelöst: " + cube.IsSolved());

}


Console.ReadLine();


void DisplayRubiks(RubiksCube cube) {

    Spectre.Console.Color GetColor(int val) {
        switch (val) {
            case 1: return Color.White;
            case 2: return Color.Red;
            case 3: return Color.Yellow1;
            case 4: return Color.Orange1;
            case 5: return Color.Green;
            case 6: return Color.Blue;
            case 7: return Color.Magenta1;

            default: throw new NotSupportedException();
        }
    }

    void SetFacePixels(RubiksFace face, int offsetX, int offsetY) {
        var values = cube.GetFace(face);
        for (int py = 0; py < 3; py++) {
            for (int px = 0; px < 3; px++) {
                display.SetPixel(offsetX + px, offsetY + py, GetColor(values[py * 3 + px]));
            }
        }
    }

    SetFacePixels(RubiksFace.UP, 3, 0);
    SetFacePixels(RubiksFace.LEFT, 0, 3);
    SetFacePixels(RubiksFace.FRONT, 3, 3);
    SetFacePixels(RubiksFace.RIGHT, 6, 3);
    SetFacePixels(RubiksFace.DOWN, 3, 6);
    SetFacePixels(RubiksFace.BACK, 3, 9);
}