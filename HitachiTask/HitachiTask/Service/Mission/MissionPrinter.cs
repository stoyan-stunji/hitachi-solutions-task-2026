using HitachiTask.DataStructures;
using HitachiTask.Dto.Mission;

namespace HitachiTask.Service.Mission;

public sealed class MissionPrinter {
    public void Print(Grid grid, List<MissionResultDto> results) {
        PrintFailures(results);
        PrintSuccesses(grid, results);
    }

    private static void PrintFailures(List<MissionResultDto> results) {
        foreach (MissionResultDto failed in results.Where(r => !r.PathResultDto.Found)) {
            Console.WriteLine($"\nMission failed - Astronaut {failed.Astronaut.Name} lost in space!");
        }
    }

    private static void PrintSuccesses(Grid grid, List<MissionResultDto> results) {
        foreach (MissionResultDto success in results
                     .Where(r => r.PathResultDto.Found)
                     .OrderBy(r => r.PathResultDto.Distance)) {
            Console.WriteLine($"\nAstronaut {success.Astronaut.Name} - Shortest path: {success.PathResultDto.Distance}");
            MapPrinter.Print(grid, success.PathResultDto.Path);
        }
    }
}