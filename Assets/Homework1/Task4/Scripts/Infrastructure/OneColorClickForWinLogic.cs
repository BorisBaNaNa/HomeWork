using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OneColorClickForWinLogic : IControllGameLogic
{
    private Color _targetColor = Color.white;
    private int _maxBallCount;
    private int _curActiveBallsCount;

    public void Initialize(List<BallDissolver> spawnedBalls)
    {
        _maxBallCount = spawnedBalls.Count(ball => ball.BaseColor == _targetColor);
        _curActiveBallsCount = 0;
    }

    public void CheckWin(Color ballColor)
    {
        if (ballColor != _targetColor)
            return;

        if (++_curActiveBallsCount == _maxBallCount)
            Debug.Log("YouWin");
    }

    public void SetTargetColor(Color color)
    {
        _targetColor = color;
    }
}
