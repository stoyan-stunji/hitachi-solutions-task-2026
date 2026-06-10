namespace HitachiTask;

public sealed class NavigatorService {
    private readonly SSSPStrategy strategy;
    public NavigatorService(SSSPStrategy strategy) {
        this.strategy = strategy;
    }

    public PathResultDto Navigate(Grid grid, Astronaut astronaut) {
        return strategy.FindPath(grid, astronaut.StartPosition, grid.Destination);
    }
}