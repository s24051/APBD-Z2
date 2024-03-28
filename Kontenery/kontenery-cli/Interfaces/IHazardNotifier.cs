namespace kontenery_cli.Interfejsy;

public interface IHazardNotifier
{
    public void printHazardWarning(string message, string container);
}