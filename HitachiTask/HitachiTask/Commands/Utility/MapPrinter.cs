using System.Text;
using HitachiTask;
using HitachiTask.DataStructures;

public static class MapPrinter {
    public static string Render(Grid grid, List<Position> path) {
        HashSet<Position> pathCells = path.Skip(1).SkipLast(1).ToHashSet();

        int rows = grid.Tiles.GetLength(0);
        int cols = grid.Tiles.GetLength(1);

        StringBuilder sb = new();
        for (int row = 0; row < rows; row++) {
            for (int col = 0; col < cols; col++) {
                Position current = new(row, col);
                string symbol = pathCells.Contains(current) ? "*" : grid.GetTile(current).Symbol;
                sb.Append(symbol);

                if (col < cols - 1) {
                    sb.Append(' ');
                }
            }
            sb.AppendLine();
        }
        return sb.ToString();
    }

    public static void Print(Grid grid, List<Position> path) {
        Console.WriteLine(Render(grid, path));
    }
}