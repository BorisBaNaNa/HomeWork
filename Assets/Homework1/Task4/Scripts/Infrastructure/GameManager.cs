using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : IService
{
    private IControllGameLogic[] _gameLogicControllers;
    private IControllGameLogic _curLogicController;

    public GameManager(IControllGameLogic[] gameLogicControllers)
    {
        _gameLogicControllers = gameLogicControllers;
    }

    public void SetGameLogic(GameLogics gameLogic)
    {
        _curLogicController = _gameLogicControllers.First(logic => logic.GetType().Name == gameLogic.ToString());
        if (_curLogicController == null)
            Debug.LogError($"{gameLogic}! type not founded");
    }

    public void SetColorForWin(Color winColor)
    {
        if (_curLogicController is not OneColorClickForWinLogic oneColorClickLogic)
            return;
        oneColorClickLogic.SetTargetColor(winColor);
    }

    public void InitGame(List<BallDissolver> spawnedBalls)
    {
        _curLogicController.Initialize(spawnedBalls);
    }

    public void BallWasActivated(Color ballColor)
    {
        _curLogicController.CheckWin(ballColor);
    }
}
