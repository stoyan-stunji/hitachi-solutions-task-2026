namespace HitachiTask;

public sealed class MissionRunnerService {
    private readonly NavigatorService navigatorService;

    public MissionRunnerService(SSSPStrategy strategy) {
        this.navigatorService = new NavigatorService(strategy);
    }

    public List<MissionResultDto> Run(MapContext context) {
        List<MissionResultDto> results = new();

        foreach (Astronaut astronaut in context.Astronauts) {
            PathResultDto path = navigatorService.Navigate(context.Grid, astronaut);
            results.Add(new MissionResultDto { Astronaut = astronaut, PathResultDto = path });
        }

        return results;
    }
}