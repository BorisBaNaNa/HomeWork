public class PlayerUIMediator
{
    private readonly Player2 _player;
    private readonly UIController _uIController;

    public PlayerUIMediator(Player2 player, UIController uIController)
    {
        _player = player;
        _player.OnLevelUp += OnPlayerLvlUp;
        _player.OnHpEdited += OnPlayerHpEdeted;
        _player.OnDead += OnPlayerDead;

        _uIController = uIController;
    }

    private void OnPlayerHpEdeted(float maxHp, float curHp) =>
        _uIController.ShowPlayerHP(maxHp, curHp);

    private void OnPlayerLvlUp(float maxHp, float curHp, int curLvl)
    {
        _uIController.ShowPlayerHP(maxHp, curHp);
        _uIController.ShowPlayerLvl(curLvl);
    }

    private void OnPlayerDead()
    {
        _uIController.ShowDefeatWnd();
    }
}