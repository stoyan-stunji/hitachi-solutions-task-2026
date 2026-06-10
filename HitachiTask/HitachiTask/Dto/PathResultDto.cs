namespace HitachiTask;

public sealed class PathResultDto {
    public bool Found {
        get; 
        init;
    }

    public int Distance {
        get; 
        init;
    }

    public List<Position> Path
    {
        get; 
        init;
    } = new();
}