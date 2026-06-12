using System.Text;
using HitachiTask.DataStructures;
using HitachiTask.Dto.Mission;

namespace HitachiTask.Service.Email;

public sealed class EmailMissionReportBuilder {
    public string Build(Grid grid, List<MissionResultDto> results) {
        StringBuilder sb = new();
        AppendHeader(sb);
        AppendResults(sb, grid, results);
        return sb.ToString();
    }

    private static void AppendHeader(StringBuilder sb) {
        sb.AppendLine("Mission Report");
        sb.AppendLine();
    }

    private static void AppendResults(StringBuilder sb, Grid grid, List<MissionResultDto> results) {
        foreach (var result in results) {
            AppendSingleResult(sb, grid, result);
        }
    }

    private static void AppendSingleResult(StringBuilder sb, Grid grid, MissionResultDto result) {
        sb.AppendLine($"Astronaut: {result.Astronaut.Name}");
        if (!result.PathResultDto.Found) {
            AppendFailure(sb);
            return;
        }
        AppendSuccess(sb, result);
        AppendMap(sb, grid, result);
    }

    private static void AppendFailure(StringBuilder sb) {
        sb.AppendLine("Status: FAILED");
        sb.AppendLine("Reason: No path to destination.");
        sb.AppendLine();
    }

    private static void AppendSuccess(StringBuilder sb, MissionResultDto result) {
        sb.AppendLine("Status: SUCCESS");
        sb.AppendLine($"Distance: {result.PathResultDto.Distance}");
        sb.AppendLine();
    }

    private static void AppendMap(StringBuilder sb, Grid grid, MissionResultDto result) {
        sb.AppendLine(MapPrinter.Render(grid, result.PathResultDto.Path));
        sb.AppendLine();
    }
}