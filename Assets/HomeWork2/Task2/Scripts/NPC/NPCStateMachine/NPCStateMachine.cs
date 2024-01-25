using UnityEngine;

public class NPCStateMachine : BaseStateMachine
{
    public NPCStateMachine(Character character, (Transform workPoint, Transform sleepPoint)targetPoints, (float sleepDur, float workDur)statesConf)
    {
        var statesData = new NPCStatesData(targetPoints.sleepPoint, targetPoints.workPoint)
        {
            CurrentMovementTarget = targetPoints.workPoint
        };

        _states = new IState[]
        {
            new MoveToTargetState(this, statesData, character),
            new SleepState(this, statesData, statesConf.sleepDur),
            new WorkState(this, statesData, statesConf.workDur),
        };
    }
}
