using System.Collections.Generic;
using UnityEngine;

public class AllClickForWinLogic : IControllGameLogic
{
    private int _maxBallCount;
    private int _curActiveBallsCount;

    public void Initialize(List<BallDissolver> spawnedBalls)
    {
        _maxBallCount = spawnedBalls.Count;
        _curActiveBallsCount = 0;
    }

    public void CheckWin(Color ballColor)
    {
        if (++_curActiveBallsCount == _maxBallCount)
            Debug.Log("YouWin");
    }
}
