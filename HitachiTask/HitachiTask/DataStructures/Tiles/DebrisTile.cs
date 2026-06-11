namespace HitachiTask;

public sealed class DebrisTile : Tile {
    public DebrisTile(Position position) {
        Position = position;
    }

    public Position Position {
        get;
    }

    public bool IsWalkable => true;

    public string Symbol => "D";

    public int MovementCost => 2;
}