namespace HitachiTask;

public sealed class RandomizeMapCommand : MapCommand {
    private readonly RandomMapGenerator generator;
    public RandomizeMapCommand(RandomMapGenerator generator) {
        this.generator = generator;
    }

    public MapContext Execute() {
        Console.Write("Rows: ");
        int rows = int.Parse(Console.ReadLine()!);

        Console.Write("Cols: ");
        int cols = int.Parse(Console.ReadLine()!);

        Console.Write("Astronauts (1-3): ");
        int astronauts = int.Parse(Console.ReadLine()!);

        Console.Write("Asteroids: ");
        int asteroids = int.Parse(Console.ReadLine()!);

        Console.Write("Debris: ");
        int debris = int.Parse(Console.ReadLine()!);

        return generator.Generate(rows, cols, astronauts, asteroids, debris);
    }
}