namespace HitachiTask;
public sealed class MapContext {
    public Grid Grid {
        get; 
        init;
    } = null!;

    public List<Astronaut> Astronauts {
        get; 
        init;
    } = new();
}