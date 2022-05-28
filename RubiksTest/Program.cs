using Rubiks;
using Rubiks.Moves;
using Rubiks.Solver;
using Spectre.Console;
using System.Diagnostics;

object _displayLock = new object();

var cube = new RubiksCube();
Canvas display = new Canvas(4 * 3 + 1, 3 * 3 + 1);

var sw = Stopwatch.StartNew();
for (int i = 0; i < 100; i++) {

    cube.Scramble().ToArray();
    var solver = new OldHoffmanSolver(cube);
    var moves = solver.Solve().ToArray();
    System.Diagnostics.Debug.Assert(cube.IsSolved());
    System.Diagnostics.Debug.WriteLine($"Cube solved in {moves.Length} moves");
}
sw.Stop();
System.Diagnostics.Debug.WriteLine($"100 Cubes solved in {sw.ElapsedMilliseconds}ms");

AnsiConsole.MarkupLine("Informatik Projekt");
AnsiConsole.MarkupLine("Mögliche moves: " + String.Join(", ", Enum.GetNames(typeof(RubiksMove)).Select(x => !x.Contains('_') ? x : x.Replace("_", "") + "'")));
AnsiConsole.MarkupLine("Bei jedem Input können beliebig viele Moves angegeben werden (durch Leerzeichen getrennt) oder: ");
AnsiConsole.MarkupLine(" - [purple]scramble[/] um den Zauberwürfel zufällig zu verdrehen");
AnsiConsole.MarkupLine(" - [red]exit[/] um das Programm zu verlassen");
AnsiConsole.MarkupLine(" - [cyan1]reset[/] um den Würfel zu resetten.");
AnsiConsole.MarkupLine(" - [green]solve[/] um den Würfel automatisch zu lösen (Old Hoffmann Methode)");
AnsiConsole.WriteLine();
AnsiConsole.WriteLine();
AnsiConsole.WriteLine();

DisplayRubiks(cube);
AnsiConsole.Write(display);

while (true) {

    try {
        var seq = AnsiConsole.Ask<string>("Move Sequenz: ");

        if (seq == "exit") {
            Environment.Exit(0);
        }
        if (seq == "reset") {
            cube = new RubiksCube();
            lock (_displayLock) {
                DisplayRubiks(cube);
                AnsiConsole.Write(display);
            }
            continue;
        }


        AnsiConsole.Live(display).Start(ctx => {

            switch (seq) {
                case "scramble":
                    foreach (var move in cube.Scramble()) {
                        lock (_displayLock) {
                            DisplayRubiks(cube);
                            ctx.Refresh();
                        }
                        Task.Delay(20).Wait();
                    }
                    break;
                case "solve":
                    Solver solv = new OldHoffmanSolver(cube);
                    List<RubiksMove> moves = new List<RubiksMove>();
                    foreach (var move in solv.Solve()) {
                        moves.Add(move);
                        lock (_displayLock) {
                            DisplayRubiks(cube);
                            ctx.Refresh();
                        }
                        Task.Delay(1).Wait();
                    }
                    AnsiConsole.WriteLine(Move.ToReadableString(moves.ToArray()));
                    break;
                default:
                    foreach (var move in cube.Move(seq)) {
                        lock (_displayLock) {
                            DisplayRubiks(cube);
                            ctx.Refresh();
                        }
                        Task.Delay(50).Wait();
                    }
                    break;
            }

        });

        AnsiConsole.WriteLine("Gelöst: " + cube.IsSolved());
    } catch (Exception ex) {

        AnsiConsole.MarkupLine("[red] Error: [/]");
        AnsiConsole.WriteException(ex);

    }
}


Console.ReadLine();


void DisplayRubiks(RubiksCube cube) {

    Spectre.Console.Color GetColor(RubiksColor color) {
        switch (color) {
            case RubiksColor.White: return Color.White;
            case RubiksColor.Red: return Color.Red;
            case RubiksColor.Yellow: return Color.Yellow1;
            case RubiksColor.Orange: return Color.Orange1;
            case RubiksColor.Green: return Color.Green;
            case RubiksColor.Blue: return Color.Blue;

            default: return Color.Magenta1;
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
    SetFacePixels(RubiksFace.BACK, 9, 3);
    SetFacePixels(RubiksFace.DOWN, 3, 6);

}