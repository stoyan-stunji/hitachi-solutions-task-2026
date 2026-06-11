namespace HitachiTask;

public sealed class SpaceNavigationApp {
    private readonly MissionRunnerService missionRunnerService;
    private readonly MissionPrinter missionPrinter;

    public SpaceNavigationApp() {
        SSSPStrategy strategy = new DijkstraStrategy();
        missionRunnerService = new MissionRunnerService(strategy);
        missionPrinter = new MissionPrinter();
    }

    public void Run() {
        while (true) {
            MapCommand command = CommandFactory.Create();

            if (command is ExitCommand) {
                Console.WriteLine("Exiting application...");
                break;
            }

            MapContext context = command.Execute()!;
            List<MissionResultDto> results = missionRunnerService.Run(context);
            missionPrinter.Print(context.Grid, results);
        }
    }
}