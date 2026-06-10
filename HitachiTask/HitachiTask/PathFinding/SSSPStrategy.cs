namespace HitachiTask;

public interface SSSPStrategy {
    PathResultDto FindPath(Grid grid, Position source, Position destination);
}