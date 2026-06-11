namespace HitachiTask;

public sealed class RandomMapGenerator {
    private readonly Random random = new();
    public MapContext Generate(int rows, int cols, int astronautsCount, int asteroidsCount, int debrisCount) {
        ValidateInput(astronautsCount, rows, cols, asteroidsCount, debrisCount);

        Tile[,] tiles = new Tile[rows, cols];
        List<Astronaut> astronauts = new();
        List<Position> freeCells = CreateFreeCells(rows, cols);
        
        Position destination = PlaceDestination(tiles, freeCells);
        PlaceAstronauts(astronautsCount, tiles, astronauts, freeCells);
        PlaceAsteroids(asteroidsCount, tiles, freeCells);
        PlaceDebris(debrisCount, tiles, freeCells);
        FillRemainingCells(tiles, freeCells);

        return CreateContext(tiles, astronauts, destination);
    }
    
    private static void ValidateInput(int astronautsCount, int rows, int cols, int asteroidsCount, int debrisCount) {
        if (astronautsCount < 1 || astronautsCount > 3) {
            throw new ArgumentException("RandomMapGenerator::ValidateInput()::Astronauts must be between 1 and 3!");
        }

        int totalNeeded = 1 + astronautsCount + asteroidsCount + debrisCount;
        if (totalNeeded > rows * cols) {
            throw new ArgumentException("RandomMapGenerator::ValidateInput()::Not enough space on the grid!");
        }
    }
    
    private static List<Position> CreateFreeCells(int rows, int cols) {
        List<Position> freeCells = new();
        for (int r = 0; r < rows; r++) {
            for (int c = 0; c < cols; c++) {
                freeCells.Add(new Position(r, c));
            }
        }
        return freeCells;
    }
    
    private Position TakeRandomCell(List<Position> freeCells) {
        int index = random.Next(freeCells.Count);
        Position position = freeCells[index];
        freeCells.RemoveAt(index);
        return position;
    }
    
    private Position PlaceDestination(Tile[,] tiles, List<Position> freeCells) {
        Position position = TakeRandomCell(freeCells);
        tiles[position.Row, position.Col] = new DestinationTile(position);
        return position;
    }

    private void PlaceAstronauts(int count, Tile[,] tiles, List<Astronaut> astronauts, List<Position> freeCells) {
        for (int i = 1; i <= count; i++) {
            string name = $"S{i}";
            
            Position position = TakeRandomCell(freeCells);
            astronauts.Add(new Astronaut(name, position));
            
            tiles[position.Row, position.Col] = new AstronautTile(position, name);
        }
    }
    
    private void PlaceAsteroids(int count, Tile[,] tiles, List<Position> freeCells) {
        for (int i = 0; i < count; i++) {
            Position position = TakeRandomCell(freeCells);
            tiles[position.Row, position.Col] = new AsteroidTile(position);
        }
    }
    
    private void PlaceDebris(int count, Tile[,] tiles, List<Position> freeCells) {
        for (int i = 0; i < count; i++) {
            Position position = TakeRandomCell(freeCells);
            tiles[position.Row, position.Col] = new DebrisTile(position);
        }
    }
    
    private static void FillRemainingCells(Tile[,] tiles, List<Position> freeCells) {
        foreach (Position position in freeCells) {
            tiles[position.Row, position.Col] = new OpenSpaceTile(position);
        }
    }
    
    private static MapContext CreateContext(Tile[,] tiles, List<Astronaut> astronauts, Position destination) {
        return new MapContext { Grid = new Grid(tiles, destination), Astronauts = astronauts };
    }
}