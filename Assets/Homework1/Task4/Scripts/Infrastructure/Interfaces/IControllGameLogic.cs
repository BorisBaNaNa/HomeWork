using System.Collections.Generic;
using UnityEngine;

public interface IControllGameLogic
{
    void Initialize(List<BallDissolver> spawnedBalls);

    void CheckWin(Color ballColor);
}

public enum GameLogics
{
    AllClickForWinLogic,
    OneColorClickForWinLogic
}