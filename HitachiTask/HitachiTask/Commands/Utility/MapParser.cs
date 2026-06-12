using HitachiTask.Commands.Utility;
using HitachiTask.DataStructures;
using HitachiTask.DataStructures.Tiles;

public sealed class MapParser {
    public MapContext Parse(int rows, int cols, IEnumerable<string> lines) {
        Tile[,] tiles = new Tile[rows, cols];
        List<Astronaut> astronauts = new();
        Position destination = default!;
        bool destinationFound = false;

        int row = 0;
        foreach (string line in lines) {
            ParseLine(line, row, cols, tiles, astronauts, ref destination, ref destinationFound);
            row++;
        }
        
        ValidateAstronautCount(astronauts);
        if (!destinationFound) {
            throw new ArgumentException("MapParser::ValidateMap()::Map must contain exactly one destination (F)!");
        }
        
        return CreateContext(tiles, astronauts, destination);
    }

    private void ParseLine(string line, int row, int cols, Tile[,] tiles, List<Astronaut> astronauts,
        ref Position destination, ref bool destinationFound) {
        string[] symbols = SplitLine(line);

        if (symbols.Length != cols) {
            throw new ArgumentException(
                $"MapParser::ParseLine()::Row {row} must contain exactly {cols} symbols, but got {symbols.Length}!");
        }

        for (int col = 0; col < cols; col++) {
            Position position = new(row, col);
            ParseCell(symbols[col], position, tiles, astronauts, ref destination, ref destinationFound);
        }
    }

    private static void ParseCell(string symbol, Position position, Tile[,] tiles, List<Astronaut> astronauts,
        ref Position destination, ref bool destinationFound) {
        switch (symbol) {
            case "0":
            case "O":
                tiles[position.Row, position.Col] = CreateOpen(position);
                break;

            case "X":
                tiles[position.Row, position.Col] = CreateAsteroid(position);
                break;

            case "D":
                tiles[position.Row, position.Col] = CreateDebris(position);
                break;

            case "F":
                destination = position;
                destinationFound = true;
                tiles[position.Row, position.Col] = CreateDestination(position);
                break;

            case "S1":
            case "S2":
            case "S3":
                AddAstronaut(symbol, position, astronauts, tiles);
                break;

            default: throw new ArgumentException(
                    $"MapParser::ParseCell()::Invalid symbol '{symbol}' at position ({position.Row},{position.Col})!");
        }
    }

    private static void ValidateAstronautCount(List<Astronaut> astronauts) {
        if (astronauts.Count < MapConstraints.MinAstronauts || astronauts.Count > MapConstraints.MaxAstronauts) {
            throw new ArgumentException($"MapParser::ValidateMap()::Astronaut count must be between " +
                                        $"{MapConstraints.MinAstronauts} and {MapConstraints.MaxAstronauts}!");
        }
    }

    private static OpenSpaceTile CreateOpen(Position position) => new(position);
    private static AsteroidTile CreateAsteroid(Position position) => new(position);
    private static DebrisTile CreateDebris(Position position) => new(position);
    private static DestinationTile CreateDestination(Position position) => new(position);

    private static void AddAstronaut(string symbol, Position position, List<Astronaut> astronauts, Tile[,] tiles) {
        astronauts.Add(new Astronaut(symbol, position));
        tiles[position.Row, position.Col] = new AstronautTile(position, symbol);
    }

    private static string[] SplitLine(string line) => line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

    private static MapContext CreateContext(Tile[,] tiles, List<Astronaut> astronauts, Position destination) {
        Grid grid = new Grid(tiles, destination);
        return new MapContext { Grid = grid, Astronauts = astronauts };
    }
}