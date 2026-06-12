using HitachiTask.DataStructures;

namespace HitachiTask.Dto.Mission;

public sealed class MissionResultDto {
    public required Astronaut Astronaut {
        get; 
        init;
    }

    public required PathResultDto PathResultDto {
        get; 
        init;
    }
}