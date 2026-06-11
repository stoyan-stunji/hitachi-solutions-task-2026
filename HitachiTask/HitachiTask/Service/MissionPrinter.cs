namespace HitachiTask;

public sealed class MissionPrinter {
    public void Print(Grid grid, List<MissionResultDto> results) {
        PrintFailures(results);
        PrintSuccesses(grid, results);
    }

    private static void PrintFailures(List<MissionResultDto> results) {
        foreach (var failed in results.Where(r => !r.PathResultDto.Found)) {
            Console.WriteLine($"Mission failed — Astronaut {failed.Astronaut.Name} lost in space!");
        }
    }

    private static void PrintSuccesses(Grid grid, List<MissionResultDto> results) {
        foreach (var success in results
                     .Where(r => r.PathResultDto.Found)
                     .OrderBy(r => r.PathResultDto.Distance)) {
            Console.WriteLine($"Astronaut {success.Astronaut.Name} - Shortest path: {success.PathResultDto.Distance}");
            MapPrinter.Print(grid, success.PathResultDto.Path);
            Console.WriteLine();
        }
    }
}