namespace HitachiTask.DataStructures.Tiles;

public interface Tile {
    Position Position {
        get;
    }

    bool IsWalkable {
        get;
    }

    string Symbol {
        get;
    }

    int MovementCost {
        get;
    }
}