using HitachiTask.DataStructures;
using HitachiTask.Dto.Mission;

namespace HitachiTask.PathFinding;

public interface SSSPStrategy {
    PathResultDto FindPath(Grid grid, Position source, Position destination);
}