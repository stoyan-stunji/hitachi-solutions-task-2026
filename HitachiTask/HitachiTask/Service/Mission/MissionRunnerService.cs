using HitachiTask.Commands.Utility;
using HitachiTask.DataStructures;
using HitachiTask.Dto.Mission;
using HitachiTask.PathFinding;

namespace HitachiTask.Service.Mission;

public sealed class MissionRunnerService(SSSPStrategy strategy) {
    private readonly MissionNavigatorService _missionNavigatorService = new(strategy);

    public List<MissionResultDto> Run(MapContext context) {
        List<MissionResultDto> results = new();
        foreach (Astronaut astronaut in context.Astronauts) {
            PathResultDto path = _missionNavigatorService.Navigate(context.Grid, astronaut);
            results.Add(new MissionResultDto { Astronaut = astronaut, PathResultDto = path });
        }
        return results;
    }
}