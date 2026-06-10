namespace HitachiTask;

public sealed class Astronaut {
    public Astronaut(string name, Position startPosition) {
        Name = name;
        StartPosition = startPosition;
    }

    public string Name {
        get;
    }

    public Position StartPosition {
        get;
    }
}