namespace HitachiTask;

public sealed class InputCustomMapCommand : MapCommand
{
    private readonly MapParser parser;
    public InputCustomMapCommand(MapParser parser) {
        this.parser = parser;
    }

    public MapContext Execute() {
        Console.Write("Rows: ");
        int rows = int.Parse(Console.ReadLine()!);
        
        Console.Write("Cols: ");
        int cols = int.Parse(Console.ReadLine()!);

        List<string> lines = new();
        for (int i = 0; i < rows; i++) {
            lines.Add(Console.ReadLine()!);
        }

        return parser.Parse(rows, cols, lines);
    }
}