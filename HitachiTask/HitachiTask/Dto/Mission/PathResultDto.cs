using HitachiTask.DataStructures;

namespace HitachiTask.Dto.Mission;

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