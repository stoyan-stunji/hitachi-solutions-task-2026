namespace HitachiTask;

public sealed class MapParser {
    public MapContext Parse(int rows, int cols, IEnumerable<string> lines) {
        Tile[,] tiles = new Tile[rows, cols];
        List<Astronaut> astronauts = new();
        Position destination = default!;

        int row = 0;
        foreach (string line in lines) {
            ParseLine(line, row, cols, tiles, astronauts, ref destination);
            row++;
        }
        
        return CreateContext(tiles, astronauts, destination);
    }
    
    private void ParseLine(string line, int row, int cols, Tile[,] tiles, 
        List<Astronaut> astronauts, ref Position destination) {
        string[] symbols = SplitLine(line);

        for (int col = 0; col < cols; col++) {
            Position position = new Position(row, col);
            ParseCell(symbols[col], position, tiles, astronauts, ref destination);
        }
    }
    
    private void ParseCell(string symbol, Position position, Tile[,] tiles, 
        List<Astronaut> astronauts, ref Position destination) {
        switch (symbol)
        {
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
                tiles[position.Row, position.Col] = CreateDestination(position);
                break;

            case "S1":
            case "S2":
            case "S3":
                AddAstronaut(symbol, position, astronauts, tiles);
                break;
        }
    }
    private static Tile CreateOpen(Position p) => new OpenSpaceTile(p);
    private static Tile CreateAsteroid(Position p) => new AsteroidTile(p);
    private static Tile CreateDebris(Position p) => new DebrisTile(p);
    private static Tile CreateDestination(Position p) => new DestinationTile(p);

    private static void AddAstronaut(string symbol, Position position, List<Astronaut> astronauts, Tile[,] tiles) {
        astronauts.Add(new Astronaut(symbol, position));
        tiles[position.Row, position.Col] = new AstronautTile(position, symbol);
    }
    
    private static string[] SplitLine(string line) {
        return line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    }
    
    private static MapContext CreateContext(Tile[,] tiles, List<Astronaut> astronauts, Position destination) {
        Grid grid = new Grid(tiles, destination);
        return new MapContext { Grid = grid, Astronauts = astronauts };
    }
}