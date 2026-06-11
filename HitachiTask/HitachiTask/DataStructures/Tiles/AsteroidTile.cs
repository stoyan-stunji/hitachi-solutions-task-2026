namespace HitachiTask;

public sealed class AsteroidTile : Tile {
    public AsteroidTile(Position position) {
        Position = position;
    }
    public Position Position { get; }

    public bool IsWalkable => false;

    public string Symbol => "X";
    
    public int MovementCost => int.MaxValue;
}