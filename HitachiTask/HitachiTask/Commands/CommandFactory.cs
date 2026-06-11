namespace HitachiTask;

public static class CommandFactory {
    public static MapCommand Create() {
        Console.WriteLine();
        Console.WriteLine("1 - Custom Map");
        Console.WriteLine("2 - Random Map");
        Console.WriteLine("3 - Exit");

        string choice = Console.ReadLine()!;
        return choice switch {
            "1" => new InputCustomMapCommand(new MapParser()),
            "2" => new RandomizeMapCommand(new RandomMapGenerator()),
            "3" => new ExitCommand(),
            _ => throw new InvalidOperationException("CommandFactory::Create()::Unknown command!")
        };
    }
}