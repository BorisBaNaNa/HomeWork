public class LevelManager
{
    private readonly IRestartable[] restartables;

    public LevelManager(IRestartable[] restartables)
    {
        this.restartables = restartables;
    }

    public void ResetLevel()
    {
        foreach (var restartable in restartables)
            restartable.Restart();
    }
}
