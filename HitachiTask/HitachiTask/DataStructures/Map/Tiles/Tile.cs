namespace HitachiTask;

public interface Tile {
    Position Position { get; }

    bool IsWalkable { get; }

    string Symbol { get; }
}