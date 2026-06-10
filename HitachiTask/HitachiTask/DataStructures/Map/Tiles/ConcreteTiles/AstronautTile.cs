namespace HitachiTask;

public sealed class AstronautTile : Tile {
    public AstronautTile(Position position, string astronautName) {
        Position = position;
        AstronautName = astronautName;
    }

    public string AstronautName { get; }

    public Position Position { get; }

    public bool IsWalkable => true;

    public string Symbol => AstronautName;
}