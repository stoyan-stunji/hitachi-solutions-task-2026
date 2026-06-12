using HitachiTask.Commands;
using HitachiTask.Commands.Utility;
using HitachiTask.Dto.Mission;
using HitachiTask.PathFinding;
using HitachiTask.Service.Email;
using HitachiTask.Service.Mission;

namespace HitachiTask;

public sealed class SpaceNavigationApp(SSSPStrategy strategy, EmailMissionWorkflowService workflowService) {
    private readonly MissionRunnerService _missionRunnerService = new(strategy);
    private readonly MissionPrinter _missionPrinter = new();

    private MapContext? _currentContext;

    public void Run() {
        while (true)
        {
            Command command = ConsoleCommandFactory.Create(this);
            try {
                command.Execute();
            }
            catch (ArgumentException ex) {
                Console.WriteLine($"Invalid input: {ex.Message}");
                continue;
            }
            catch (Exception ex) {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                continue;
            }

            if (_currentContext is null) {
                continue;
            }

            List<MissionResultDto> results = _missionRunnerService.Run(_currentContext);
            _missionPrinter.Print(_currentContext.Grid, results);
            workflowService.Run(_currentContext.Grid, results);
        }
    }

    public void SetContext(MapContext context) {
        _currentContext = context;
    }
}