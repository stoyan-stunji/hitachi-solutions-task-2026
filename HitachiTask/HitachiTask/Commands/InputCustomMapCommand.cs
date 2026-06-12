using HitachiTask.Commands.Utility;

namespace HitachiTask.Commands;

public sealed class InputCustomMapCommand(SpaceNavigationApp app, MapParser mapParser) : Command {
    public void Execute() {
        Console.Write("Input Number Of Rows: ");
        int rows = int.Parse(Console.ReadLine()!);
        ValidateRows(rows);

        Console.Write("Input Number Of Cols: ");
        int cols = int.Parse(Console.ReadLine()!);
        ValidateCols(cols);
        
        Console.Write("Input Cosmic Map: \n");
        List<string> lines = new();
        for (int i = 0; i < rows; i++)
        {
            lines.Add(Console.ReadLine()!);
        }

        MapContext context = mapParser.Parse(rows, cols, lines);
        app.SetContext(context);
    }

    private static void ValidateRows(int rows) {
        if (rows < MapConstraints.MinRows || rows > MapConstraints.MaxRows) {
            throw new ArgumentException("InputCustomMapCommand::ValidateRows()::Rows must be in range [2,100]!");
        }
    }

    private static void ValidateCols(int cols) {
        if (cols < MapConstraints.MinCols || cols > MapConstraints.MaxCols) {
            throw new ArgumentException("InputCustomMapCommand::ValidateCols()::Cols must be in range [2,100]!");
        }
    }
}