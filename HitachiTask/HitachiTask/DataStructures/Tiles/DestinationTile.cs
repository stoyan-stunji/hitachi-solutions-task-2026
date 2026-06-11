namespace HitachiTask;

public sealed class DestinationTile : Tile {
    public DestinationTile(Position position) {
        Position = position;
    }

    public Position Position {
        get;
    }

    public bool IsWalkable => true;

    public string Symbol => "F";
    
    public int MovementCost => 1;
}