namespace HitachiTask;

internal class Program
{
    static void Main(string[] args)
    {
        int rows = int.Parse(Console.ReadLine()!);
        int cols = int.Parse(Console.ReadLine()!);

        Tile[,] tiles = new Tile[rows, cols];

        List<Astronaut> astronauts = new();

        Position destination = null!;

        for (int row = 0; row < rows; row++)
        {
            string[] symbols = Console.ReadLine()!
                .Split(' ', StringSplitOptions.RemoveEmptyEntries);

            for (int col = 0; col < cols; col++)
            {
                string symbol = symbols[col];

                Position position = new(row, col);

                switch (symbol)
                {
                    case "0":
                    case "O":
                        tiles[row, col] =
                            new OpenSpaceTile(position);
                        break;

                    case "X":
                        tiles[row, col] =
                            new AsteroidTile(position);
                        break;

                    case "F":
                        destination = position;

                        tiles[row, col] =
                            new DestinationTile(position);
                        break;

                    case "S1":
                    case "S2":
                    case "S3":
                        astronauts.Add(
                            new Astronaut(
                                symbol,
                                position));

                        tiles[row, col] =
                            new AstronautTile(
                                position,
                                symbol);
                        break;
                }
            }
        }

        Grid grid =
            new Grid(
                tiles,
                destination);

        SSSPStrategy strategy =
            new DijkstraStrategy();

        NavigatorService navigatorService =
            new NavigatorService(strategy);

        List<MissionResultDto> results =
            astronauts
                .Select(a =>
                    new MissionResultDto
                    {
                        Astronaut = a,
                        PathResultDto =
                            navigatorService.Navigate(grid, a)
                    })
                .ToList();

        foreach (var failed in results.Where(r => !r.PathResultDto.Found))
        {
            Console.WriteLine(
                $"Mission failed — Astronaut {failed.Astronaut.Name} lost in space!");
        }

        foreach (var success in results
                     .Where(r => r.PathResultDto.Found)
                     .OrderBy(r => r.PathResultDto.Distance))
        {
            Console.WriteLine(
                $"Astronaut {success.Astronaut.Name} - Shortest path: {success.PathResultDto.Distance} steps");

            PrintMap(
                rows,
                cols,
                success.PathResultDto.Path,
                tiles);

            Console.WriteLine();
        }
    }

    private static void PrintMap(
        int rows,
        int cols,
        List<Position> path,
        Tile[,] originalTiles)
    {
        HashSet<Position> pathCells =
            path.Skip(1)
                .SkipLast(1)
                .ToHashSet();

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                Position current =
                    new Position(row, col);

                string symbol;

                if (pathCells.Contains(current))
                {
                    symbol = "*";
                }
                else
                {
                    symbol =
                        originalTiles[row, col]
                            .Symbol;
                }

                Console.Write(symbol);

                if (col < cols - 1)
                {
                    Console.Write(" ");
                }
            }

            Console.WriteLine();
        }
    }
}