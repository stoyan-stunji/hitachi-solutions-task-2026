namespace HitachiTask.DataStructures.Tiles;

public sealed class OpenSpaceTile : Tile {
    public OpenSpaceTile(Position position) {
        Position = position;
    }

    public Position Position {
        get;
    }

    public bool IsWalkable => true;

    public string Symbol => "0";
    
    public int MovementCost => 1;
}