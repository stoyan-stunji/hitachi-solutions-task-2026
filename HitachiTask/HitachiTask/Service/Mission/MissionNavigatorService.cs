using HitachiTask.DataStructures;
using HitachiTask.Dto.Mission;
using HitachiTask.PathFinding;

namespace HitachiTask.Service.Mission;

public sealed class MissionNavigatorService(SSSPStrategy strategy) {
    public PathResultDto Navigate(Grid grid, Astronaut astronaut) {
        return strategy.FindPath(grid, astronaut.StartPosition, grid.Destination);
    }
}