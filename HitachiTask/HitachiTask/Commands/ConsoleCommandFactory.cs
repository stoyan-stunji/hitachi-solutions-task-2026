using HitachiTask.Commands.Utility;

namespace HitachiTask.Commands;

public static class ConsoleCommandFactory {
    public static Command Create(SpaceNavigationApp app) {
        while (true) {
            Console.WriteLine("Hitachi Solutions Europe - Assessment Task - SPACE 2026");
            Console.WriteLine("[1]: Custom Map");
            Console.WriteLine("[2]: Random Map");
            Console.WriteLine("[3]: Exit");

            string choice = Console.ReadLine()!;
            switch (choice) {
                case "1": return new InputCustomMapCommand(app, new MapParser());
                case "2": return new RandomizeMapCommand(app, new RandomMapGenerator());
                case "3": return new ExitCommand();
                default: 
                    Console.WriteLine("Invalid option. Please choose [1], [2] or [3]!");
                    break;
            }
        }
    }
}