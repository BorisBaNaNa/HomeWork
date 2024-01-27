public class LevelManagerUIMediator
{
    readonly UIController _uIController;
    readonly LevelManager _levelManager;

    public LevelManagerUIMediator(UIController uIController, LevelManager levelManager)
    {
        _uIController = uIController;
        _uIController.OnNeedRestart += ResetLevelAction;

        _levelManager = levelManager;
    }

    private void ResetLevelAction() => _levelManager.ResetLevel();
}
