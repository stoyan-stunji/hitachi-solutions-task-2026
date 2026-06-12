using HitachiTask.Commands.Utility;
using HitachiTask.DataStructures;

namespace HitachiTask.Commands;

public sealed class RandomizeMapCommand(SpaceNavigationApp app, RandomMapGenerator generator)  : Command {
    public void Execute() {
        Console.Write("Input Number Of Rows: ");
        int rows = int.Parse(Console.ReadLine()!);
        ValidateRows(rows);

        Console.Write("Input Number Of Cols: ");
        int cols = int.Parse(Console.ReadLine()!);
        ValidateCols(cols);

        Console.Write("Input Number Of Astronauts: ");
        int astronauts = int.Parse(Console.ReadLine()!);
        ValidateAstronauts(astronauts);

        Console.Write("Input Number Of Asteroids: ");
        int asteroids = int.Parse(Console.ReadLine()!);

        Console.Write("Input Number Of Debris: ");
        int debris = int.Parse(Console.ReadLine()!);

        ValidateCapacity(rows, cols, astronauts, asteroids, debris);
        
        MapContext context = generator.Generate(rows, cols, astronauts, asteroids, debris);
        PrintMap(context);
        app.SetContext(context);
    }

    private static void ValidateRows(int rows) {
        if (rows < MapConstraints.MinRows || rows > MapConstraints.MaxRows) {
            throw new ArgumentException(
                $"RandomizeMapCommand::ValidateRows()::Rows must be in range [{MapConstraints.MinRows}, {MapConstraints.MaxRows}]");   
        }
    }

    private static void ValidateCols(int cols) {
        if (cols < MapConstraints.MinCols || cols > MapConstraints.MaxCols) {
            throw new ArgumentException(
                $"RandomizeMapCommand::ValidateCols()::Cols must be in range [{MapConstraints.MinCols}, {MapConstraints.MaxCols}]");   
        }
    }

    private static void ValidateAstronauts(int astronauts) {
        if (astronauts < MapConstraints.MinAstronauts || astronauts > MapConstraints.MaxAstronauts) {
            throw new ArgumentException(
                $"RandomizeMapCommand::ValidateAstronauts()::Astronauts must be in range [{MapConstraints.MinAstronauts}, {MapConstraints.MaxAstronauts}]");
        }
    }

    private static void ValidateCapacity(int rows, int cols, int astronauts, int asteroids, int debris) {
        int required = 1 + astronauts + asteroids + debris;

        if (required > rows * cols) {
            throw new ArgumentException(
                "RandomizeMapCommand::ValidateCapacity()::Map is too small for the requested number of objects!");
        }
    }

    private static void PrintMap(MapContext context) {
        Console.WriteLine();
        Console.WriteLine("Generated Map:");
        Console.WriteLine();

        MapPrinter.Print(context.Grid, new List<Position>());
        Console.WriteLine();
    }
}