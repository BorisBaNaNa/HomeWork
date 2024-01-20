using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsBoostraper : MonoBehaviour
{
    private void Awake()
    {
        InitServices();
    }

    private void InitServices()
    {
        AllServices.RegisterService_(new GameManager(GetGameLogics()));
    }

    private IControllGameLogic[] GetGameLogics() => new IControllGameLogic[]
    {
        new AllClickForWinLogic(),
        new OneColorClickForWinLogic()
    };
}
