namespace HitachiTask;

public sealed class Grid {
    public Grid(Tile[,] tiles, Position destination) {
        Tiles = tiles;
        Destination = destination;
    }

    public Tile[,] Tiles {
        get;
    }

    public Position Destination {
        get;
    }
    
    public bool IsInside(Position p) {
        return p.Row >= 0 && p.Row < Rows && p.Col >= 0 && p.Col < Cols;
    }

    public Tile GetTile(Position p) {
        return Tiles[p.Row, p.Col];
    }
    
    private int Rows => Tiles.GetLength(0);

    private int Cols => Tiles.GetLength(1);
}