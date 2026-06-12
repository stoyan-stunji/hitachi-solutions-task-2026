namespace HitachiTask.Commands;

public sealed class ExitCommand : Command {
    public void Execute() {
        Environment.Exit(0);
    }
}